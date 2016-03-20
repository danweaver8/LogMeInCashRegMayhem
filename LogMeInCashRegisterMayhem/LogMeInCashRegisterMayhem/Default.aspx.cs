using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CashRegisterLib;
using System.IO.Compression;

namespace LogMeInCashRegisterMayhem
{
    public partial class _Default : Page
    {
        private Receipt MyReceipt;
        static Table ReceiptItemTable, GroceryTable, ElectronicTable,ClothingTable,KitchenTable;

        protected void Page_Init(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
            HttpContext.Current.Response.AppendHeader("Content-encoding", "gzip");
            HttpContext.Current.Response.Cache.VaryByHeaders["Accept-encoding"] = true;
        } 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Populate drop down with states
                LoadStateDropDown();
                //Initialize receipt sections
                ReceiptItemTable = new Table(); 
                GroceryTable = new Table();
                ElectronicTable = new Table();
                ClothingTable = new Table();
                KitchenTable = new Table();
                ReceiptItemTable.Style.Add("width", "100%");
                GroceryTable.Style.Add("width", "100%");
                ElectronicTable.Style.Add("width", "100%");
                ClothingTable.Style.Add("width", "100%");
                KitchenTable.Style.Add("width", "100%");
                //Add company, phone, date to receipt
                SetReceiptHeader();
            }
            //Readd the tables on postback so they appear in the receipt
            resultHolder.Controls.Add(ReceiptItemTable);
            GroceryHolder.Controls.Add(GroceryTable);
            ElectronicHolder.Controls.Add(ElectronicTable);
            ClothingHolder.Controls.Add(ClothingTable);
            KitchenHolder.Controls.Add(KitchenTable);
        }

        #region Initial Load Helpers (State DropDown, Receipt Header)
        protected void AddStateTaxReceipt(object sender, EventArgs e)
        {
             decimal salesTax = StateSalesTax.stateDictTax[statesDDL.SelectedValue] * 100;
            StateTaxLabel.Text = salesTax + "%";
            UpdateTotal();
        }

        public void SetReceiptHeader()
        {
            MyReceipt = new Receipt();
            MyReceipt.company = "Foo Company";
            MyReceipt.city = "Philadelphia";
            MyReceipt.phoneNumber = "609-509-6101";
            MyReceipt.expirePeriod = DateTime.Now.AddMonths(3);

            CompanyLabel.Text = MyReceipt.company;
            CityLabel.Text = MyReceipt.city;
            Phone.Text = MyReceipt.phoneNumber;
            Expiration.Text = "Expires: " + MyReceipt.expirePeriod.ToString();
        }

        public void LoadStateDropDown()
        {
            statesDDL.DataSource = StateSalesTax.stateDictTax.Keys;
            statesDDL.DataBind();
            statesDDL.Items.Insert(0, string.Empty);
        }
        #endregion

        #region Add New Item to Receipt
        protected void AddNewItem(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewItemNameTextbox.Text) || string.IsNullOrEmpty(ItemPriceTextbox.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", "alert(\"An Item Name and Price is required to add an item\");", true);
                return;
            }

            //Add Miscellaneous header and sales tax if adding an item for the first time.
            AddMiscCategoryReceipt();

            //Create a new miscellaneous item to add to receipt
            MiscItem newReceiptItem = new MiscItem();
            newReceiptItem.itemName = NewItemNameTextbox.Text.Trim();
            decimal itemPriceTest = 0m;
            decimal.TryParse(ItemPriceTextbox.Text, out itemPriceTest);
            newReceiptItem.itemPrice = itemPriceTest;
            
            //Add new Item to Miscellaneous
            AddNewItemReceiptRow(newReceiptItem);
            //Update Subtotal with new item
            UpdateSubtotal(itemPriceTest);
            //Update total factoring in state tax
            UpdateTotal();
        }

        public void AddMiscCategoryReceipt()
        {
            if (ReceiptItemTable.Rows.Count == 0)
            {
                TableRow CategoryRow = new TableRow();
                TableCell CategoryCell = new TableCell();
                CategoryCell.Text = ItemCategory.Miscellaneous.ToString();
                CategoryCell.Font.Bold = true;
                CategoryRow.Cells.Add(CategoryCell);
                ReceiptItemTable.Rows.Add(CategoryRow);
            }
        }

        public void AddNewItemReceiptRow(MiscItem newReceiptItem)
        {
            TableRow newItemRow = new TableRow();
            TableCell newItemNumber = new TableCell();
            TableCell newItemName = new TableCell();
            TableCell newItemPrice = new TableCell();
            TableCell newItemQuantity = new TableCell();
            newItemNumber.Text = newReceiptItem.inventoryNum;
            newItemNumber.Width = 135;
            newItemName.Text = newReceiptItem.itemName;
            newItemPrice.Text = newReceiptItem.itemPrice.ToString("c");
            newItemQuantity.Text = QuantityDDL.SelectedValue;
            newItemRow.Cells.Add(newItemNumber);
            newItemRow.Cells.Add(newItemName);
            newItemRow.Cells.Add(newItemPrice);
            newItemRow.Cells.Add(newItemQuantity);
            ReceiptItemTable.Rows.Add(newItemRow);
            resultHolder.Controls.Add(ReceiptItemTable);
        }
        #endregion

        #region Add Existing Item
        protected void AddExistingItem(object sender, EventArgs e)
        {
            string existingCategory = CategoryCascDDL.SelectedValue;
            switch(existingCategory)
            {
                case "Grocery":
                    AddGroceryItem();
                    break;
                case "Electronic":
                    AddElectronicItem();
                    break;
                case "Clothing":
                    AddClothingItem();
                    break;
                case "Kitchen":
                    AddKitchenItem();
                    break;
                default:
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", " jQuery.facebox(\"Please select a Category\");", true);
                    break;
            }
        }


        public void AddGroceryItem()
        {
            //Check if we can add an item
            if (ItemCascDDL.SelectedItem.Text == "Select Items" || ItemCascDDL.SelectedItem.Text == string.Empty || QuantityCascDDL.SelectedItem.Text == "Select Quantity" || QuantityCascDDL.SelectedItem.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", " jQuery.facebox(\"Please select an item and Quantity to add\");", true);
                return;
            }
            //Check if we need to add the category to receipt
            AddExistingCategoryReceipt(GroceryTable, ItemCategory.Grocery);

            //Get values since we dont have a grocery object yet;
            string groceryItem = ItemCascDDL.SelectedItem.Text;
            decimal groceryItemPrice = decimal.Parse(ItemCascDDL.SelectedValue);
            string quantity = QuantityCascDDL.SelectedValue;
            //Add item to Receipt
            AddExistingItemReceipt(groceryItem, groceryItemPrice, quantity, GroceryTable, GroceryHolder);
            //Update subTotal
            UpdateSubtotalExistingItem(groceryItemPrice, quantity);
            //Update Total
            UpdateTotal();
        }
        public void AddElectronicItem()
        {
            //Check if we can add an item
            if (ItemCascDDL.SelectedItem.Text == "Select Items" || ItemCascDDL.SelectedItem.Text == string.Empty || QuantityCascDDL.SelectedItem.Text == "Select Quantity" || QuantityCascDDL.SelectedItem.Text==string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", " jQuery.facebox(\"Please select an item and Quantity to add\");", true);
                return;
            }
            //Check if we need to add the category to receipt
            AddExistingCategoryReceipt(ElectronicTable, ItemCategory.Electronic);

            //Get values since we dont have a grocery object yet;
            string electronicItem = ItemCascDDL.SelectedItem.Text;
            decimal electronicItemPrice = decimal.Parse(ItemCascDDL.SelectedValue);
            string quantity = QuantityCascDDL.SelectedValue;
            //Add item to Receipt
            AddExistingItemReceipt(electronicItem, electronicItemPrice, quantity, ElectronicTable, ElectronicHolder);
            //Update subTotal
            UpdateSubtotalExistingItem(electronicItemPrice, quantity);
            //Update Total
            UpdateTotal();
        }

        public void AddClothingItem()
        {
            //Check if we can add an item
            if (ItemCascDDL.SelectedItem.Text == "Select Items" || ItemCascDDL.SelectedItem.Text == string.Empty || QuantityCascDDL.SelectedItem.Text == "Select Quantity" || QuantityCascDDL.SelectedItem.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", " jQuery.facebox(\"Please select an item and Quantity to add\");", true);
                return;
            }
            //Check if we need to add the category to receipt
            AddExistingCategoryReceipt(ClothingTable, ItemCategory.Clothing);

            //Get values since we dont have a grocery object yet;
            string clothingItem = ItemCascDDL.SelectedItem.Text;
            decimal clothingItemPrice = decimal.Parse(ItemCascDDL.SelectedValue);
            string quantity = QuantityCascDDL.SelectedValue;
            //Add item to Receipt
            AddExistingItemReceipt(clothingItem, clothingItemPrice, quantity, ClothingTable, ClothingHolder);
            //Update subTotal
            UpdateSubtotalExistingItem(clothingItemPrice, quantity);
            //Update Total
            UpdateTotal();
        }

        public void AddKitchenItem()
        {
            //Check if we can add an item
            if (ItemCascDDL.SelectedItem.Text == "Select Items" || ItemCascDDL.SelectedItem.Text == string.Empty || QuantityCascDDL.SelectedItem.Text == "Select Quantity" || QuantityCascDDL.SelectedItem.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Error", " jQuery.facebox(\"Please select an item and Quantity to add\");", true);
                return;
            }
            //Check if we need to add the category to receipt
            AddExistingCategoryReceipt(KitchenTable, ItemCategory.Kitchen);

            //Get values since we dont have a grocery object yet;
            string kitchenItem = ItemCascDDL.SelectedItem.Text;
            decimal kitchenItemPrice = decimal.Parse(ItemCascDDL.SelectedValue);
            string quantity = QuantityCascDDL.SelectedValue;
            //Add item to Receipt
            AddExistingItemReceipt(kitchenItem, kitchenItemPrice, quantity, KitchenTable, KitchenHolder);
            //Update subTotal
            UpdateSubtotalExistingItem(kitchenItemPrice, quantity);
            //Update Total
            UpdateTotal();
        }
        #endregion  S

        #region Add new and existing Category Headers Receipt
        public void AddExistingCategoryReceipt(Table categoryTable, ItemCategory category)
        {
            if (categoryTable.Rows.Count == 0)
            {
                TableRow CategoryRow = new TableRow();
                TableCell CategoryCell = new TableCell();
                CategoryCell.Text = category.ToString();
                CategoryCell.Font.Bold = true;
                CategoryCell.Style.Add("text-align", "left");
                CategoryRow.Cells.Add(CategoryCell);
                categoryTable.Rows.Add(CategoryRow);
            }
        }
        #endregion
        public void AddExistingItemReceipt(string existingItem, decimal price, string quantity, Table table, PlaceHolder holder)
        {
            TableRow existingItemRow = new TableRow();
            TableCell existingItemNumber = new TableCell();
            TableCell existingItemName = new TableCell();
            TableCell existingItemPrice = new TableCell();
            TableCell existingItemQuantity = new TableCell();
            existingItemNumber.Text = UniqueInventoryNum.getRandomID();
            existingItemNumber.Width = 135;
            existingItemName.Text = existingItem;
            existingItemPrice.Text = price.ToString("c");
            existingItemQuantity.Text = quantity;
            existingItemRow.Cells.Add(existingItemNumber);
            existingItemRow.Cells.Add(existingItemName);
            existingItemRow.Cells.Add(existingItemPrice);
            existingItemRow.Cells.Add(existingItemQuantity);
            table.Rows.Add(existingItemRow);
            holder.Controls.Add(table);
        }

        #region Update Total and Subtotals
        public void UpdateTotal()
        {
            decimal stateTax = StateSalesTax.stateDictTax[statesDDL.SelectedValue];
            decimal subTotal = decimal.Parse(SubtotalLabel.Text.Substring(1, SubtotalLabel.Text.Length - 1));
            decimal taxAmount = stateTax * subTotal;
            decimal Total = subTotal + taxAmount;
            TotalLabel.Text = Total.ToString("c");
        }

        public void UpdateSubtotal(decimal itemPriceTest)
        {
            int quantity = int.Parse(QuantityDDL.SelectedValue);
            decimal runningSubtotal = decimal.Parse(SubtotalLabel.Text.Substring(1, SubtotalLabel.Text.Length - 1));
            runningSubtotal += itemPriceTest * quantity;
            SubtotalLabel.Text = runningSubtotal.ToString("c");
        }

        public void UpdateSubtotalExistingItem(decimal itemPrice, string quantity)
        {
            int updateQuantity = int.Parse(quantity);
            decimal runningSubtotal = decimal.Parse(SubtotalLabel.Text.Substring(1, SubtotalLabel.Text.Length - 1));
            runningSubtotal += itemPrice * updateQuantity;
            SubtotalLabel.Text = runningSubtotal.ToString("c");
        }
        #endregion
    }
}