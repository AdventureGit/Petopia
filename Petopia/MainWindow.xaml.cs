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

                             //&& s.Password == txtbPassword.Text
                             select s;

            foreach (var login in employeeLogin)
            {
                if (login.Employee_Password == PasswordTbx.Text)
                {
                    //loginFlag = true;
                    //userName = login.Name;
                    //login.LastLoginDate = cDT;

                    //Log log = new Log();
                    //log.LoginID = login.LoginID;
                    //log.TimeStamp = cDT;

                    //_lsDC.Logs.InsertOnSubmit(log);
                    //_lsDC.SubmitChanges();
                    MessageBox.Show("Hacked the mainframe");
                    SelectionPage selectionpage = new SelectionPage();
                    selectionpage.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password");
                }
            }

        }
    }
}
