using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterLib
{
    /// <summary>
    /// 
    /// </summary>
    public static class StateSalesTax
    {
        /// <summary>
        /// This dictionary contains the state as the key and the surge sales tax price as the value.
        /// The list below will be used to populate the drop down so it makes sense to have an O(1)
        /// access to each state's sales tax.
        /// </summary>
       public static Dictionary<string, decimal> stateDictTax = new Dictionary<string, decimal>
       {
            { "AL", .135m }, { "AK", .07m }, { "AZ", .10725m }, { "AR", .11625m }, { "CA", .10m }, { "CO", .10m },
            { "CT", .0635m }, { "DE", 0m }, { "FL", .075m }, { "GA", .08m }, { "HI", .04712m }, { "ID", .085m },
            { "IL", .0975m }, { "IN", .07m }, { "IA", .07m }, { "KS", .0965m }, { "KY", .06m }, { "LA", .11m },
            { "ME", .055m }, { "MD", .06m }, { "MA", .0625m }, { "MI", .06m }, { "MN", .07875m }, { "MS", .0725m },
            { "MO", .1085m }, { "MT", 0m }, { "NE", .075m }, { "NV", .081m }, { "NH", 0m }, { "NJ", .07m },
            { "NM", .08688m }, { "NY", .08875m }, { "NC", .0750m }, { "ND", .08m }, { "OH", .08m },
            { "OK", .11m }, { "OR", 0m }, { "PA", .08m }, { "RI", .07m }, { "SC", .09m },
            { "SD", .06m }, { "TN", .0975m }, { "TX", .0825m }, { "UT", .0835m }, { "VT", .07m },
            { "VA", .06m }, { "WA", .096m }, { "WV", .07m }, { "WI", .056m }, { "WY", .06m }
        };
    }
}
