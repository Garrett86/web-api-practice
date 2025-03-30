using Microsoft.EntityFrameworkCore;
using System.Data;
using Test_Web_API.Entities;
using Test_Web_API.Models;

namespace Test_Web_API.Repositories
{
    //用於 Northwind .db .Supplier 資料庫
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindDbContext _northwindDbContext;
        private readonly ILogger<SupplierRepository> _logger;

        public SupplierRepository(NorthwindDbContext northwindDbContext, ILogger<SupplierRepository> logger)
        {
            _northwindDbContext = northwindDbContext;
            _logger = logger;
        }

        //找不到回傳 new List<Supplier>()
        public async Task<ApiResult> GetAllAsync() =>
            await RunSafeAsync(() => _northwindDbContext.Suppliers.ToListAsync());


        //FindAsync 會回傳 ValueTask 所以必須 .AsTask()
        public async Task<ApiResult> GetByIdAsync(int id) =>
            await RunSafeAsync(() => _northwindDbContext.Suppliers.FindAsync(id).AsTask());

        public async Task<ApiResult> UpdateAsync(int id, Supplier supplier)
        {
            return await RunSafeAsync(async () =>
            {
                var existringSupplier = await _northwindDbContext.Suppliers.FindAsync(id);
                if (existringSupplier is null) throw new Exception("資料不存在");

                foreach (var prop in typeof(Supplier).GetProperties())
                {
                    if (prop.Name == "SupplierID") //Id不修改
                        continue;

                    var newValue = prop.GetValue(supplier);
                    if (newValue is not null)
                        prop.SetValue(existringSupplier, newValue);
                }

                //儲存變更
                await _northwindDbContext.SaveChangesAsync();
                return new ApiResult { Success = true, Message = "更新成功" };
            });
        }
        public async Task<ApiResult> AddAsync(Supplier supplier)
        {
            return await RunSafeAsync(async () =>
            {
                if (supplier is null) throw new Exception("輸入資料錯誤");

                var existringSupplier = await _northwindDbContext.Suppliers.FindAsync(supplier.SupplierID);
                if (existringSupplier is not null) throw new Exception("資料已存在");

                await _northwindDbContext.Suppliers.AddAsync(supplier);
                await _northwindDbContext.SaveChangesAsync();
                return new ApiResult { Success = true, Message = "新增成功" };
            });
        }

        public async Task<ApiResult> DeleteAsync(int id)
        {
            return await RunSafeAsync(async () =>
            {
                var exitstringSupplier = await _northwindDbContext.Suppliers.FindAsync(id);
                if (exitstringSupplier is null) throw new Exception("資料不存在");

                _northwindDbContext.Remove(exitstringSupplier);
                return await _northwindDbContext.SaveChangesAsync();
            });
        }

        // 統一例外處理，出錯回傳設定值，方便控制層管理，並記錄錯誤訊息
        private async Task<ApiResult> RunSafeAsync<T>(Func<Task<T>> action)
        {
            try
            {
                var resultData = await action();
                return new ApiResult
                {
                    Data = resultData,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                var supplierTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _logger.LogError(ex, "Supplier 資料庫錯誤發生於: {Time}", supplierTime);
                return new ApiResult
                {
                    Data = default!,
                    ErrorMessage = ex.ToString()
                };
            }
        }
    }
}
