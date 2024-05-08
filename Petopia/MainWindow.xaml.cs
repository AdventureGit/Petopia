
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

namespace Petopia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>  
    public partial class MainWindow : Window
    {
        PetopiaLinkerDataContext _petDB = null;
        bool loginSuccessful = false;
        string loggedInEmployeeID;
        public MainWindow()
        {
            InitializeComponent();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.Petopia_UpdatedConnectionString);

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (loginSuccessful)
            {
                var employeeLogin = from s in _petDB.Employees
                                    where s.Employee_Username == UsernameTbx.Text
                                        && s.Employee_Password == PasswordTbx.Text
                                    select s;

                foreach (var login in employeeLogin)
                {
                    Log offlineLog = _petDB.Logs.FirstOrDefault(log => log.Employee_ID == login.Employee_ID && log.LogStatus_ID == "Active");
                    if (offlineLog != null)
                    {
                        offlineLog.LogStatus_ID = "Offline";
                        _petDB.SubmitChanges();
                    }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var employeeLogin = from s in _petDB.Employees
                                where s.Employee_Username == UsernameTbx.Text
                                    && s.Employee_Password == PasswordTbx.Text
                                select s;

            foreach (var login in employeeLogin)
            {
                Log newLog = new Log
                {
                    Logs_ID = GenerateNextLogID(),
                    Employee_ID = login.Employee_ID,
                    Log_Time = DateTime.Now,
                    LogStatus_ID = "Active"
                };

                _petDB.Logs.InsertOnSubmit(newLog);
                _petDB.SubmitChanges();

      
                loggedInEmployeeID = login.Employee_ID;

                MessageBox.Show("Hacked the mainframe");
                SelectionPage selectionPage = new SelectionPage(loggedInEmployeeID);
                selectionPage.Show();


                this.Close();

                loginSuccessful = true;
            }

            if (!loginSuccessful)
            {
                MessageBox.Show("Incorrect username or password");
            }
        }
        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            Signup SignupWindow = new Signup();
            SignupWindow.Show();
        }

        private string GenerateNextLogID()
        {
            var maxLogID = (from log in _petDB.Logs
                            select log.Logs_ID).Max();

            if (maxLogID != null)
            {
                int lastLogNumber = int.Parse(maxLogID.Substring(1));
                int nextLogNumber = lastLogNumber + 1;

                // Generate the new log ID
                string nextLogID = "L" + nextLogNumber.ToString("D3"); 
                return nextLogID;
            }
            else
            {
                return "L001";
            }
        }

        private void PasswordTbx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private string GetEmployeeID()
        {
         
            return loggedInEmployeeID;
        }
    }
}
