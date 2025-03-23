using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGUInventoryManagemntSystem
{
    public class InHousePart : Part
    {
        public int MachineID { get; set; }

        public InHousePart(int partID, string name, int inventory, decimal price, int min, int max, int machineID)
            : base(partID, name, inventory, price, min, max)
        {
            MachineID = machineID;
        }
    }


}
