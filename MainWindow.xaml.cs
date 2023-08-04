using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсовая_по_БДиЭС
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //DataModel.Clients.Clear();
        }

        private void Add_Equipment_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(Equipment_DataGrid);
            add.Show();
        }

        private void Delete_Equipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment eq = Equipment_DataGrid.SelectedItem as Equipment;
            if (eq == null) return;
            DataModel.DeleteEquipment(eq);
            MessageBox.Show("Удалено");
        }

        private void Update_Equipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment eq = Equipment_DataGrid.SelectedItem as Equipment;
            if (eq == null) return;
            DataModel.UpdateEquipment(eq, eq.Name, eq.Manufacturer, eq.Model, eq.SeriaNum, Convert.ToDateTime(eq.PurchaseDate).Date, Convert.ToInt32(eq.WarrantyPeriod), eq.Cost);
            MessageBox.Show("Данные изменены");
        }

        private void Add_Client_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(Clients_DataGrid);
            add.Show();
        }

        private void Delete_Client_Click(object sender, RoutedEventArgs e)
        {
            Client c = Clients_DataGrid.SelectedItem as Client;
            if (c == null) return;
            DataModel.DeleteClient(c);
            MessageBox.Show("Удалено");
        }

        private void Update_Client_Click(object sender, RoutedEventArgs e)
        {
            Client c = Clients_DataGrid.SelectedItem as Client;
            if (c == null) return;
            DataModel.UpdateClient(c, c.FullName, c.Phone);
            MessageBox.Show("Данные изменены");

        }

        private void Add_Device_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(Devices_DataGrid);
            add.Show();
        }

        private void Delete_Device_Click(object sender, RoutedEventArgs e)
        {
            Device d = Devices_DataGrid.SelectedItem as Device;
            if (d == null) return;
            DataModel.DeleteDevice(d);
            MessageBox.Show("Удалено");
        }

        private void Update_Device_Click(object sender, RoutedEventArgs e)
        {
            Device d = Devices_DataGrid.SelectedItem as Device;
            if (d == null) return;
            DataModel.UpdateDevice(d, d.Name, d.Manufacturer, d.Model, d.SeriaNum, Convert.ToDateTime(d.PurchaseDate).Date, Convert.ToInt32(d.WarrantyPeriod));
            MessageBox.Show("Данные изменены");
        }

        private void Add_Worker_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(Workers_DataGrid);
            add.Show();
        }

        private void Delete_Worker_Click(object sender, RoutedEventArgs e)
        {
            Worker w = Workers_DataGrid.SelectedItem as Worker;
            if (w == null) return;
            DataModel.DeleteWorker(w);
            MessageBox.Show("Удалено");
        }

        private void Edit_Worker_Click(object sender, RoutedEventArgs e)
        {
            Worker w = Workers_DataGrid.SelectedItem as Worker;
            if (w == null) return;
            DataModel.UpdateWorker(w, w.FullName, w.Position, Convert.ToDateTime(w.StartWork).Date, Convert.ToDateTime(w.Birthday).Date, w.Phone);
            MessageBox.Show("Данные изменены");
        }
        private void Add_Postion_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(Positions_DataGrid);
            add.Show();
        }

        private void Delete_Postion_Click(object sender, RoutedEventArgs e)
        {
            Position p = Positions_DataGrid.SelectedItem as Position;
            if (p == null) return;
            DataModel.DeletePosition(p);
            MessageBox.Show("Удалено");
        }

        private void Edit_Postion_Click(object sender, RoutedEventArgs e)
        {
            Position p = Positions_DataGrid.SelectedItem as Position;
            if (p == null) return;
            DataModel.UpdatePosition(p, p.Name, p.Salary);
            MessageBox.Show("Данные изменены");
        }
        private void Add_Service_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(Services_DataGrid);
            add.Show();
        }

        private void Delete_Service_Click(object sender, RoutedEventArgs e)
        {
            Service s = Services_DataGrid.SelectedItem as Service;
            if (s == null) return;
            DataModel.DeleteService(s);
            MessageBox.Show("Удалено");
        }

        private void Update_Service_Click(object sender, RoutedEventArgs e)
        {
            Service s = Services_DataGrid.SelectedItem as Service;
            if (s == null) return;
            DataModel.UpdateService(s, s.Name, s.Price);
            MessageBox.Show("Данные изменены");
        }
        //работа с базой знаний

        private void GoToKnowledgebase_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersNew_DataGrid.SelectedItem as Order;
            //Diagnostics d = new Diagnostics(order);
            //d.Show();
        }
        //работа с заказами
        private void Add_Order_Click(object sender, RoutedEventArgs e)
        {
            AddRow add = new AddRow(OrdersNew_DataGrid);
            add.Show();
        }

        private void Delete_Order_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersNew_DataGrid.SelectedItem as Order;
            if (order == null) return;
            DataModel.DeleteNewOrder(order);
            MessageBox.Show("Удалено");
        }

        private void Edit_Order_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersNew_DataGrid.SelectedItem as Order;
            if (order == null) return;
            DataModel.UpdateNewOrder(order, order.Device, order.Service, order.ClientsDescription, order.Date);
            MessageBox.Show("Данные изменены");
        }

        private void add_orderInpr_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void delete_orderInpr_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrdersInProcess_DataGrid.SelectedItem as Order;
            if (order == null) return;
            DataModel.DeleteOrderInProcess(order);
            MessageBox.Show("Удалено");
        }

        private void update_orderInpr_Click(object sender, RoutedEventArgs e)
        {
            //DataModel.UpdateOrderInProcess()
            MessageBox.Show("Данные изменены");
        }

        private void ToFinished_Click(object sender, RoutedEventArgs e)
        {
            //AddEndDate addEndDate = new AddEndDate();
            //addEndDate.Show();
        }

    }
   
}
