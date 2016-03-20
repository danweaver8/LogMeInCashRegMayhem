using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;
using CashRegisterLib;
using System.Collections.Specialized;

namespace LogMeInCashRegisterMayhem
{
    /// <summary>
    /// Summary description for CascDropDown
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CascDropDown : System.Web.Services.WebService
    {

        [WebMethod]
        public CascadingDropDownNameValue[] GetCategory(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> categories = new List<CascadingDropDownNameValue>();
            categories.Add(new CascadingDropDownNameValue("Grocery", "Grocery"));
            categories.Add(new CascadingDropDownNameValue("Electronic", "Electronic"));
            categories.Add(new CascadingDropDownNameValue("Clothing", "Clothing"));
            categories.Add(new CascadingDropDownNameValue("Kitchen", "Kitchen"));
            return categories.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetItems(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> items = new List<CascadingDropDownNameValue>();
            StringDictionary SelectedCategory = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
            ItemCategory myCat = (ItemCategory)Enum.Parse(typeof(ItemCategory), SelectedCategory["categoryid"].ToString(), true);
            switch (myCat)
            {
                case ItemCategory.Clothing:
                    items.Add(new CascadingDropDownNameValue("Shirt", "10.00"));
                    items.Add(new CascadingDropDownNameValue("Pants", "20.00"));
                    items.Add(new CascadingDropDownNameValue("Shoes", "30.00"));
                    items.Add(new CascadingDropDownNameValue("Belt", "25.00"));
                    break;
                case ItemCategory.Electronic:
                    items.Add(new CascadingDropDownNameValue("LED TV", "300.00"));
                    items.Add(new CascadingDropDownNameValue("Laptop", "350.00"));
                    items.Add(new CascadingDropDownNameValue("SSD", "100.00"));
                    items.Add(new CascadingDropDownNameValue("LED Monitor", "100.00"));
                    items.Add(new CascadingDropDownNameValue("iPod", "250.00"));
                    break;
                case ItemCategory.Grocery:
                    items.Add(new CascadingDropDownNameValue("Pretzels", "2.50"));
                    items.Add(new CascadingDropDownNameValue("Lettuce", "2.00"));
                    items.Add(new CascadingDropDownNameValue("Filet Mignon", "16.50"));
                    items.Add(new CascadingDropDownNameValue("Margarita Mix", "4.00"));
                    break;
                case ItemCategory.Kitchen:
                    items.Add(new CascadingDropDownNameValue("Mixing Spoon", "5.00"));
                    items.Add(new CascadingDropDownNameValue("Cutting Board", "12.00"));
                    items.Add(new CascadingDropDownNameValue("Utensil Holder", "8.00"));
                    break;
                default:
                    break;
            }
            return items.ToArray();
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetQuantity(string knownCategoryValues, string category)
        {
            List<CascadingDropDownNameValue> quantity = new List<CascadingDropDownNameValue>();
            quantity.Add(new CascadingDropDownNameValue("1", "1"));
            quantity.Add(new CascadingDropDownNameValue("2", "2"));
            quantity.Add(new CascadingDropDownNameValue("3", "3"));
            quantity.Add(new CascadingDropDownNameValue("4", "4"));
            quantity.Add(new CascadingDropDownNameValue("5", "5"));
            quantity.Add(new CascadingDropDownNameValue("6", "6"));
            return quantity.ToArray();
        }
    }
}
