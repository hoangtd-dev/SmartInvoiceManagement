using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IVendorService
    {
        Task<ICollection<VendorModel>> GetVendors();
        Task<VendorModel> GetVendorById(int id);
        Task AddVendor(CreateVendorRequest vendor);
        Task UpdateVendor(UpdateVendorRequest vendor);
        Task DeleteVendor(int id);
    }
}
