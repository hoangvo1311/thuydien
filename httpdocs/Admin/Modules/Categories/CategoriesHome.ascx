<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesHome.ascx.cs" Inherits="Admin_Modules_Categories_CategoriesHome" %>
<asp:GridView ID="grdCategories" runat="server" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="CategoryID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader"
    RowStyle-CssClass="gridItem" OnRowCommand="grdCategories_RowCommand">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <ItemTemplate>
                <%#Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="T&#234;n Chuy&#234;n mục">
            <ItemTemplate>
                <asp:HyperLink ID="link" runat="server"><%# Eval(MMC.VTT.DAL.vw_Categories_Home.ColumnNames.Title) %></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <HeaderTemplate>
                Thứ tự
                <asp:ImageButton ID="imgOrder" runat="server" ImageUrl="../../images/tick_16.png" OnClick="imgOrder_Click" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:TextBox ID="txtThuTu_grd" runat="server" Style="text-align: center;" Text='<%# Eval(MMC.VTT.DAL.vw_Categories_Home.ColumnNames.SortOrder) %>'
                    Width="95%"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <HeaderTemplate>
                Hiển thị trang chủ
            </HeaderTemplate>
            <ItemTemplate>
            
              
               <asp:ImageButton ID="imgLock" runat="server" CommandArgument='<%#Container.DataItemIndex%>'                    
                CommandName="Unlock" ImageUrl="~/images/locked_16.gif" Visible='<%# Eval(MMC.VTT.DAL.vw_Categories_Home.ColumnNames.Home).ToString() != "1" ? true : false %>'  />
                <asp:ImageButton ID="imgUnLock" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="Lock" ImageUrl="~/images/unlocked_16.gif" Visible='<%# Eval(MMC.VTT.DAL.vw_Categories_Home.ColumnNames.Home).ToString() == "1" ? true : false %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
