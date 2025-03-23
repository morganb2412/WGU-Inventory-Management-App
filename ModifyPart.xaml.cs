using System;
using System.Windows;
using System.Windows.Controls;

namespace WGUInventoryManagemntSystem
{
    public partial class ModifyPart : Window
    {
        private Part selectedPart;

        public ModifyPart(Part part)
        {
            InitializeComponent();
            selectedPart = part;
            LoadPartData();
            if (DynamicFieldLabel == null)
            {
                MessageBox.Show("Error: DynamicFieldLabel is null. Check if it exists in XAML.",
                                "UI Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (InHouseRadio.IsChecked == true)
            {
                DynamicFieldLabel.Text = "Machine ID";
            }
            else if (OutsourcedRadio.IsChecked == true)
            {
                DynamicFieldLabel.Text = "Company Name";
            }
        }

        private void LoadPartData()
        {
            IDTextBox.Text = selectedPart.PartID.ToString();
            NameTextBox.Text = selectedPart.Name;
            InventoryTextBox.Text = selectedPart.Inventory.ToString();
            PriceTextBox.Text = selectedPart.Price.ToString();
            MaxTextBox.Text = selectedPart.Max.ToString();
            MinTextBox.Text = selectedPart.Min.ToString();

            if (selectedPart is InHousePart inHousePart)
            {
                InHouseRadio.IsChecked = true;
                DynamicFieldLabel.Text = "Machine ID";
                DynamicFieldTextBox.Text = inHousePart.MachineID.ToString();
            }
            else if (selectedPart is OutsourcedPart outsourcedPart)
            {
                OutsourcedRadio.IsChecked = true;
                DynamicFieldLabel.Text = "Company Name";
                DynamicFieldTextBox.Text = outsourcedPart.CompanyName;
            }
        }

        private void InHouseRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (DynamicFieldLabel != null)
            {
                DynamicFieldLabel.Text = "Machine ID";
            }
        }

        private void OutsourcedRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (DynamicFieldLabel != null)
            {
                DynamicFieldLabel.Text = "Company Name";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (!int.TryParse(InventoryTextBox.Text, out int inventory) ||
                !decimal.TryParse(PriceTextBox.Text, out decimal price) ||
                !int.TryParse(MaxTextBox.Text, out int max) ||
                !int.TryParse(MinTextBox.Text, out int min))
            {
                MessageBox.Show("Please enter valid numeric values for Inventory, Price, Max, and Min.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (min > max)
            {
                MessageBox.Show("Min value cannot be greater than Max value.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (inventory < min || inventory > max)
            {
                MessageBox.Show("Inventory must be between Min and Max values.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string name = NameTextBox.Text;

            // Handle In-House and Outsourced changes
            if (InHouseRadio.IsChecked == true)
            {
                if (!int.TryParse(DynamicFieldTextBox.Text, out int machineID))
                {
                    MessageBox.Show("Please enter a valid numeric Machine ID.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Replace part in inventory if type changed
                if (selectedPart is OutsourcedPart)
                {
                    Inventory.RemovePart(selectedPart.PartID);
                    selectedPart = new InHousePart(selectedPart.PartID, name, inventory, price, min, max, machineID);
                    Inventory.AddPart(selectedPart);
                }
                else
                {
                    // Update existing InHousePart
                    ((InHousePart)selectedPart).MachineID = machineID;
                }
            }
            else if (OutsourcedRadio.IsChecked == true)
            {
                string companyName = DynamicFieldTextBox.Text;

                // Replace part in inventory if type changed
                if (selectedPart is InHousePart)
                {
                    Inventory.RemovePart(selectedPart.PartID);
                    selectedPart = new OutsourcedPart(selectedPart.PartID, name, inventory, price, min, max, companyName);
                    Inventory.AddPart(selectedPart);
                }
                else
                {
                    // Update existing OutsourcedPart
                    ((OutsourcedPart)selectedPart).CompanyName = companyName;
                }
            }

            // Update common properties
            selectedPart.Name = name;
            selectedPart.Inventory = inventory;
            selectedPart.Price = price;
            selectedPart.Min = min;
            selectedPart.Max = max;

            MessageBox.Show("Part Modified!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Redirects back to main form
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}