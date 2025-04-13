using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujemyTask.Models.CSVConvert
{
    public class StockStatusCSV
    {
        [Index(0)]
        public string ProductId { get; set; }
        [Index(3)]
        public string Stock { get; set; }
        [Index(6)]
        public string Shipping {  get; set; }
        [Index(2)]
        public string UnitType { get; set; }
    }
}
