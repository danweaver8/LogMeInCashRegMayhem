using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// This store item class is used to represent newly added items to the list as well
    /// as items existing in the Foo store inventory. This is the generic class used to hold
    /// the item name, unique store number, and price.
    /// </summary>
    public interface StoreItem
    {
        string inventoryNum { get; }
        string itemName { get; set; }
        decimal itemPrice { get; set; }
    }
}
