using SIM.Core.DTOs.Responses;

namespace SIM.Core.Interfaces.Services
{
    public interface IVendorService
    {
        Task<ICollection<VendorModel>> GetVendors();
    }
}
