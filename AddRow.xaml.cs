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
using System.Windows.Shapes;

namespace Курсовая_по_БДиЭС
{
    /// <summary>
    /// Логика взаимодействия для AddRow.xaml
    /// </summary>
    public partial class AddRow : Window
    {
        DataGrid dg = new DataGrid();
        public AddRow(DataGrid dataGrid)
        {
            InitializeComponent();
            dg = dataGrid;
            ChooseGrid();
        }
        private void ChooseGrid()
        {
            foreach (object o in mainView.Children)
            {
                if (o is Grid)
                {
                    if (dg.Name.Contains((o as Grid).Name))
                    {
                        (o as Grid).Visibility = Visibility.Visible;
                    }
                    else
                    {
                        (o as Grid).Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private void ClearData()
        {
            foreach (object g in mainView.Children)
            {
                if (g is Grid)
                {
                    foreach (object st in (g as Grid).Children)
                    {
                        if (st is StackPanel)
                        {
                            foreach (object obj in (st as StackPanel).Children)
                                if (obj is TextBox)
                                    (obj as TextBox).Text = "";
                                else
                                    if (obj is ComboBox)
                                    (obj as ComboBox).Text = "";
                        }
                    }
                }

            }
        }
        private int Choose(DataGrid grid)
        {
            int choice = 0;
            if (grid.Name == "Clients_DataGrid")
                return choice = 0;
            else
            {
                if (grid.Name == "Devices_DataGrid")
                    return choice = 1;
                else
                {
                    if (grid.Name == "Equipment_DataGrid")
                        return choice = 2;
                    else
                    {
                        if (grid.Name == "Workers_DataGrid")
                        {
                            return choice = 3;
                        }
                        else
                        {
                            if (grid.Name == "OrdersNew_DataGrid")
                                return choice = 4;
                            else
                            {
                                if (grid.Name == "Positions_DataGrid")
                                    return choice = 7;
                                else
                                {
                                    if (grid.Name == "Services_DataGrid")
                                        return choice = 8;
                                }
                            }

                        }
                    }
                }

                return choice;
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int choice = Choose(dg);
            switch (choice)
            {
                case 0:
                    {
                        DataModel.AddClient(client_fullname.Text, client_phone.Text);
                        break;
                    }
                case 1:
                    {
                        Client c = client_forDevice.SelectedItem as Client;
                        DataModel.AddDevice(d_name.Text, d_manufacturer.Text, d_model.Text, d_seriaNum.Text, Convert.ToDateTime(d_purchaseDate.Text).Date, Convert.ToInt32(d_warrantyPeriod.Text), c.ID);
                        break;
                    }

                case 2:
                    {
                        DataModel.AddEquipment(eq_name.Text, eq_manufacturer.Text, eq_model.Text, eq_seriaNum.Text, Convert.ToDateTime(eq_purchaseDate.Text).Date, Convert.ToInt32(eq_warrantyPeriod.Text), float.Parse(eq_cost.Text));
                        break;
                    }
                case 3:
                    {
                        DataModel.AddWorker(w_fullname.Text, w_position.Text, Convert.ToDateTime(w_startWork.Text).Date, Convert.ToDateTime(w_birthday.Text).Date, w_phone.Text);
                        break;
                    }
                case 4:
                    {
                        Device d = neworder_device.SelectedItem as Device;
                        DataModel.AddNewOrder(d, (order_service.SelectedItem as Service), order_clientDescription.Text, Convert.ToDateTime(order_date.Text).Date);
                        break;
                    }
                case 5:
                    {
                        //DataModel.AddNewOrder((order_device.SelectedItem as Device), order_service.Text, order_clientDescription.Text, Convert.ToDateTime(order_date).Date);
                        break;
                    }
                case 6:
                    {
                        //DataModel.AddNewOrder((order_device.SelectedItem as Device), order_service.Text, order_clientDescription.Text, Convert.ToDateTime(order_date).Date);
                        break;
                    }
                case 7:
                    {
                        DataModel.AddPosition(p_name.Text, Convert.ToInt32(p_salary.Text));
                        break;
                    }
                case 8:
                    {
                        DataModel.AddService(s_name.Text, float.Parse(price.Text));
                        break;
                    }
            }
            MessageBox.Show("Данные внесены!");
            ClearData();
        }
    }
}
