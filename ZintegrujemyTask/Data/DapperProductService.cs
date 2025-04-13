using Dapper;
using ZintegrujemyTask.Models;

namespace ZintegrujemyTask.Data
{
    public class DapperProductService
    {
        private readonly DapperContext _context;

        public DapperProductService(DapperContext context)
        {
            _context = context;
        }

        public async Task AddBatchAsync(ProductBatchDto dto)
        {
            using var connection = _context.CreateConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            string productSql = @"INSERT INTO Products (Id, SKU, Name, EAN, ProducerName, Category, IsVire, Shipping, IsAvailable, IsVendor, DefaultImage)
                VALUES (@Id, @SKU, @Name, @EAN, @ProducerName, @Category, @IsVire, @Shipping, @IsAvailable, @IsVendor, @DefaultImage)
                ON DUPLICATE KEY UPDATE
                    SKU = VALUES(SKU),
                    Name = VALUES(Name),
                    EAN = VALUES(EAN),
                    ProducerName = VALUES(ProducerName),
                    Category = VALUES(Category),
                    IsVire = VALUES(IsVire),
                    Shipping = VALUES(Shipping),
                    IsAvailable = VALUES(IsAvailable),
                    IsVendor = VALUES(IsVendor),
                    DefaultImage = VALUES(DefaultImage);";

            string pricingSql = @"INSERT INTO ProductPricing (SKU, TAX, NettoPrice)
            VALUES (@SKU, @TAX, @NettoPrice)
            ON DUPLICATE KEY UPDATE
                TAX = VALUES(TAX),
                NettoPrice = VALUES(NettoPrice);
            ";

            string stockSql = @"
            INSERT INTO ProductStock (ProductId, Stock, UnitType)
            VALUES (@ProductId, @Stock, @UnitType)
            ON DUPLICATE KEY UPDATE
                Stock = VALUES(Stock);
            ";

            try
            {
                
                foreach (var product in dto.Products)
                    await connection.ExecuteAsync(productSql, product, transaction);

                foreach (var pricing in dto.Pricings)
                    await connection.ExecuteAsync(pricingSql, pricing, transaction);
                
                foreach (var stock in dto.Stocks)
                    await connection.ExecuteAsync(stockSql, stock, transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<ProductDto> GetProductBySkuAsync(string sku)
        {
            using var connection = _context.CreateConnection();

            var product = await connection.QueryFirstOrDefaultAsync<ProductDto>(
                @"SELECT * FROM Products WHERE SKU = @SKU", new { SKU = sku });

            if (product == null)
                return null;

            var pricing = await connection.QueryFirstOrDefaultAsync(
                @"SELECT TAX, NettoPrice FROM ProductPricing WHERE SKU = @SKU", new { SKU = sku });

            if (pricing != null)
            {
                product.TAX = pricing.TAX;
                product.NettoPrice = pricing.NettoPrice;
            }

            var stock = await connection.QueryFirstOrDefaultAsync(
                @"SELECT Stock, UnitType FROM ProductStock WHERE ProductId = @ProductId", new { ProductId = product.Id });

            if (stock != null)
            {
                product.Stock = stock.Stock;
                product.UnitType = stock.UnitType;
            }

            return product;
        }

    }

}
