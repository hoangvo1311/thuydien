<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OldMemberList.ascx.cs"
    Inherits="Admin_Modules_OldMembers_OldMemberList" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdMembers.ClientID %>");
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

Danh sách cựu&nbsp;<asp:DropDownList ID="ddlMemberType" runat="server" Width="131px" OnSelectedIndexChanged="ddlMemberType_SelectedIndexChanged"
    AutoPostBack="True">
    <asp:ListItem Value="HS" Selected="True">Học sinh</asp:ListItem>
    <asp:ListItem Value="GV">Gi&#225;o vi&#234;n</asp:ListItem>
</asp:DropDownList>
<asp:GridView ID="grdMembers" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="gridAltItem"
    GridLines="Both" HeaderStyle-CssClass="gridHeader" CssClass="grid" RowStyle-CssClass="gridItem"
    DataKeyNames="ID" AllowPaging="True" OnPageIndexChanging="grdCategories_PageIndexChanging"
    OnRowDataBound="grdMembers_RowDataBound" OnSelectedIndexChanged="grdMembers_SelectedIndexChanged">
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
        <asp:TemplateField HeaderText="Họ tên">
            <ItemTemplate>
                <asp:LinkButton ID="link" runat="server" CommandName="select"><%#  Eval(MMC.VTT.DAL.OldMembers.ColumnNames.FullName) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày sinh">
            <ItemTemplate>
                <%#  Eval(MMC.VTT.DAL.OldMembers.ColumnNames.BirthDate) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Điện thoại">
            <ItemTemplate>
                <%#  Eval(MMC.VTT.DAL.OldMembers.ColumnNames.Phone) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <%#  Eval(MMC.VTT.DAL.OldMembers.ColumnNames.Email) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Địa chỉ">
            <ItemTemplate>
                <%#  Eval(MMC.VTT.DAL.OldMembers.ColumnNames.Address) %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
