using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujemyTask.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public string EAN { get; set; }
        public string ProducerName { get; set; }
        public string Category { get; set; }
        public bool IsVire { get; set; }
        public string Shipping { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsVendor { get; set; }
        public string DefaultImage { get; set; }
    }
}
