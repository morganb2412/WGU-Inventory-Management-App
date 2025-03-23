using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGUInventoryManagemntSystem
{
    public class Product
    {
        public int ProductID { get; private set; } // 🔹 Prevents modification after creation
        public string Name { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public List<Part> AssociatedParts { get; set; } = new List<Part>();

        public Product(int productID, string name, int inventory, decimal price, int min, int max)
        {
            if (min > max)
                throw new ArgumentException("Min value cannot be greater than Max value.");

            if (inventory < min || inventory > max)
                throw new ArgumentException("Inventory must be between Min and Max values.");

            ProductID = productID;
            Name = name;
            Inventory = inventory;
            Price = price;
            Min = min;
            Max = max;
        }

        public void AddAssociatedPart(Part part)
        {
            AssociatedParts.Add(part);
        }

        public bool RemoveAssociatedPart(int partID)
        {
            var part = AssociatedParts.FirstOrDefault(p => p.PartID == partID);
            if (part != null)
            {
                AssociatedParts.Remove(part);
                return true;
            }
            return false;
        }

        public Part LookupAssociatedPart(int partID)
        {
            return AssociatedParts.FirstOrDefault(p => p.PartID == partID);
        }
    }
}