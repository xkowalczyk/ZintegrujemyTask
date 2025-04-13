namespace ZintegrujemyTask.Models
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public string IsVire { get; set; }
        public string Shipping { get; set; }
        public string IsAvailable { get; set; }
        public string IsVendor { get; set; }
        public string DefaultImage { get; set; }
        public int TAX { get; set; }
        public float NettoPrice { get; set; }
        public int Stock { get; set; }
        public string UnitType { get; set; }
    }

}
