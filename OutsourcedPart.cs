using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGUInventoryManagemntSystem
{
    public class OutsourcedPart : Part
    {
        public string CompanyName { get; set; }

        public OutsourcedPart(int partID, string name, int inventory, decimal price, int min, int max, string companyName)
            : base(partID, name, inventory, price, min, max)
        {
            CompanyName = companyName;
        }
    }

}
