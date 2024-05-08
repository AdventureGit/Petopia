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
    /// Interaction logic for SelectionPage.xaml
    /// </summary>
    public partial class SelectionPage : Window
    {
        private string loggedInEmployeeID;

        public SelectionPage(string employeeID)
        {
            InitializeComponent();
            loggedInEmployeeID = employeeID;
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            AddItem addItem = new AddItem(loggedInEmployeeID);
            addItem.Show();
            this.Close();
        }
        private void SearchEditButton_Click(object sender, RoutedEventArgs e)
        {
            SearchEdit searchEdit = new SearchEdit();
            searchEdit.Show();
            this.Close();
        }

        private void LogsButton_Click(object sender, RoutedEventArgs e)
        {
            Logs logs = new Logs();
            logs.Show();
            this.Close();
        }
    }
}
