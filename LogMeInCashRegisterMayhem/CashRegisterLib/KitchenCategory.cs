using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// This class would be used to hold kitchen items on a receipt. The constructor has an example of theoretical items that could exist in a kitchen\
    /// category.
    /// </summary>
    public class KitchenCategory
    {
        public string categoryDescription { get; }
        public string categoryTitle { get; }
        public List<KitchenItem> miscellaneousItems = new List<KitchenItem>();

        public KitchenCategory()
        {
            categoryTitle = "Kitchen";
            categoryDescription = "The Kitchen cattegory contains items that would theoretically already be in the inventory";

            KitchenItem Fork = new KitchenItem();
            Fork.itemName = "Fork";
            Fork.inventoryNum = "00000000";
            Fork.itemPrice = 2.0m;

            KitchenItem Spoon = new KitchenItem();
            Spoon.itemName = "Spoon";
            Spoon.inventoryNum = "00000001";
            Spoon.itemPrice = 2.0m;

            KitchenItem Pan = new KitchenItem();
            Pan.itemName = "Pan";
            Pan.inventoryNum = "00000002";
            Pan.itemPrice = 10.5m;

            miscellaneousItems.Add(Fork);
            miscellaneousItems.Add(Spoon);
            miscellaneousItems.Add(Pan);
        }
    }
}
