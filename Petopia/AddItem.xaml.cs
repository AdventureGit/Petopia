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
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();
            InitializeItems();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.PetopiaNewConnectionString);

        }
        private void InitializeItems()
        {
            EntryTypeCombo.Items.Add("Pet");
            EntryTypeCombo.Items.Add("Item");
        }

        private void ActivateItems()
        {
            Tb0.Visibility = Visibility.Visible;
            Tb1.Visibility = Visibility.Visible;
            Tb2.Visibility = Visibility.Visible;
            Tb3.Visibility = Visibility.Visible;
            Tb4.Visibility = Visibility.Visible;
            Tb5.Visibility = Visibility.Visible;
            Tb1.Visibility = Visibility.Visible;
            Tbx0.Visibility = Visibility.Visible;
            Tbx1.Visibility = Visibility.Visible;
            Tbx2.Visibility = Visibility.Visible;
            ComboType1.Visibility = Visibility.Visible;
            ComboType2.Visibility = Visibility.Visible;
            ComboType3.Visibility = Visibility.Visible;
            AddItemBtn.Visibility = Visibility.Visible;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EntryTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntryTypeCombo.SelectedItem.ToString() == "Pet")
            {
                Tb1.Text = "Pet Type:";
                Tb2.Text = "Breed:";
                Tb3.Text = "Weight:";
                Tb4.Text = "Gender:";
                Tb5.Text = "Adoption Status:";
                ComboType1.Items.Clear();
                ComboType2.Items.Clear();
                ComboType3.Items.Clear();
                ComboType1.Items.Add("Dog");
                ComboType1.Items.Add("Cat");
                ComboType2.Items.Add("Male");
                ComboType2.Items.Add("Female");
                ComboType3.Items.Add("Adopted");
                ComboType3.Items.Add("Not Adopted");
                ActivateItems();
            }
            else if (EntryTypeCombo.SelectedItem.ToString() == "Item")
            {
                Tb1.Text = "Pet Type:";
                Tb2.Text = "Price:";
                Tb3.Text = "Quantity:";
                Tb4.Text = "Item Type:";
                Tb5.Text = "Availabilty:";
                ComboType1.Items.Clear();
                ComboType2.Items.Clear();
                ComboType3.Items.Clear();
                ComboType1.Items.Add("Dog");
                ComboType1.Items.Add("Cat");
                ComboType2.Items.Add("Food");
                ComboType2.Items.Add("Accessories");
                ComboType3.Items.Add("Available");
                ComboType3.Items.Add("Unavailable");
                ActivateItems();
            }
        }
    }
}
