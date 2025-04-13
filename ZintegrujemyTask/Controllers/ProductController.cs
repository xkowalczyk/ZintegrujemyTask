using Microsoft.AspNetCore.Mvc;
using ZintegrujemyTask.Data;
using ZintegrujemyTask.Models;
using ZintegrujemyTask.Services;

namespace ZintegrujemyTask.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly CsvService _csvservice;
        private readonly DapperProductService _dapperproductservice;


        public ProductController(CsvService csvservice, DapperProductService dapperproductservice)
        {
            _csvservice = csvservice;
            _dapperproductservice = dapperproductservice;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import()
        {
            ProductBatchDto BatchData = await _csvservice.GetProductBatchAsync();
            await _dapperproductservice.AddBatchAsync(BatchData);

            return Ok(new { message = "Batch inserted successfully." });
        }

        [HttpGet("{sku}")]
        public async Task<IActionResult> GetProduct(string sku)
        {

            var product = await _dapperproductservice.GetProductBySkuAsync(sku);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }

}
