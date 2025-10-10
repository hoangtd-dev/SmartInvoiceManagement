using SIM.Core.DTOs.Responses;
using SIM.Core.Interfaces.Repositories;
using SIM.Core.Interfaces.Services;

namespace SIM.Core.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRespository _vendorRepository;
        public VendorService(IVendorRespository vendorRespository)
        {
            _vendorRepository = vendorRespository;
        }
        public async Task<ICollection<VendorModel>> GetVendors()
        {
            var vendors = await _vendorRepository.GetAllAsync();

            return vendors.Select(v => new VendorModel { 
                Id = v.Id,
                Name = v.VendorName,
                ContactEmail = v.ContactEmail,
                ContactPhone = v.ContactPhone,
                Address = v.Address
            }).ToList();
        }
    }
}
