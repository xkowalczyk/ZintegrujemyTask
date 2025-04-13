using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujemyTask.Models.CSVConvert
{
    public class ProductPriceCSV
    {
        [Index(1)]
        public string SKU { get; set; }
        [Index(4)]
        public string TAX { get; set; }
        [Index(5)]
        public string NettoPrice { get; set; }

    }
}
