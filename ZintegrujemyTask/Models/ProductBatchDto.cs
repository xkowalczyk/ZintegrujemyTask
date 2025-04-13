using ZintegrujemyTask.Models.CSVConvert;

namespace ZintegrujemyTask.Models
{
    public class ProductBatchDto
    {
        public List<Product> Products { get; set; }
        public List<ProductPrice> Pricings { get; set; }
        public List<StockStatus> Stocks { get; set; }
    }
}
