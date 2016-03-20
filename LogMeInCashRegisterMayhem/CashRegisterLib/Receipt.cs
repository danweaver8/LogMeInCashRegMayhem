using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// This class represents a full receipt of items. This can be used to pullback existing receipts or add new items and checkout.
    /// </summary>
    public class Receipt
    {
        public string company { get; set; }
        public string city { get; set; }
        public string phoneNumber { get; set; }
        public DateTime expirePeriod { get; set; }
        public decimal subTotal { get; set; }
        public decimal fee { get; set; }
        public decimal stateTax { get; set; }
        public decimal total { get; set; }
        public MiscCategory Miscellaneous { get; set; }
        public KitchenCategory Kitchen { get; set; }
        //Other future categories will follow

        
        public Receipt()
            {
            Miscellaneous = new MiscCategory();
            Kitchen = new KitchenCategory();
            }
    }
}
