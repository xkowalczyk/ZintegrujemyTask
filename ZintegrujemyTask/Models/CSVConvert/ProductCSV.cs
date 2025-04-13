using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujemyTask.Models.CSVConvert
{
    public class ProductCSV
    {
        [Index(0)]
        public string Id { get; set; }
        [Index(1)]
        public string SKU { get; set; }
        [Index(2)]
        public string Name { get; set; }
        [Index(4)]
        public string EAN { get; set; }
        [Index(6)]
        public string ProducerName { get; set; }
        [Index(7)]
        public string Category { get; set; }
        [Index(8)]
        public string IsVire { get; set; }
        [Index(9)]
        public string Shipping { get; set; }
        [Index(11)]
        public string IsAvailable { get; set; }
        [Index(16)]
        public string IsVendor { get; set; }
        [Index(18)]
        public string DefaultImage { get; set; }
    }
}
