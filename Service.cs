using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_по_БДиЭС
{
    public class Service
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public Service(long iD, string name, float price)
        {
            ID = iD;
            Name = name;
            Price = price;

        }
    }
}
