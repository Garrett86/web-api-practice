using Test_Web_API.Entities;
using Test_Web_API.Models;

namespace Test_Web_API.Services
{
    public interface ISupplierDbSeriver
    {
        Task<ApiResult> GetAllAsync();
        Task<ApiResult> GetByIdsync(Int32 id);
        Task<ApiResult> CreateAsync(Supplier supplier);
        Task<ApiResult> UpdateAsync(Int32 id, Supplier supplier);

        Task<ApiResult> DeleteAsync(Int32 id);
    }
}
