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

namespace WGUInventoryManagemntSystem
{
    public partial class AddProduct : Window
    {
        private List<Part> AssociatedParts = new List<Part>();

        public AddProduct()
        {
            InitializeComponent();
            dgAllCandidateParts.ItemsSource = Inventory.AllParts;
            dgAssociatedParts.ItemsSource = AssociatedParts;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearch.Text.ToLower();
            var results = Inventory.AllParts.Where(p => p.Name.ToLower().Contains(query) || p.PartID.ToString() == query).ToList();
            dgAllCandidateParts.ItemsSource = results;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgAllCandidateParts.SelectedItem is Part selectedPart)
            {
                AssociatedParts.Add(selectedPart);
                dgAssociatedParts.ItemsSource = null;
                dgAssociatedParts.ItemsSource = AssociatedParts;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAssociatedParts.SelectedItem is Part selectedPart)
            {
                AssociatedParts.Remove(selectedPart);
                dgAssociatedParts.ItemsSource = null;
                dgAssociatedParts.ItemsSource = AssociatedParts;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            if (!int.TryParse(txtInventory.Text, out int inventory) ||
                !decimal.TryParse(txtPrice.Text, out decimal price) ||
                !int.TryParse(txtMax.Text, out int max) ||
                !int.TryParse(txtMin.Text, out int min))
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

            int newProductID = Inventory.GenerateProductID();
            string name = txtName.Text;

            
            Product newProduct = new Product(newProductID, name, inventory, price, min, max);

            
            foreach (Part part in AssociatedParts)
            {
                newProduct.AddAssociatedPart(part);
            }

            Inventory.AddProduct(newProduct);

            MessageBox.Show("Product Saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // Redirects back to main form
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
