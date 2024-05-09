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
    /// Interaction logic for SearchEdit.xaml
    /// </summary>x
    public partial class SearchEdit : Window
    {
        private string loggedInEmployeeID;
        private PetopiaLinkerDataContext _petDB = null;

        public SearchEdit(string loggedInEmployeeID)
        {
            InitializeComponent();
            InitializeItems();
            _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.Petopia_UpdatedConnectionString);
            this.loggedInEmployeeID = loggedInEmployeeID;
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

        private void RenamePet()
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
        }

        private void RenameItem()
        {
            Tb1.Text = "Pet Type:";
            Tb2.Text = "Price:";
            Tb3.Text = "Quantity:";
            Tb4.Text = "Item Type:";
            Tb5.Text = "Availability:";
            ComboType1.Items.Clear();
            ComboType2.Items.Clear();
            ComboType3.Items.Clear();
            ComboType1.Items.Add("Dog");
            ComboType1.Items.Add("Cat");
            ComboType2.Items.Add("Food");
            ComboType2.Items.Add("Accessories");
            ComboType3.Items.Add("Available");
            ComboType3.Items.Add("Unavailable");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemBox.SelectedItem != null)
            {
                string selectedItemName = ItemBox.SelectedItem.ToString();

                if (EntryTypeCombo.SelectedItem.ToString() == "Pet")
                {
                    Pet selectedPet = _petDB.Pets.FirstOrDefault(p => p.Pet_Name == selectedItemName);

                    if (selectedPet != null)
                    {
                        Tbx0.Text = selectedPet.Pet_Name;
                        ComboType1.SelectedItem = selectedPet.PetType.PetType_Name;
                        Tbx1.Text = selectedPet.Pet_Breed;
                        Tbx2.Text = selectedPet.Pet_Weight.ToString();
                        ComboType2.SelectedItem = selectedPet.Pet_Gender;
                        ComboType3.SelectedItem = selectedPet.IsAdopted.IsAdopted_Status;
                    }
                }
                else if (EntryTypeCombo.SelectedItem.ToString() == "Item")
                {
                    Item selectedItem = _petDB.Items.FirstOrDefault(i => i.Item_Name == selectedItemName);

                    if (selectedItem != null)
                    {
                        Tbx0.Text = selectedItem.Item_Name;
                        Tbx1.Text = selectedItem.Item_Price.ToString();
                        Tbx2.Text = selectedItem.Item_Quantity.ToString();
                        ComboType1.SelectedItem = selectedItem.PetType.PetType_Name;
                        ComboType2.SelectedItem = selectedItem.ItemType.ItemType_Name;
                        ComboType3.SelectedItem = selectedItem.Availability.Availability_Status;
                    }
                }
            }
        }

        private void EntryTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntryTypeCombo.SelectedItem.ToString() == "Pet")
            {
                ItemBox.Items.Clear();
                RenamePet();
                ActivateItems();
                foreach (var item in _petDB.Pets)
                {
                    ItemBox.Items.Add(item.Pet_Name.ToString());
                }
            }
            else if (EntryTypeCombo.SelectedItem.ToString() == "Item")
            {
                ItemBox.Items.Clear();
                RenameItem();
                ActivateItems();
                foreach (var item in _petDB.Items)
                {
                    ItemBox.Items.Add(item.Item_Name.ToString());
                }
            }
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {
            if (EntryTypeCombo.SelectedItem.ToString() == "Pet")
            {
                // Update pet information
                Pet selectedPet = _petDB.Pets.FirstOrDefault(p => p.Pet_Name == Tbx0.Text);

                if (selectedPet != null)
                {
                    selectedPet.PetType.PetType_Name = ComboType1.SelectedItem.ToString();
                    selectedPet.Pet_Breed = Tbx1.Text;
                    selectedPet.Pet_Weight = decimal.Parse(Tbx2.Text);
                    selectedPet.Pet_Gender = ComboType2.SelectedItem.ToString();
                    selectedPet.IsAdopted.IsAdopted_Status = ComboType3.SelectedItem.ToString();
                    _petDB.SubmitChanges();
                    MessageBox.Show("Pet information updated successfully!");
                }
            }
            else if (EntryTypeCombo.SelectedItem.ToString() == "Item")
            {
                // Update item information
                Item selectedItem = _petDB.Items.FirstOrDefault(i => i.Item_Name == Tbx0.Text);

                if (selectedItem != null)
                {
                    selectedItem.PetType.PetType_Name = ComboType1.SelectedItem.ToString();
                    selectedItem.Item_Price = decimal.Parse(Tbx1.Text);
                    selectedItem.Item_Quantity = int.Parse(Tbx2.Text);
                    selectedItem.ItemType.ItemType_Name = ComboType2.SelectedItem.ToString();
                    _petDB.SubmitChanges();
                    MessageBox.Show("Item information updated successfully!");
                }
            }
        }
    }
}