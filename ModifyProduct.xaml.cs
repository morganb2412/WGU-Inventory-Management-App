using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WGUInventoryManagemntSystem
{
    public partial class ModifyProduct : Window
    {
        private Product selectedProduct;
        private List<Part> associatedParts = new List<Part>();  

        public ModifyProduct(Product product)
        {
            InitializeComponent();
            selectedProduct = product;
            LoadProductData();
        }

        private void LoadProductData()
        {
            
            txtID.Text = selectedProduct.ProductID.ToString();
            txtName.Text = selectedProduct.Name;
            txtInventory.Text = selectedProduct.Inventory.ToString();
            txtPrice.Text = selectedProduct.Price.ToString();
            txtMax.Text = selectedProduct.Max.ToString();
            txtMin.Text = selectedProduct.Min.ToString();

            
            dgAllParts.ItemsSource = Inventory.AllParts;

            
            associatedParts = selectedProduct.AssociatedParts.ToList();
            dgAssociatedParts.ItemsSource = associatedParts;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearch.Text.ToLower();
            var results = Inventory.AllParts.Where(p => p.Name.ToLower().Contains(query) || p.PartID.ToString() == query).ToList();
            dgAllParts.ItemsSource = results;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgAllParts.SelectedItem is Part selectedPart)
            {
                
                if (!associatedParts.Any(p => p.PartID == selectedPart.PartID))
                {
                    associatedParts.Add(selectedPart);
                    RefreshAssociatedParts();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgAssociatedParts.SelectedItem is Part selectedPart)
            {
                associatedParts.Remove(selectedPart);
                RefreshAssociatedParts();
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

            
            selectedProduct.Name = txtName.Text;
            selectedProduct.Inventory = inventory;
            selectedProduct.Price = price;
            selectedProduct.Max = max;
            selectedProduct.Min = min;

            
            selectedProduct.AssociatedParts = associatedParts;

            MessageBox.Show("Product Modified Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshAssociatedParts()
        {
            dgAssociatedParts.ItemsSource = null;
            dgAssociatedParts.ItemsSource = associatedParts;
        }
    }
}