using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_по_БДиЭС
{
    public class Worker
    {
        public long ID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }

        public Worker(long iD, string fullName, string position, DateTime startWork, DateTime birthday, string phone)
        {
            ID = iD;
            FullName = fullName;
            Position = position;
            StartWork = startWork;
            Birthday = birthday;
            Phone = phone;
        }
    }
    public class Position
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }

        public Position(long iD, string name, int salary)
        {
            ID = iD;
            Name = name;
            Salary = salary;
        }
    }
}
