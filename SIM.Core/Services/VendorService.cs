using SIM.Core.DTOs.Requests;
using SIM.Core.DTOs.Responses;
using SIM.Core.Entities;
using SIM.Core.Exceptions;
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

        public async Task AddVendor(CreateVendorRequest vendor)
        {
            var newVendor = new Vendor 
            { 
                ContactPhone = vendor.ContactPhone,
                ContactEmail = vendor.ContactEmail,
                Address = vendor.Address,
                VendorName = vendor.VendorName,
            };
            await _vendorRepository.AddAsync(newVendor);
        }

        public async Task DeleteVendor(int id)
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);
            if (vendor is null) throw new NotFoundException($"Vendor with id:{id} is not found !!!");

            await _vendorRepository.DeleteAsync(vendor);
        }

        public async Task<VendorModel> GetVendorById(int id)
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);

            if (vendor is null) throw new NotFoundException($"Vendor with id:{id} is not found !!!");

            return new VendorModel 
            { 
                Id = vendor.Id,
                Address = vendor.Address,
                ContactEmail = vendor.ContactEmail,
                ContactPhone = vendor.ContactPhone, 
                Name = vendor.VendorName
            };
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

        public async Task UpdateVendor(UpdateVendorRequest updatedVendor)
        {
            var vendor = await _vendorRepository.GetByIdAsync(updatedVendor.Id);

            if (vendor is null) throw new NotFoundException($"Vendor with id:{updatedVendor.Id} is not found !!!");

            vendor.VendorName = updatedVendor.VendorName;
            vendor.Address = updatedVendor.Address;
            vendor.ContactEmail = updatedVendor.ContactEmail;
            vendor.ContactPhone = updatedVendor.ContactPhone;

            await _vendorRepository.UpdateAsync(vendor);
        }
    }
}
