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
using System.Data.SQLite;
using System.IO.Ports;
using System.Threading;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        SerialPort Serial;
        private delegate void SetText(string text);
        DatabaseService Database = new DatabaseService();

        public MainWindow()
        {
            InitializeComponent();
            PrepareForm();
            SQLiteConnection Connect = Database.Connect();
            Database.CreateTable(Connect);
            List<int> datas = new List<int>();
            for (int i = 0;i < ListBox_Scales.Items.Count; i++)
            {
                int data = Convert.ToInt32(ListBox_Scales.Items[i].ToString().Split(' ')[2].Replace("g",String.Empty));
                datas.Add(data);
            }
            Label_Times.Content = ListBox_Scales.Items.Count.ToString();
            Label_Min.Content = (datas.Count > 0) ? datas.Min().ToString() : 0.ToString();
            Label_Max.Content = (datas.Count > 0) ? datas.Max().ToString() : 0.ToString();
            Label_Average.Content = (datas.Count > 0) ? datas.Average().ToString("#.00") : "0.00";
        }

        private void PrepareForm()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            ComboBox1.Items.Clear();
            ListBox_Scales.Items.Clear();
            Label_Times.Content = "";
            Label_Min.Content = "";
            Label_Max.Content = "";
            Label_Average.Content = "";
            TextBoxLotID.Text = "Please Key Lot ID";
            TextBoxLotID.IsEnabled = true;
            ButtonAddLotID.IsEnabled = true;

            Label_LotID.Content = "";
            Label_No.Content = "";
            Label_Date.Content = "";
            ComboBoxUnit.SelectedItem = ComboBoxUnit.Items[0];
            Label_Weight.Content = "0000";
            ProgressBar1.Value = 0;

            Label_LotID2.Content = "Lot ID:";
            ListBoxData.Items.Clear();
        }

        public void SerialConnect()
        {
            Serial = new SerialPort("COM11", 9600, Parity.None, 8, StopBits.One);
            Serial.Handshake = Handshake.None;
            Serial.DataReceived += new SerialDataReceivedEventHandler(SerialRead);
            Serial.WriteTimeout = 500;
            Serial.Open();
        }

        private void SerialRead(object sender, SerialDataReceivedEventArgs e)
        {
            if (Serial.IsOpen)
            {
                Serial.Write("S\r\n");
                Thread.Sleep(300);
                string Message = Serial.ReadLine();
                Dispatcher.BeginInvoke(new SetText(si_DataReceived), new object[] { Message });
            }
        }

        private void si_DataReceived(string data)
        {
            if (data.Length > 0 && data.Trim() != "" && data.Trim() != "ZA")
            {
                Label_Weight.Content = data;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tare", "Click");
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Show Report Clicked", "Show Report");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click Zero", "Zero");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click Finish", "Finish");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ListBoxData.Items.Count > 0)
            {
                Label_No.Content = "1";
                ListBoxData.Items.Clear();
                MessageBox.Show("List Data Successfully Clear", "Clear Data");
            }
            else
            {
                MessageBox.Show("No Data to clear", "Clear Data");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PrepareForm();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (TextBoxLotID.Text.Trim() == "")
            {
                MessageBox.Show("Please Key Lot ID", "Empty Lot ID");
            }
            else if (TextBoxLotID.Text == "Please Key Lot ID") 
            {
                MessageBox.Show("Please Key Lot ID", "Empty Lot ID");
            }
            else
            {
                TextBoxLotID.IsEnabled = false;
                ButtonAddLotID.IsEnabled = false;
                Label_LotID.Content = TextBoxLotID.Text;
                Label_No.Content = "1";
                Label_Date.Content = DateTime.Now.ToString("dd/MM/yy");
                Label_Weight.Content = "0000";
                Label_LotID2.Content = "Lot ID: " + TextBoxLotID.Text;
                ListBoxData.Items.Clear();
                SerialConnect();
            }
        }

        private void TextBoxLotID_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxLotID.Text = "";
        }

        private void ButtonCreateDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Database successfully create", "Create Database");
        }

        private void ButtonCreateTable_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Table successfully create", "Create Table");
        }

        private void buttonCheckDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Connecttion Ok \nDatabase Name: DatabaseName.db \nTable Name: TableName","Connection Status");
        }
    }
}
