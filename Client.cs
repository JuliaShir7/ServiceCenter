using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_по_БДиЭС
{
    public class Client
    {
        public long ID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public Client(long iD, string fullname, string phone)
        {
            ID = iD;
            FullName = fullname;
            Phone = phone;
            Devices = new ObservableCollection<Device>();
        }
    }
}
