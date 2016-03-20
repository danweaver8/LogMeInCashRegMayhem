using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    public class MiscItem : StoreItem
    {
        public string inventoryNum { get; }
        public string itemName { get; set; }
        public decimal itemPrice { get; set; }

        public MiscItem()
        {
            inventoryNum = UniqueInventoryNum.getRandomID();
            itemName = string.Empty;
            itemPrice = 0m;
        }
    }
}
