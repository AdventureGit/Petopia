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

namespace Petopia
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        private PetopiaLinkerDataContext _petDB;

        public class LogItem
        {
            public string EmployeeName { get; set; }
            public string LogTime { get; set; }
            public string LogStatusItem { get; set; }
            //public string Time { get; set; }
        }
        public Logs()
        {
            InitializeComponent();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.Petopia_UpdatedConnectionString);
            LoadListView();
        }
        
        private void LoadListView()
        {
            foreach (var item in _petDB.Logs)
            {
                var leaderboardItem = new LogItem()
                {
                    EmployeeName = item.Employee_ID.ToString(),
                    LogTime = item.Log_Time.ToString(),
                    LogStatusItem = item.LogStatus_ID.ToString(),

                };
                
                Logs_ListView.Items.Add(leaderboardItem);
            }
        }
    }
}


 