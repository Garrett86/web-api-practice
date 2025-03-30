using Test_Web_API.Entities;
using Test_Web_API.Models;

namespace Test_Web_API.Repositories
{
    public interface ISupplierRepository
    {
        Task<ApiResult> GetAllAsync();

        Task<ApiResult> GetByIdAsync(int id);

        Task<ApiResult> AddAsync(Supplier supplier);

        Task<ApiResult> UpdateAsync(int id, Supplier supplier);

        Task<ApiResult> DeleteAsync(int id);
    }
}
