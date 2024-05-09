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
        private string loggedInEmployeeID;
        PetopiaLinkerDataContext _petDB = null;

        public AddItem(string loggedInEmployeeID)
        {
            InitializeComponent();
            InitializeItems();
            try
            {
                _petDB = new PetopiaLinkerDataContext(Properties.Settings.Default.Petopia_UpdatedConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while connecting to the database: {ex.Message}", "Database Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
              
            }

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
            Tbx0.Visibility = Visibility.Visible;
            Tbx1.Visibility = Visibility.Visible;
            Tbx2.Visibility = Visibility.Visible;
            ComboType1.Visibility = Visibility.Visible;
            ComboType2.Visibility = Visibility.Visible;
            ComboType3.Visibility = Visibility.Visible;
            AddItemBtn.Visibility = Visibility.Visible;
        }

        private void EntryTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EntryTypeCombo.SelectedItem != null)
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
                    ComboType3.Items.Add("Active");
                    ComboType3.Items.Add("Inactive");
                    ActivateItems();
                }
                else if (EntryTypeCombo.SelectedItem.ToString() == "Item")
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
                    ActivateItems();
                }
            }
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EntryTypeCombo.SelectedItem != null)
                {
                    if (EntryTypeCombo.SelectedItem.ToString() == "Pet")
                    {
                        Pet newPet = new Pet
                        {
                            Pet_ID = GenerateNextPetID(),
                            Pet_Name = Tbx0.Text,
                            PetType_ID = GetPetTypeID(ComboType1.SelectedItem.ToString()),
                            Pet_Breed = Tbx1.Text,
                            Pet_Weight = Convert.ToDecimal(Tbx2.Text),
                            Pet_Gender = ComboType2.SelectedItem.ToString(),
                            IsAdopted_ID = GetIsAdoptedID(ComboType3.SelectedItem.ToString()),
                            Employee_ID = loggedInEmployeeID
                        };

                        _petDB.Pets.InsertOnSubmit(newPet);
                        _petDB.SubmitChanges();
                    }
                    else if (EntryTypeCombo.SelectedItem.ToString() == "Item")
                    {
                        Item newItem = new Item
                        {

                            Item_ID = GenerateNextItemID(),
                            Item_Name = Tbx0.Text,
                            Item_Price = Convert.ToDecimal(Tbx1.Text),
                            Item_Quantity = Convert.ToInt32(Tbx2.Text),
                            PetType_ID = GetPetTypeID(ComboType1.SelectedItem.ToString()),
                            ItemType_ID = GetItemTypeID(ComboType2.SelectedItem.ToString()),
                            Availability_ID = GetAvailabilityID(ComboType3.SelectedItem.ToString()),
                            Employee_ID = loggedInEmployeeID
                        };

                        _petDB.Items.InsertOnSubmit(newItem);
                        _petDB.SubmitChanges();
                    }

                    MessageBox.Show("Item added successfully.");
                }
                else
                {
                    MessageBox.Show("Please select an entry type.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GenerateNextPetID()
        {
            var maxPetID = (from pet in _petDB.Pets select pet.Pet_ID).Max();

            if (maxPetID != null)
            {
                int lastPetNumber = int.Parse(maxPetID.Substring(1));
                int nextPetNumber = lastPetNumber + 1;
                string nextPetID = "P" + nextPetNumber.ToString("D3");
                return nextPetID;
            }
            else
            {
                return "P001";
            }
        }

        private string GenerateNextItemID()
        {
            var maxItemID = (from item in _petDB.Items select item.Item_ID).Max();

            if (maxItemID != null)
            {
                int lastItemNumber = int.Parse(maxItemID.Substring(1));
                int nextItemNumber = lastItemNumber + 1;
                string nextItemID = "I" + nextItemNumber.ToString("D3");
                return nextItemID;
            }
            else
            {
                return "I001";
            }
        }

        private string GetPetTypeID(string petTypeName)
        {
            if (petTypeName == "Cat")
            {
                return "PT001";
            }
            else if (petTypeName == "Dog")
            {
                return "PT002";
            }
            else
            {
                return "Unknown";
            }
        }

        private string GetItemTypeID(string itemTypeName)
        {

            if (itemTypeName.Equals("Food"))
            {
                return "IT001";
            }
            else if (itemTypeName.Equals("Accessory"))
            {
                return "IT002";
            }
            else
            {
                throw new ArgumentException("Invalid item type name.");
            }
        }


        private string GetIsAdoptedID(string adoptionStatus)
        {
            if (adoptionStatus.Equals("Active"))
            {
                return "ADO001";
            }
            else if (adoptionStatus.Equals("Inactive"))
            {
                return "ADO002";
            }
            else
            {
                throw new ArgumentException("Invalid adoption status.");
            }
        }

        private string GetAvailabilityID(string availabilityStatus)
        {
            if (availabilityStatus.Equals("Available"))
            {
                return "AV001";
            }
            else if (availabilityStatus.Equals("Unavailable"))
            {
                return "AV002";
            }
            else
            {
                throw new ArgumentException("Invalid availability status.");
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            SelectionPage selectionPage = new SelectionPage(loggedInEmployeeID);

            selectionPage.Show();

            this.Close();
        }

    }
}

