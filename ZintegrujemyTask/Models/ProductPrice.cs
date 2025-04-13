using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujemyTask.Models
{
    public class ProductPrice
    {
        public string SKU { get; set; }
        public int TAX { get; set; }
        public float NettoPrice { get; set; }
    }
}
