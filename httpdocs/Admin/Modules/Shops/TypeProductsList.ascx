<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TypeProductsList.ascx.cs"
    Inherits="Admin_Modules_Products_TypeProductsList" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdProductTypes.ClientID %>");
                //variable to contain the cell of the grid
                var cell;
                
                if (grid.rows.length > 0)
                {
                    //loop starts from 1. rows[0] points to the header.
                    for (i=1; i<grid.rows.length; i++)
                    {
                        //get the reference of first column
                        cell = grid.rows[i].cells[1];
                        
                        //loop according to the number of childNodes in the cell
                        for (j=0; j<cell.childNodes.length; j++)
                        {           
                            //if childNode type is CheckBox                 
                            if (cell.childNodes[j].type =="checkbox")
                            {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                                cell.childNodes[j].checked = document.getElementById(id).checked;
                            }
                        }
                    }
                }
            }
            catch(err)
            {}
        }
</script>

<asp:GridView ID="grdProductTypes" runat="server" AllowSorting="True" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="ProductTypeID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader" RowStyle-CssClass="gridItem" EmptyDataText="Không có dữ liệu" OnRowDataBound="grdProductTypes_RowDataBound" OnRowCommand="grdProductTypes_RowCommand">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <ItemTemplate>
                <%#Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Chọn">
            <HeaderTemplate>
                <asp:CheckBox ID="chk" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chk" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="T&#234;n loại h&#224;ng">
            <ItemTemplate>
                <asp:LinkButton ID="link" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="viewDetails"><%# Eval(MMC.VTT.DAL.Shop_ProductTypes.ColumnNames.ProductTypeName) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="M&#244; tả">
            <ItemTemplate>
                <%# Eval(MMC.VTT.DAL.Shop_ProductTypes.ColumnNames.Description) %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
