<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinksList.ascx.cs" Inherits="Admin_Modules_Links_LinksList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdLinks.ClientID %>");
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

<asp:GridView ID="grdLinks" runat="server" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="LinkID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader" OnRowCommand="grdLinks_RowCommand" OnRowDataBound="grdLinks_RowDataBound"
    RowStyle-CssClass="gridItem" EmptyDataText="Chưa có liên kết">
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
        <asp:TemplateField HeaderText="T&#234;n li&#234;n kết">
            <ItemTemplate>
                <asp:LinkButton ID="linkDetails" runat="server" CommandName="viewDetails" CommandArgument='<%#Container.DataItemIndex%>'><%# Eval(vtt_Links.ColumnNames.LinkName) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Đường dẫn">
            <ItemTemplate>
                <asp:HyperLink ID="link" runat="server" Target="_blank" NavigateUrl='<%# Eval(vtt_Links.ColumnNames.JumpURL) %>'><%# Eval(vtt_Links.ColumnNames.JumpURL) %></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
