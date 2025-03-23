using System;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace WGUInventoryManagemntSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PartsDataGrid.ItemsSource = Inventory.AllParts;
            ProductsDataGrid.ItemsSource = Inventory.AllProducts;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {
            AddPart addPartForm = new AddPart();
            addPartForm.Owner = this;
            addPartForm.ShowDialog();
        }

        private void btnModifyPart_Click(object sender, RoutedEventArgs e)
        {
            if (PartsDataGrid.SelectedItem is Part selectedPart)
            {
                ModifyPart modifyPartForm = new ModifyPart(selectedPart);
                modifyPartForm.Owner = this;
                modifyPartForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a part to modify.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeletePart_Click(object sender, RoutedEventArgs e)
        {
            if (PartsDataGrid.SelectedItem is Part selectedPart)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete part: {selectedPart.Name}?",
                                                          "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Inventory.RemovePart(selectedPart.PartID);
                    PartsDataGrid.ItemsSource = new BindingList<Part>(Inventory.AllParts);
                }
            }
            else
            {
                MessageBox.Show("Please select a part to delete.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSearchPart_Click(object sender, RoutedEventArgs e)
        {
            string query = PartsSearchBox.Text.ToLower();
            var results = Inventory.AllParts.Where(p => p.Name.ToLower().Contains(query) || p.PartID.ToString() == query).ToList();
            PartsDataGrid.ItemsSource = results;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProductForm = new AddProduct();
            addProductForm.Owner = this;
            addProductForm.ShowDialog();
        }

        private void btnModifyProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                ModifyProduct modifyProductForm = new ModifyProduct(selectedProduct);
                modifyProductForm.Owner = this;
                modifyProductForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product to modify.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Product selectedProduct)
            {
                if (selectedProduct.AssociatedParts.Count == 0)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete product: {selectedProduct.Name}?",
                                                              "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Inventory.RemoveProduct(selectedProduct.ProductID);
                        ProductsDataGrid.ItemsSource = new BindingList<Product>(Inventory.AllProducts);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot delete a product with associated parts.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnSearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string query = ProductsSearchBox.Text.ToLower();
            var results = Inventory.AllProducts.Where(p => p.Name.ToLower().Contains(query) || p.ProductID.ToString() == query).ToList();
            ProductsDataGrid.ItemsSource = results;
        }
    }
}
