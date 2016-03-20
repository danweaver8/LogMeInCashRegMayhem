using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// This class would store any kitchen item to fit into a kitchen category in a receipt. Items such as a fork, spoon, knife, etc.
    /// </summary>
    public class KitchenItem: StoreItem
    {
        public string inventoryNum { get; set; }
        public string itemName { get; set; }
        public decimal itemPrice { get; set; }

        public KitchenItem()
        {
            inventoryNum = string.Empty;
            itemName = string.Empty;
            itemPrice = 0m;
        }
    }
}
