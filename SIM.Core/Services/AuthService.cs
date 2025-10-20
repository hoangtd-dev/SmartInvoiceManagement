using System.Security.Cryptography;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginModel> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null || !VerifyPassword(password, user.PasswordHash))
                {
                    throw new ArgumentException("EmailOrPasswordInvalid");
                }

                return new UserLoginModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("InternalError", ex);
            }
        }

        public async Task RegisterAsync(string firstName, string lastName, string email, string rawPassword)
        {
            try
            {
                var existing = await _userRepository.GetByEmailAsync(email);
                if (existing != null)
                    throw new ArgumentException("EmailAlreadyExists");

                var user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PasswordHash = HashPassword(rawPassword),
                };

                var created = await _userRepository.AddAsync(user);
                return;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("InternalError", ex);
            }
        }

        // PBKDF2 password hashing
        private static string HashPassword(string password)
        {
            // Format: {iterations}.{saltBase64}.{hashBase64}
            const int iterations = 100_000;
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                var parts = storedHash?.Split('.') ?? Array.Empty<string>();
                if (parts.Length != 3)
                    return false;

                int iterations = int.Parse(parts[0]);
                byte[] salt = Convert.FromBase64String(parts[1]);
                byte[] hash = Convert.FromBase64String(parts[2]);

                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
                byte[] computed = pbkdf2.GetBytes(hash.Length);

                return CryptographicOperations.FixedTimeEquals(computed, hash);
            }
            catch
            {
                return false;
            }
        }
    }
}
