using Test_Web_API.Entities;
using Test_Web_API.Models;
using Test_Web_API.Repositories;

namespace Test_Web_API.Services
{
    public class SupplierDbSeriver : ISupplierDbSeriver
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierDbSeriver(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ApiResult> GetAllAsync()
            => await _supplierRepository.GetAllAsync();

        public async Task<ApiResult> GetByIdsync(Int32 id)
            => await _supplierRepository.GetByIdAsync(id);

        public async Task<ApiResult> CreateAsync(Supplier supplier)
        => await _supplierRepository.AddAsync(supplier);


        public async Task<ApiResult> UpdateAsync(Int32 id, Supplier supplier)
        => await _supplierRepository.UpdateAsync(id, supplier);

        public async Task<ApiResult> DeleteAsync(Int32 id)
        => await _supplierRepository.DeleteAsync(id);
    }
}
