using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_по_БДиЭС
{
    public class Device
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long ID_Client { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SeriaNum { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int WarrantyPeriod { get; set; }


        public Device(long iD, string name, string manufacturer, string model, string seriaNum, DateTime purchaseDate, int warrantyPeriod)
        {
            ID = iD;
            Name = name;
            Manufacturer = manufacturer;
            Model = model;
            SeriaNum = seriaNum;
            PurchaseDate = purchaseDate;
            WarrantyPeriod = warrantyPeriod;
        }
    }
}
