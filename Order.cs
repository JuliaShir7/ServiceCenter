using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_по_БДиЭС
{
    public class Order
    {
        public long ID { get; set; }
        public Device Device { get; set; }
        public string Service { get; set; }
        public string ClientsDescription { get; set; }
        public DateTime Date { get; set; }
        public string WorkDescription { get; set; }
        public Worker Worker { get; set; }
        public DateTime EndDate { get; set; }

        public Order(long iD, Device device, string service, string clientsDescription, DateTime date)
        {
            ID = iD;
            Device = device;
            Service = service;
            ClientsDescription = clientsDescription;
            Date = date;
        }
        public Order(long iD, Device device, string service, string workDescription, DateTime date, Worker worker)
        {
            ID = iD;
            Device = device;
            Service = service;
            WorkDescription = workDescription;
            Date = date;
            Worker = worker;
        }
        public Order(long iD, Device device, string service, string clientsDescription, DateTime date, string workDescription, Worker worker, DateTime endDate)
        {
            ID = iD;
            Device = device;
            Service = service;
            ClientsDescription = clientsDescription;
            Date = date;
            WorkDescription = workDescription;
            Worker = worker;
            EndDate = endDate;
        }
    }
}
