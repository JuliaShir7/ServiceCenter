using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DBExtends.Adapters;

namespace Курсовая_по_БДиЭС
{
    public static class DataModel
    {
        private static IDataAdapter db = new MySQLDataAdapter();
        public static ObservableCollection<Client> Clients { get; set; }
        public static ObservableCollection<Equipment> Equipment { get; set; }
        public static ObservableCollection<Worker> Workers { get; set; }
        public static ObservableCollection<Device> Devices { get; set; }
        public static ObservableCollection<Order> NewOrders { get; set; }
        public static ObservableCollection<Order> OrdersInProcess { get; set; }
        public static ObservableCollection<Order> OrdersFinished { get; set; }
        public static ObservableCollection<Position> Positions { get; set; }
        public static ObservableCollection<Service> Services { get; set; }
        static DataModel()
        {
            Clients = new ObservableCollection<Client>();
            Equipment = new ObservableCollection<Equipment>();
            Workers = new ObservableCollection<Worker>();
            Devices = new ObservableCollection<Device>();
            NewOrders = new ObservableCollection<Order>();
            OrdersInProcess = new ObservableCollection<Order>();
            OrdersFinished = new ObservableCollection<Order>();
            Positions = new ObservableCollection<Position>();
            Services = new ObservableCollection<Service>();
            db.Connect(new ConnectionSettings()
            {
                Host = "localhost",
                Port = "3306",
                DefaultSchema = "service",
                User = "root",
                Password = "23571788ysik",
                CharSet = "utf8"
            });
            //LoadClients();
            LoadClientsAndDevices();
            LoadEquipment();
            LoadWorkers();
            LoadDevices();
            LoadPositions();
            LoadNewOrders();
            LoadOrdersInProcess();
            LoadFinishedOrders();
            LoadServices();
        }
        //загрузка данных из БД
        static void LoadPositions()
        {
            string q = "select positions.ID, `Name`, Salary from `positions`;";
            List<Dictionary<string, string>> positions = db.GetQueryResult(q);
            Position newposition;
            foreach (Dictionary<string, string> p in positions)
            {
                newposition = new Position(long.Parse(p["ID"]), p["Name"], Convert.ToInt32(p["Salary"]));
                Positions.Add(newposition);
            }
        }
        static void LoadDevices()
        {
            string q = "select devices.ID, devices.Name, devices.ID_Client, devices.Manufacturer, devices.Model, devices.SeriaNum, devices.PurchaseDate, devices.WarrantyPeriod from `devices`;";
            List<Dictionary<string, string>> devices = db.GetQueryResult(q);
            foreach (Dictionary<string, string> device in devices)
            {
                Devices.Add(new Device(long.Parse(device["ID"]), device["Name"], device["Manufacturer"], device["Model"], device["SeriaNum"], Convert.ToDateTime(device["PurchaseDate"]).Date, int.Parse(device["WarrantyPeriod"])));
            }
        }
        static void LoadEquipment()
        {
            string q = "select `ID`, `Name`, `Manufacturer`, `Model`, `SeriaNum`, `PurchaseDate`, `WarrantyPeriod`, `Cost` from `equipment`;";
            List<Dictionary<string, string>> equipment = db.GetQueryResult(q);
            Equipment newequipment;
            foreach (Dictionary<string, string> eq in equipment)
            {
                newequipment = new Equipment(long.Parse(eq["ID"]), eq["Name"], eq["Manufacturer"], eq["Model"], eq["SeriaNum"], DateTime.Parse(eq["PurchaseDate"]), int.Parse(eq["WarrantyPeriod"]), float.Parse(eq["Cost"]));
                Equipment.Add(newequipment);
            }
        }
        static void LoadWorkers()
        {

            string q = "select workers.ID, `FullName`, positions.Name as Position, `StartWork`, `Birthday`, `Phone` from `workers` INNER JOIN positions ON positions.ID = workers.ID_Position;";
            List<Dictionary<string, string>> workers = db.GetQueryResult(q);
            Worker newWorker;
            foreach (Dictionary<string, string> w in workers)
            {
                newWorker = new Worker(long.Parse(w["ID"]), w["FullName"], w["Position"], Convert.ToDateTime(w["StartWork"]).Date, Convert.ToDateTime(w["Birthday"]).Date, w["Phone"]);
                Workers.Add(newWorker);
            }
        }
        static void LoadClientsAndDevices()
        {
            string q = "select `ID`, `FullName`, `Phone` from `clients`;";
            List<Dictionary<string, string>> clients = db.GetQueryResult(q);
            Client newclient;
            q = "select devices.ID, devices.Name, devices.ID_Client, devices.Manufacturer, devices.Model, devices.SeriaNum, devices.PurchaseDate, devices.WarrantyPeriod from `devices`;";
            List<Dictionary<string, string>> devices = db.GetQueryResult(q);
            foreach (Dictionary<string, string> c in clients)
            {
                List<Dictionary<string, string>> listOfDevices = devices.FindAll(d => d["ID_Client"] == c["ID"]);
                newclient = new Client(long.Parse(c["ID"]), c["FullName"], c["Phone"]);
                foreach (Dictionary<string, string> device in listOfDevices)
                {
                    newclient.Devices.Add(new Device(long.Parse(device["ID"]), device["Name"], device["Manufacturer"], device["Model"], device["SeriaNum"], Convert.ToDateTime(device["PurchaseDate"]).Date, int.Parse(device["WarrantyPeriod"])));
                }
                Clients.Add(newclient);
            }
        }
        static void LoadNewOrders()
        {
            string q = "SELECT distinct orders.ID, orders.ID_Device, services.`Name` as ServiceName, orders.ClientsDescription, orders.`Date` from `orders` INNER JOIN services ON services.ID=orders.ID_Service WHERE  orders.ID NOT IN (select `repair`.ID_Order from repair);";
            List<Dictionary<string, string>> orders = db.GetQueryResult(q);
            Order newOrder = null;
            foreach (Dictionary<string, string> o in orders)
            {
                foreach (Device device in Devices)
                {
                    if (long.Parse(o["ID_Device"]) == device.ID)
                        newOrder = new Order(long.Parse(o["ID"]), device, o["ServiceName"], o["ClientsDescription"], Convert.ToDateTime(o["Date"]).Date);
                }
                NewOrders.Add(newOrder);
            }
        }
        static void LoadOrdersInProcess()
        {
            string q = "select distinct orders.ID, orders.ID_Device, services.`Name` as ServiceName, repair.WorkDescription as `WorkDescription`, orders.`Date`, repair.ID_Worker from `repair` INNER Join `orders` ON repair.ID_Order=orders.ID INNER JOIN services ON services.ID=orders.ID_Service where `repair`.EndDate is null;";
            List<Dictionary<string, string>> orders = db.GetQueryResult(q);
            Order newOrder = null;
            foreach (Dictionary<string, string> o in orders)
            {
                foreach (Device device in Devices)
                {
                    foreach (Worker worker in Workers)
                    {
                        if (long.Parse(o["ID_Worker"]) == worker.ID)
                            newOrder = new Order(long.Parse(o["ID"]), device, o["ServiceName"], o["WorkDescription"], Convert.ToDateTime(o["Date"]).Date, worker);
                    }
                }
                OrdersInProcess.Add(newOrder);
            }
        }
        static void LoadFinishedOrders()
        {
            string q = "select distinct orders.ID, orders.ID_Device, services.`Name` as ServiceName, orders.ClientsDescription, orders.`Date`, repair.WorkDescription as WorkDescription, repair.ID_Worker, repair.EndDate from `repair` INNER Join `orders` ON repair.ID_Order=orders.ID INNER JOIN services ON services.ID=orders.ID_Service where `repair`.EndDate is not null;";
            List<Dictionary<string, string>> orders = db.GetQueryResult(q);
            Order newOrder = null;
            foreach (Dictionary<string, string> o in orders)
            {
                foreach (Device device in Devices)
                {
                    foreach (Worker worker in Workers)
                    {
                        if (long.Parse(o["ID_Worker"]) == worker.ID)
                            newOrder = new Order(long.Parse(o["ID"]), device, o["ServiceName"], o["ClientsDescription"], Convert.ToDateTime(o["Date"]).Date, o["WorkDescription"], worker, Convert.ToDateTime(o["EndDate"]).Date);
                    }
                }
                OrdersFinished.Add(newOrder);
            }
        }
        static void LoadServices()
        {
            string q = "select `ID`, `Name`, `Price` from `services`;";
            List<Dictionary<string, string>> services = db.GetQueryResult(q);
            Service newservice;
            foreach (Dictionary<string, string> s in services)
            {
                newservice = new Service(long.Parse(s["ID"]),s["Name"], float.Parse(s["Price"]));
                Services.Add(newservice);
            }
        }
        //работаем с оборудованием сервисного центра
        public static void AddEquipment(string name, string manufacturer, string model, string seriaNum, DateTime purchaseDate, int warrantyPeriod, float cost)
        {
            string pD = ConvertDate(purchaseDate);
            long id = db.InsertRow("equipment", new Dictionary<string, object> { { "Name", name }, { "Manufacturer", manufacturer }, { "Model", model }, { "SeriaNum", seriaNum }, { "PurchaseDate", pD }, { "WarrantyPeriod", warrantyPeriod }, { "Cost", cost } });
            Equipment.Add(new Equipment(id, name, manufacturer, model, seriaNum, purchaseDate, warrantyPeriod, cost));
            //`ID`, `Name`, `Manufacturer`, `Model`, `SeriaNum`, `PurchaseDate`, `WarrantyPeriod`, `Cost`
        }
        public static void DeleteEquipment(Equipment equipment)
        {
            db.DeleteRow("equipment", equipment.ID);
            Equipment.Remove(equipment);
        }
        public static void UpdateEquipment(Equipment eq, string name, string manufacturer, string model, string seriaNum, DateTime purchaseDate, int warrantyPeriod, float cost)
        {
            string pD = ConvertDate(purchaseDate);
            db.UpdateRow("equipment", eq.ID, new Dictionary<string, object> { { "Name", name }, { "Manufacturer", manufacturer }, { "Model", model }, { "SeriaNum", seriaNum }, { "PurchaseDate", pD }, { "WarrantyPeriod", warrantyPeriod }, { "Cost", cost } });
            Equipment.Remove(eq);
            Equipment.Add(new Equipment(eq.ID, name, manufacturer, model, seriaNum, purchaseDate, warrantyPeriod, cost));
        }
        //работа с клиентами
        public static void AddClient(string fullname, string phone)
        {
            long id = db.InsertRow("clients", new Dictionary<string, object> { { "FullName", fullname }, { "Phone", phone } });
            Clients.Add(new Client(id, fullname, phone));
        }
        public static void DeleteClient(Client client)
        {
            db.DeleteRow("clients", client.ID);
            Clients.Remove(client);
        }
        public static void UpdateClient(Client c, string fullname, string phone)
        {
            db.UpdateRow("clients", c.ID, new Dictionary<string, object> { { "FullName", fullname }, { "Phone", phone } });
            Clients.Remove(c);
            Clients.Add(new Client(c.ID, fullname, phone));
        }
        //операции с устройствами в ремонте
        public static void AddDevice(string name, string manufacturer, string model, string seriaNum, DateTime purchaseDate, int warrantyPeriod, long id_client)
        {
            string pD = ConvertDate(purchaseDate);
            long id = db.InsertRow("devices", new Dictionary<string, object> { { "Name", name }, { "Manufacturer", manufacturer }, { "Model", model }, { "SeriaNum", seriaNum }, { "PurchaseDate", pD }, { "WarrantyPeriod", warrantyPeriod }, { "ID_Client", id_client } });
            Devices.Add(new Device(id, name, manufacturer, model, seriaNum, purchaseDate, warrantyPeriod));
            Clients.Clear();
            LoadClientsAndDevices();
        }
        public static void DeleteDevice(Device device)
        {
            db.DeleteRow("devices", device.ID);
            Devices.Remove(device);
            Clients.Clear();
            LoadClientsAndDevices();
        }
        public static void UpdateDevice(Device d, string name, string manufacturer, string model, string seriaNum, DateTime purchaseDate, int warrantyPeriod)
        {
            string pD = ConvertDate(purchaseDate);
            db.UpdateRow("devices", d.ID, new Dictionary<string, object> { { "Name", name }, { "Manufacturer", manufacturer }, { "Model", model }, { "SeriaNum", seriaNum }, { "PurchaseDate", pD }, { "WarrantyPeriod", warrantyPeriod } });
            Devices.Remove(d);
            Devices.Add(new Device(d.ID, name, manufacturer, model, seriaNum, purchaseDate, warrantyPeriod));
        }
        private static string ConvertDate(DateTime date)
        {
            string d = date.Year + "-" + date.Month + "-" + date.Day;
            return d;
        }
        //операции с сотрудниками
        static int ReturnCurrentPositionID(List<Dictionary<string, string>> positions, string position)
        {
            int cur = -1;
            foreach (Dictionary<string, string> pos in positions)
            {
                if (pos["Name"] == position)
                {
                    cur = Convert.ToInt32(pos["ID"]);
                }
            }
            return cur;
        }
        public static void AddWorker(string fullname, string position, DateTime startWork, DateTime birthday, string phone)
        {
            string q = $"select positions.ID as `ID`, positions.Name from positions;";
            List<Dictionary<string, string>> positions = db.GetQueryResult(q);
            int p = ReturnCurrentPositionID(positions, position);
            string sw = ConvertDate(startWork);
            string b = ConvertDate(birthday);
            long id = db.InsertRow("workers", new Dictionary<string, object> { { "FullName", fullname }, { "Position", p }, { "StartWork", sw }, { "Birthday", b }, { "Phone", phone } });
            Workers.Add(new Worker(id, fullname, position, startWork, birthday, phone));
        }
        public static void DeleteWorker(Worker worker)
        {
            db.DeleteRow("workers", worker.ID);
            Workers.Remove(worker);
        }
        public static void UpdateWorker(Worker w, string fullname, string position, DateTime startWork, DateTime birthday, string phone)
        {
            string q = $"select positions.ID as `ID`, positions.Name from positions;";
            List<Dictionary<string, string>> positions = db.GetQueryResult(q);
            int p = ReturnCurrentPositionID(positions, position);
            string sw = ConvertDate(startWork);
            string b = ConvertDate(birthday);
            db.UpdateRow("workers", w.ID, new Dictionary<string, object> { { "FullName", fullname }, { "Position", p }, { "StartWork", sw }, { "Birthday", b }, { "Phone", phone } });
            Workers.Remove(w);
            Workers.Add(new Worker(w.ID, fullname, position, startWork, birthday, phone));
        }
        public static void AddPosition(string name, int salary)
        {
            long id = db.InsertRow("positions", new Dictionary<string, object> { { "Name", name }, { "Salary", salary } });
            //Workers.Clear();
            Positions.Add(new Position(id, name, salary));
        }
        public static void DeletePosition(Position position)
        {
            db.DeleteRow("positions", position.ID);
            Positions.Remove(position);
        }
        public static void UpdatePosition(Position p, string name, int salary)
        {

            db.UpdateRow("positions", p.ID, new Dictionary<string, object> { { "Name", name }, { "Salary", salary } });
            Positions.Remove(p);
            Positions.Add(new Position(p.ID, name, salary));
        }
        //новые заказы
        public static void AddNewOrder(Device device, Service service, string clientsDescription, DateTime date)
        {
            long id = db.InsertRow("orders", new Dictionary<string, object> { { "ID_Device", device.ID }, { "ID_Service", service.ID }, { "ClientsDescription", clientsDescription }, { "Date", ConvertDate(date) } });
            //Workers.Clear();
            NewOrders.Add(new Order(id, device, service.Name, clientsDescription, date));
        }
        public static void DeleteNewOrder(Order order)
        {
            db.DeleteRow("orders", order.ID);
            NewOrders.Remove(order);
        }
        public static void UpdateNewOrder(Order order, Device device, string service, string clientsDescription, DateTime date)
        {
            long sId = -1;
            foreach (Service s in Services)
            {
                if (s.Name == service)
                {
                    sId = s.ID;
                    break;
                }
            }
            db.UpdateRow("orders", order.ID, new Dictionary<string, object> { { "ID_Device", device.ID }, { "ID_Service", sId }, { "ClientsDescription", clientsDescription }, { "Date", ConvertDate(date) } });
            NewOrders.Remove(order);
            NewOrders.Add(new Order(order.ID, device, service, clientsDescription, date));
        }
        //выполняемые заказы
        public static void AddOrderInProcess(Device device, string service, string workDescription, DateTime date, Worker worker)
        {
            long sId = -1;
            foreach (Service s in Services)
            {
                if (s.Name == service)
                {
                    sId = s.ID;
                    break;
                }
            }
            long id = db.InsertRow("orders", new Dictionary<string, object> { { "ID_Device", device.ID }, { "ID_Service", sId }, { "WorkDescription", workDescription }, { "Date", ConvertDate(date) }, { "ID_Worker", worker.ID } });
            //Workers.Clear();
            OrdersInProcess.Add(new Order(id, device, service, workDescription, date, worker));
        }
        public static void DeleteOrderInProcess(Order order)
        {
            db.DeleteRow("orders", order.ID);
            OrdersInProcess.Remove(order);
        }
        public static void UpdateOrderInProcess(Order order, Device device, string service, string workDescription, DateTime date, Worker worker)
        {
            db.UpdateRow("orders", order.ID, new Dictionary<string, object> { { "ID_Device", device.ID }, { "Service", service }, { "WorkDescription", workDescription }, { "Date", ConvertDate(date) }, { "Worker", worker.ID } });
            OrdersInProcess.Remove(order);
            OrdersInProcess.Add(new Order(order.ID, device, service, workDescription, date, worker));
        }

        //предоставляемые услуги
        public static void AddService(string name, float price)
        {
            long id = db.InsertRow("services", new Dictionary<string, object> { { "Name", name }, { "Price", price } });
            //Workers.Clear();
            Services.Add(new Service(id, name, price));
        }
        public static void DeleteService(Service service)
        {
            db.DeleteRow("services", service.ID);
            Services.Remove(service);
        }
        public static void UpdateService(Service s, string name, float price)
        {
            db.UpdateRow("services", s.ID, new Dictionary<string, object> { { "Name", name }, { "LowDifficultyPrice", price } });
            Services.Remove(s);
            Services.Add(new Service(s.ID, name, price));
        }

    }
}
