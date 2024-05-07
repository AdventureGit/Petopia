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
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        PetopiaLinkerDataContext _petDB = null;
        public Signup()
        {
            InitializeComponent();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.PetopiaNewConnectionString);
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string name = Name.Text;
            string password = Password.Text;


            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter all fields.");
                return;
            }


            string newEmployeeID = GenerateEmployeeID();

            try
            {
                Employee newEmployee = new Employee
                {
                    Employee_ID = newEmployeeID,
                    Employee_Name = name,
                    Employee_Username = username,
                    Employee_Password = password
                };

                _petDB.Employees.InsertOnSubmit(newEmployee);
                _petDB.SubmitChanges();

                MessageBox.Show("Employee account created successfully.");
                this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating employee account: " + ex.Message);
            }
        }

        // Method to generate a new employee ID based on the number of existing employees
        private string GenerateEmployeeID()
        {
            int employeeCount = _petDB.Employees.Count();
            return "E" + (employeeCount + 1).ToString().PadLeft(3, '0');
        }
    }
}