using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCProShirts.Models
{
   public class Product
    {
        public string _Id { get; set; }
        public string Name { get; set; }
        public string PrintSize { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string SizeFormat { get; set; }
        public string Msrp { get; set; }
        public double Price { get; set; }
        public List<OColor> Colors { get; set; }

    }
}
