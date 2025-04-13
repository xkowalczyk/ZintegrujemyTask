using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http;
using ZintegrujemyTask.Data;
using ZintegrujemyTask.Models;
using ZintegrujemyTask.Models.CSVConvert;
namespace ZintegrujemyTask.Services
{
    public class CsvService
    {
        private readonly DapperContext _context;
        private readonly HttpClient _httpClient;

        public CsvService(DapperContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<Product>> GetProductsAsync(string filePath)
        {
            List<Product> products = new List<Product>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
                IgnoreBlankLines = true,
                BadDataFound = null,
                MissingFieldFound = null,
            };
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<ProductCSV>();

                    foreach (var record in records)
                    {
                        if (record.Shipping == "24h" && !record.Category.Contains("Kable i przewody") && record.Id != "__empty_line__")
                        {
                            try
                            {
                                Product toSave = new Product()
                                {
                                    Id = record.Id,
                                    SKU = record.SKU,
                                    Name = record.Name,
                                    EAN = record.EAN,
                                    ProducerName = record.ProducerName,
                                    Category = record.Category,
                                    IsVire = int.TryParse(record.IsVire, out int isVireParsed)
                                    ? Convert.ToBoolean(isVireParsed)
                                    : false,
                                    IsAvailable = int.TryParse(record.IsAvailable, out int isAvailableParsed)
                                    ? Convert.ToBoolean(isAvailableParsed)
                                    : false,
                                    IsVendor = int.TryParse(record.IsVendor, out int isVendorParsed)
                                    ? Convert.ToBoolean(isVendorParsed)
                                    : false,
                                    Shipping = record.Shipping,
                                    DefaultImage = record.DefaultImage,
                                };
                                products.Add(toSave);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas konwersji csv: " + ex.Message);
            }
            return products;
        }

        public async Task<List<StockStatus>> GetStockStatusAsync(string filePath)
        {
            List<StockStatus> Stocks = new List<StockStatus>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                IgnoreBlankLines = true,
                BadDataFound = null,
                MissingFieldFound = null,
            };
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<StockStatusCSV>();

                    foreach (var record in records)
                    {
                        if (record.Shipping == "24h")
                        {
                            try
                            {
                                StockStatus toSave = new StockStatus()
                                {
                                    ProductId = record.ProductId,
                                    Stock = double.TryParse(record.Stock, NumberStyles.Any, CultureInfo.InvariantCulture, out double stockParsed)
                                    ? Convert.ToInt32(stockParsed)
                                    : 0,
                                    UnitType = record.UnitType
                                };

                                Stocks.Add(toSave);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas konwersji csv: " + ex.Message);
            }

            return Stocks;
        }

        public async Task<List<ProductPrice>> GetProductPriceAsync(string filepath)
        {
            List<ProductPrice> Prices = new List<ProductPrice>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                IgnoreBlankLines = true,
                BadDataFound = null,
                MissingFieldFound = null,
            };
            try
            {
                using (var reader = new StreamReader("Prices.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<ProductPriceCSV>();

                    foreach (var record in records)
                    {
                        try
                        {
                            ProductPrice toSave = new ProductPrice()
                            {
                                SKU = record.SKU,
                                NettoPrice = float.TryParse(record.NettoPrice, NumberStyles.Any, CultureInfo.InvariantCulture, out float nettoPrice)
                                ? nettoPrice
                                : 0f,
                                TAX = double.TryParse(record.TAX, NumberStyles.Any, CultureInfo.InvariantCulture, out double taxParsed)
                                ? Convert.ToInt32(taxParsed)
                                : 0
                            };

                            Prices.Add(toSave);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas konwersji csv: " + ex.Message);
            }
            return Prices;
        }

        public async Task<ProductBatchDto> GetProductBatchAsync()
        {
            string[] urls = {
                "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv",
                "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv",
                "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv"
            };

            string[] localFiles = {
                "Products.csv", "Inventory.csv", "Prices.csv"
            };
            /*
            for (int i = 0; i < urls.Length; i++)
            {
                var data = await _httpClient.GetByteArrayAsync(urls[i]);
                await File.WriteAllBytesAsync(localFiles[i], data);
            }
            */
            List<Product> ProductList = await GetProductsAsync(localFiles[0]);
            List<ProductPrice> PriceList = await GetProductPriceAsync(localFiles[2]);
            List<StockStatus> StockList = await GetStockStatusAsync(localFiles[1]);

            ProductBatchDto productBatch = new ProductBatchDto()
            {
                Products = ProductList,
                Pricings = PriceList,
                Stocks = StockList
            };

            return productBatch;
        }
    }

}
