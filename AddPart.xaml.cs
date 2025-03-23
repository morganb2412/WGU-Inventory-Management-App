using System;
using System.Windows;

namespace WGUInventoryManagemntSystem
{
    public partial class AddPart : Window
    {
        public AddPart()
        {
            InitializeComponent();
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

            int newPartID = Inventory.GeneratePartID(); // ✅ Generates a unique ID for the part
            string name = NameTextBox.Text;

            if (InHouseRadio.IsChecked == true)
            {
                if (!int.TryParse(DynamicFieldTextBox.Text, out int machineID))
                {
                    MessageBox.Show("Please enter a valid numeric Machine ID.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                InHousePart newPart = new InHousePart(newPartID, name, inventory, price, min, max, machineID);  // ✅ Fixed order
                Inventory.AddPart(newPart);
            }
            else if (OutsourcedRadio.IsChecked == true)
            {
                string companyName = DynamicFieldTextBox.Text;
                OutsourcedPart newPart = new OutsourcedPart(newPartID, name, inventory, price, min, max, companyName);  // ✅ Fixed order
                Inventory.AddPart(newPart);
            }

            MessageBox.Show("Part Saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Redirects back to main form
        }
        

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
