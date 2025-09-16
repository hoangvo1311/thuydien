<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesList.ascx.cs" Inherits="Modules_Categories_BackEnd_CategoriesList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>
<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdCategories.ClientID %>");
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
            catch (err)
            {}
        }
    </script>
    <label>Vị trí menu :</label>
    
<asp:DropDownList ID="ddlAdsPosition" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAdsPosition_SelectedIndexChanged">
    <asp:ListItem Value="top">Phía trên</asp:ListItem>
    <asp:ListItem Value="left"  Selected="True">B&#234;n trái</asp:ListItem>
    <asp:ListItem Value="right">B&#234;n phải</asp:ListItem>
</asp:DropDownList>
<hr class="admin-hr" />
<asp:GridView ID="grdCategories" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="gridAltItem" GridLines="Both" HeaderStyle-CssClass="gridHeader" CssClass="grid" RowStyle-CssClass="gridItem"
     DataKeyNames="CategoryID" OnRowCommand="grdCategories_RowCommand" OnRowDataBound="grdCategories_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <ItemTemplate>
                <%#Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Chọn" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <HeaderTemplate>
                <asp:CheckBox ID="chk" runat="server"/>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chk" runat="server"/>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="T&#234;n Chuy&#234;n mục">
            <ItemTemplate>
                <asp:HyperLink ID="link" runat="server"><%# Eval(vtt_Categories.ColumnNames.Title) %></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <HeaderTemplate>
                Thứ tự
                <asp:ImageButton ID="imgOrder" ToolTip="Lưu thứ tự hiển thị" runat="server" ImageUrl="../../images/tick_16.png" OnClick="imgOrder_Click" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:TextBox ID="txtThuTu_grd" runat="server" Style="text-align: center;" Text='<%# Eval(vtt_Categories.ColumnNames.SortOrder) %>'
                    Width="95%"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <HeaderTemplate>
                Khóa mục
            </HeaderTemplate>
            <ItemTemplate>
                <asp:ImageButton ID="imgLock" runat="server" ImageUrl="~/images/locked_16.gif" CommandName="Unlock" CommandArgument='<%#Container.DataItemIndex%>' Visible='<%# !bool.Parse(Eval(vtt_Categories.ColumnNames.CategoryEnabled).ToString()) %>'/>
                <asp:ImageButton ID="imgUnLock" runat="server" ImageUrl="~/images/unlocked_16.gif" CommandName="Lock" CommandArgument='<%#Container.DataItemIndex%>' Visible='<%# bool.Parse(Eval(vtt_Categories.ColumnNames.CategoryEnabled).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>