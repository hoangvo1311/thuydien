<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MesssageList.ascx.cs"
    Inherits="Admin_Modules_ContactUs_MesssageList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdMessages.ClientID %>");
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

<asp:GridView ID="grdMessages" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-CssClass="gridAltItem"
    GridLines="Both" HeaderStyle-CssClass="gridHeader" CssClass="grid" RowStyle-CssClass="gridItem"
    DataKeyNames="ID"
    AllowPaging="True" OnPageIndexChanging="grdCategories_PageIndexChanging" OnSelectedIndexChanged="grdMessages_SelectedIndexChanged" OnRowDataBound="grdMessages_RowDataBound">
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
        <asp:TemplateField HeaderText="Thông điệp">
            <ItemTemplate>
                <asp:LinkButton ID="link" runat="server" Font-Bold='<%# !bool.Parse(Eval(vtt_Suggest.ColumnNames.Readed).ToString()) %>' CommandName="select"><%#  Eval(vtt_Suggest.ColumnNames.Subject) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày gửi" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <%#  Eval(vtt_Suggest.ColumnNames.CreatedDate) %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
