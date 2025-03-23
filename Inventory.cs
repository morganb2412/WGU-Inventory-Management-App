using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGUInventoryManagemntSystem
{
    public static class Inventory
    {
        public static BindingList<Part> AllParts { get; set; } = new BindingList<Part>();
        public static BindingList<Product> AllProducts { get; set; } = new BindingList<Product>();

        private static int nextPartID = 1;
        private static int nextProductID = 1; 

        public static int GeneratePartID()
        {
            return nextPartID++;
        }

        public static int GenerateProductID() 
        {
            return nextProductID++;
        }

        public static void AddPart(Part part)
        {
            AllParts.Add(part);
        }

        public static void AddProduct(Product product)
        {
            AllProducts.Add(product);
        }

        public static bool RemovePart(int partID)
        {
            var part = AllParts.FirstOrDefault(p => p.PartID == partID);
            if (part != null)
            {
                AllParts.Remove(part);
                return true;
            }
            return false;
        }

        public static bool RemoveProduct(int productID)
        {
            var product = AllProducts.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                AllProducts.Remove(product);
                return true;
            }
            return false;
        }

        public static Part LookupPart(int partID)
        {
            return AllParts.FirstOrDefault(p => p.PartID == partID);
        }

        public static Product LookupProduct(int productID)
        {
            return AllProducts.FirstOrDefault(p => p.ProductID == productID);
        }
    }
}
