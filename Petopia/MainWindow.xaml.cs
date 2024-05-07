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
        public MainWindow()
        {
            InitializeComponent();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.PetopiaNewConnectionString);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var employeeLogin = from s in _petDB.Employees
                                where
                                    s.Employee_Username == UsernameTbx.Text
                                    && s.Employee_Password == PasswordTbx.Text

                                select s;

            foreach (var login in employeeLogin)
            {
                // Insert a new log entry
                Log newLog = new Log
                {
                    Logs_ID = Guid.NewGuid().ToString(), // Generate a unique ID for the log entry
                    Employee_ID = login.Employee_ID,
                    Log_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), // Current date and time
                    LogStatus_ID = "YourLogStatusIDHere" // You need to set the LogStatus_ID based on your logic
                };

                _petDB.Logs.InsertOnSubmit(newLog);
                _petDB.SubmitChanges();

                MessageBox.Show("Hacked the mainframe");
                SelectionPage selectionpage = new SelectionPage();
                selectionpage.Show();
                this.Close();
                return;
            }

            MessageBox.Show("Incorrect username or password");
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            Signup SignupWindow = new Signup();
            SignupWindow.Show();

        }
        private string GenerateNextEmployeeID()
        {
            // Retrieve the highest existing employee ID
            var maxEmployeeID = (from emp in _petDB.Employees
                                 select emp.Employee_ID).Max();

            // Extract the numeric part and increment
            int lastEmployeeNumber = int.Parse(maxEmployeeID.Substring(1));
            int nextEmployeeNumber = lastEmployeeNumber + 1;

            // Generate the new employee ID
            string nextEmployeeID = "E" + nextEmployeeNumber.ToString("D3"); // Assuming IDs are padded with zeros
            return nextEmployeeID;
        }

        private void PasswordTbx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

