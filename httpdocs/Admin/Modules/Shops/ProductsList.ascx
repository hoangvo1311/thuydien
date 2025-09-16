<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductsList.ascx.cs"
    Inherits="Admin_Modules_Products_ProductsList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdProducts.ClientID %>");
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

<asp:HiddenField ID="hdCategoryID" runat="server" Value="0" />
<asp:GridView ID="grdProducts" runat="server" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="ProductID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader" RowStyle-CssClass="gridItem" OnRowCommand="grdProducts_RowCommand"
    OnRowDataBound="grdProducts_RowDataBound" AllowPaging="True" OnPageIndexChanging="grdProducts_PageIndexChanging"
    OnSorting="grdProducts_Sorting" AllowSorting="True" OnSelectedIndexChanged="grdProducts_SelectedIndexChanged">
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
        <asp:TemplateField HeaderText="T&#234;n sản phẩm" SortExpression="ProductName">
            <ItemTemplate>
                <asp:LinkButton ID="link" runat="server" CommandName="viewDetails" CommandArgument='<%#Container.DataItemIndex%>'><%# Eval(Shop_Products.ColumnNames.ProductName) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Gi&#225; sản phẩm" SortExpression="CurrentPrice">
            <ItemTemplate>
                <%# Eval(Shop_Products.ColumnNames.CurrentPrice) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Gi&#225; khuyến m&#227;i" SortExpression="PromotePrice">
            <ItemTemplate>
                <%# Eval(Shop_Products.ColumnNames.PromotePrice) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ng&#224;y tạo" SortExpression="AddedDate">
            <ItemTemplate>
                <%# DateTime.Parse(Eval(Shop_Products.ColumnNames.AddedDate).ToString()).ToString("dd/MM/yyyy") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="HotProduct" HeaderText="Sản phẩm b&#225;n chạy">
            <ItemTemplate>
                <asp:ImageButton ID="imgHotProduct1" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setHotProduct" ImageUrl="~/images/locked_16.gif" Visible='<%# !bool.Parse(Eval(Shop_Products.ColumnNames.HotProduct).ToString()) %>' />
                <asp:ImageButton ID="imgHotProduct2" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="removeHotProduct" ImageUrl="~/images/unlocked_16.gif" Visible='<%# bool.Parse(Eval(Shop_Products.ColumnNames.HotProduct).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField SortExpression="IsPublished" HeaderText="Kh&#243;a">
            <ItemTemplate>
                <asp:ImageButton ID="imgUnPublished" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setPublished" ImageUrl="~/images/locked_16.gif" Visible='<%# !bool.Parse(Eval(Shop_Products.ColumnNames.IsPublished).ToString()) %>' />
                <asp:ImageButton ID="imgPublished" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="removePublished" ImageUrl="~/images/unlocked_16.gif" Visible='<%# bool.Parse(Eval(Shop_Products.ColumnNames.IsPublished).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
