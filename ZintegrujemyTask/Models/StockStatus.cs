using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujemyTask.Models
{
    public class StockStatus
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
        public string UnitType { get; set; }
    }
}
