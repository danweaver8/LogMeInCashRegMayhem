<%@ Page Title="LogMeIn Cash Register Mayhem" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LogMeInCashRegisterMayhem._Default" EnableEventValidation="false" %>

<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/Default.css" rel="Stylesheet" type="text/css" />
    <link href="/Content/facebox/jQuery.facebox.css" media="screen" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="StateDiv" ClientIDMode="Static">
        <asp:Label runat="server" ID="StateLabel" Text="State: "></asp:Label>
        <asp:DropDownList runat="server" ID="statesDDL" AutoPostBack="true" OnSelectedIndexChanged="AddStateTaxReceipt"></asp:DropDownList>
    </asp:Panel>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="Receipt" style="float: right; min-width: 400px; border: 1px solid black; margin-bottom: 20px;">
                    <div style="text-align: center; display: block">
                        <asp:Label runat="server" ID="CompanyLabel" Font-Bold="true" Font-Size="Large" CssClass="ReceiptFont"></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="CityLabel" Font-Bold="true" CssClass="ReceiptFont"></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="Phone" Font-Bold="true" CssClass="ReceiptFont"></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="Expiration" Font-Bold="true" CssClass="ReceiptFont"></asp:Label>
                        <br />
                        <br />
                        <asp:Image runat="server" AlternateText="Barcode Image" ImageUrl="/images/barcode.png" Height="60" Width="250px" ImageAlign="AbsMiddle" />
                        <br />
                        <br />
                        <asp:PlaceHolder ID="resultHolder" runat="server"></asp:PlaceHolder>
                        <br />
                        <asp:PlaceHolder ID="GroceryHolder" runat="server"></asp:PlaceHolder>
                        <br />
                        <asp:PlaceHolder ID="ElectronicHolder" runat="server"></asp:PlaceHolder>
                        <br />
                        <asp:PlaceHolder ID="ClothingHolder" runat="server"></asp:PlaceHolder>
                        <br />
                        <asp:PlaceHolder ID="KitchenHolder" runat="server"></asp:PlaceHolder>
                        <br />
                        <br />
                        <div>
                            <asp:Label runat="server" Text="Subtotal"></asp:Label>
                            <asp:Label runat="server" ID="SubtotalLabel" Text="$0.00"></asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Tax"></asp:Label>
                            <asp:Label runat="server" ID="StateTaxLabel"></asp:Label>
                            <br />
                            <asp:Label runat="server" Text="Total"></asp:Label>
                            <asp:Label runat="server" ID="TotalLabel" Text="$0.00"></asp:Label>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="NewItembtn" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ExistingItembtn" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="statesDDL" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <div id="newItemContainer">
            <asp:Label runat="server" ID="NewItemLabel" Text="Add a new item" Font-Bold="true" Font-Size="Large"></asp:Label>
            <div>
                <asp:Label runat="server" Text="Item Name"></asp:Label>
                <asp:TextBox runat="server" ID="NewItemNameTextbox" TabIndex="1" pattern="[A-Za-z\s]+" title="Only letters are valid" placeholder="Item Name" style="margin-left:2px"></asp:TextBox>
                <br />
                <asp:Label runat="server" Text="Item Price" CssClass="labelWidth"></asp:Label>
                $<asp:TextBox runat="server" ID="ItemPriceTextbox" TabIndex="2" placeholder="Item Price" pattern="^\d+(\.|\,)\d{2}$" title="Please Only numbers in a currency format such as '2.00'"></asp:TextBox>
                <br />
                <asp:Label runat="server" Text="Quantity" CssClass="QuantityLabel"></asp:Label>
                <asp:DropDownList runat="server" ID="QuantityDDL">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                </asp:DropDownList>
                <div class="ReceiptButton">
                    <asp:Button runat="server" ID="NewItembtn" Text="Add to Receipt" OnClientClick="return StateCheck();" OnClick="AddNewItem" />
                </div>
            </div>
        </div>
        <div id="ExistingInventoryItem">
            <asp:Label runat="server" ID="ExistingItemLabel" Text="Add an existing inventory item" Font-Bold="true" Font-Size="Large" ClientIDMode="Static"></asp:Label>
            <div>
                <asp:Label runat="server" Text="Category" CssClass="labelWidth"></asp:Label>
                <asp:DropDownList runat="server" ID="CategoryCascDDL"></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown runat="server" ID="CategoriesCasc" ServicePath="CascDropDown.asmx" ServiceMethod="GetCategory" TargetControlID="CategoryCascDDL" PromptText="Select Category" Category="CategoryID" />
                <br />
                <asp:Label runat="server" Text="Item" CssClass="labelWidth"></asp:Label>
                <asp:DropDownList runat="server" ID="ItemCascDDL"></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown runat="server" ID="ItemsCasc" ServicePath="CascDropDown.asmx" ServiceMethod="GetItems" TargetControlID="ItemCascDDL" PromptText="Select Items" Category="ItemID" ParentControlID="CategoryCascDDL" />
                <br />
                <asp:Label runat="server" Text="Quantity" CssClass="labelWidth"></asp:Label>
                <asp:DropDownList runat="server" ID="QuantityCascDDL"></asp:DropDownList>
                <ajaxToolkit:CascadingDropDown runat="server" ID="QuantityCasc" ServicePath="CascDropDown.asmx" ServiceMethod="GetQuantity" TargetControlID="QuantityCascDDL" PromptText="Select Quantity" Category="Quantity" ParentControlID="ItemCascDDL" />
            </div>
            <div class="ReceiptButton">
                <asp:Button runat="server" ID="ExistingItembtn" Text="Add to Receipt" OnClientClick="return StateCheck();" OnClick="AddExistingItem" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="scripts" ContentPlaceHolderID="ExtraScripts" runat="server">
    <script src="/Scripts/jquery.facebox.min.js" type="text/javascript"></script>
    <script src="/Scripts/Default.js" type="text/javascript"></script>
</asp:Content>
