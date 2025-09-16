<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdsList.ascx.cs" Inherits="Admin_Modules_Ads_AdsList" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdAds.ClientID %>");
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
            {
            }
        }
</script>
<label>Vị trí quảng cáo :</label>
<asp:DropDownList ID="ddlAdsPosition" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlAdsPosition_SelectedIndexChanged">
    <asp:ListItem Value="main" Selected="True">Ở giữa - Trang chủ</asp:ListItem>
    <asp:ListItem Value="donvitructhuoc">Đơn vị trực thuộc</asp:ListItem>
    <asp:ListItem Value="left">B&#234;n trái</asp:ListItem>
    <asp:ListItem Value="right">B&#234;n phải</asp:ListItem>
</asp:DropDownList>

<hr class="admin-hr hidden" />
<asp:GridView ID="grdAds" runat="server" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="AdsID" HeaderStyle-CssClass="gridHeader"
    RowStyle-CssClass="gridItem" OnRowCommand="grdAds_RowCommand" OnRowDataBound="grdAds_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <ItemTemplate>
                <%#Container.DataItemIndex + 1%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Chọn" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <HeaderTemplate>
                <asp:CheckBox ID="chk" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chk" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="T&#234;n quảng c&#225;o">
            <ItemTemplate>
                <asp:LinkButton ID="linkDetails" runat="server" CommandName="viewDetails" CommandArgument='<%#Container.DataItemIndex%>'><%# Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.LinkName) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Đường dẫn ảnh">
            <ItemTemplate>
                <a href="<%# Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.AdsImage) %>" target="_blank">
                    <%# Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.AdsImage) %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Li&#234;n kết đến">
            <ItemTemplate>
                <a href="<%# Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.JumpURL) %>" target="_blank">
                    <%# Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.JumpURL) %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Số lượt" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <%# Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.AdsCounter) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Hiển thị" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <asp:ImageButton ID="imgUnabled" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setEnabled" ImageUrl="~/images/locked_16.gif" Visible='<%# !bool.Parse(Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.IsEnable).ToString()) %>' />
                <asp:ImageButton ID="imgEnabled" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setUnabled" ImageUrl="~/images/unlocked_16.gif" Visible='<%# bool.Parse(Eval(MMC.VTT.DAL.vtt_Ads.ColumnNames.IsEnable).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
