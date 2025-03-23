using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace WGUInventoryManagemntSystem
{
    public abstract class Part
    {
        public int PartID { get; private set; } 
        public string Name { get; set; }
        public int Inventory { get; set; }
        public decimal Price { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        
        protected Part() { }

        
        protected Part(int partID, string name, int inventory, decimal price, int min, int max)
        {
            if (min > max)
                throw new ArgumentException("Min value cannot be greater than Max value.");

            if (inventory < min || inventory > max)
                throw new ArgumentException("Inventory must be between Min and Max values.");

            PartID = partID;
            Name = name;
            Inventory = inventory;
            Price = price;
            Min = min;
            Max = max;
        }
    }
}

