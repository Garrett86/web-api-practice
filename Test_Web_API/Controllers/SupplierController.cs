using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Web_API.Entities;
using Test_Web_API.Models;
using Test_Web_API.Services;

namespace Test_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierDbSeriver _supplierDbSeriver;

        public SupplierController(ISupplierDbSeriver supplierDbSeriver)
        {
            _supplierDbSeriver = supplierDbSeriver;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            RunSafe(await _supplierDbSeriver.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Int32 id) =>
            RunSafe(await _supplierDbSeriver.GetByIdsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Supplier supplier) =>
            RunSafe(await _supplierDbSeriver.CreateAsync(supplier));

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(Int32 id, [FromBody] Supplier supplier) =>
            RunSafe(await _supplierDbSeriver.UpdateAsync(id, supplier));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Int32 id) =>
            RunSafe(await _supplierDbSeriver.DeleteAsync(id));

        private IActionResult RunSafe(ApiResult result)
        {
            try
            {
                //if (result.errormessage != string.empty)
                //    throw new exception(result.errormessage);
                //else if (result.data == null)
                //    return notfound();
                //else if (result.data is list<supplier> supplier && supplier.count == 0)
                //    return notfound();
                //else if (result.data is int statuscode)
                //    return statuscode > 0 ? ok() : notfound();
                //return ok(result);

                //優化判斷
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    throw new Exception(result.ErrorMessage); //拋出資料層錯誤

                if (result.Data == null || (result.Data is List<Supplier> suppliers && suppliers.Count == 0)) // 檢查單筆搜尋 or 多筆搜尋
                    return NotFound();

                if (result.Data is int statusCode)
                    return statusCode > 0 ? Ok() : NotFound();

                return Ok(result.Data);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
