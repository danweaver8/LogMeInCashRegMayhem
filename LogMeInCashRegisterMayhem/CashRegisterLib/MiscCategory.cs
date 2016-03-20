using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// The miscellaneous category is currently used to store newly added items to a receipt.
    /// </summary>
    public class MiscCategory
    {
        public string categoryDescription { get; }
        public string categoryTitle { get;  }
        private List<MiscItem> miscItems;
        
        public List<MiscItem> miscellaneousItems { get { return miscItems; } }

        public MiscCategory()
            {
                miscItems = new List<MiscItem>();
                categoryTitle = "Miscellaneous";
                categoryDescription = "The Miscellaneous category currently contains newly added items to be sorted into a proper category at a later time";
            }
    }
}
