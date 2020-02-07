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
using System.Windows.Threading;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using Microsoft.Win32;

namespace book
{
    public partial class storekeeper : Window
    {
        public storekeeper()
        {
            InitializeComponent();
            timerStart();
            Load();
        }

        private DispatcherTimer timer = null;
        bool enable = true;
        private void timerStart()
        {
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(10000000);//секунда
            if (enable) { timer.Start(); }
        }

        private void Load()
        {
            try
            {
                Bas.Connect.Open();
                string query = "SELECT name_provider, surname_provider, patronymic_provider , address ,number ,quantity ,receipt_date, id  FROM deliveries";
                MySqlCommand cmd = new MySqlCommand(query, Bas.Connect);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                Spisok_DataGrid.AutoGenerateColumns = true;
                Spisok_DataGrid.ItemsSource = dt.DefaultView;
                //Bas.Connect.Close();

                MySqlDataReader Date = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Bas.Connect.Close();
            }
        }

        //int f = 0;
        private void timerTick(object sender, EventArgs e)
        {
            //f++;
            //Time_labl.Content = f.ToString();
            Time_labl.Content = DateTime.Now.ToString();
        }

        //string id,
        private void Spisok_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (DataRowView Row in Spisok_DataGrid.SelectedItems)
                {
                    name_textbox.Text = Row.Row.ItemArray[0].ToString();
                    famil_textbox.Text = Row.Row.ItemArray[1].ToString();
                    otch_textbox.Text = Row.Row.ItemArray[2].ToString();
                    adres_textbox.Text = Row.Row.ItemArray[3].ToString();
                    number_textbox.Text = Row.Row.ItemArray[4].ToString();
                    colichestvo_textbox.Text = Row.Row.ItemArray[6].ToString();
                    stoimost_textbox.Text = Row.Row.ItemArray[7].ToString();
                    nametovar_textbox.Text = Row.Row.ItemArray[8].ToString();
                }
                //TextImage = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void delete_but_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bas.Connect.Open();
                DataRowView drv = Spisok_DataGrid.SelectedItem as DataRowView;
                string text = drv[1].ToString();
                string query = $"UPDATE workers SET Role = 'Удален' WHERE Name = '{text}'";
                MySqlCommand cmd = new MySqlCommand(query, Bas.Connect);
                MySqlDataReader reader = cmd.ExecuteReader();
                Bas.Connect.Close();
                Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void but_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
