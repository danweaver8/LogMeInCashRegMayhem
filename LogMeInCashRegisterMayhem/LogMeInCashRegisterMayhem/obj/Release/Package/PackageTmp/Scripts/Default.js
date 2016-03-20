function StateCheck()
{
    var stateSelected = $("#MainContent_statesDDL").val();
    if(stateSelected=='')
    {
        jQuery.facebox.settings.closeImage = '/Content/facebox/closelabel.png';
        jQuery.facebox.settings.loadingImage = '/Content/facebox/loading.gif';
        jQuery.facebox("Please select a state before trying to add an item. <br/> The sales tax is required.");
        return false;
    }
}