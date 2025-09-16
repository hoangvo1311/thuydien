<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoList.ascx.cs" Inherits="Admin_Modules_Video_VideoList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdVideo.ClientID %>");
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
            catch(err){}    
        }
</script>
<hr class="admin-hr" />
<asp:GridView ID="grdVideo" runat="server" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="VideoID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader" OnRowCommand="grdVideo_RowCommand" OnRowDataBound="grdVideo_RowDataBound"
    RowStyle-CssClass="gridItem" EmptyDataText="Chưa có Video">
    <Columns>
        <asp:TemplateField HeaderText="STT" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
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
        <asp:TemplateField HeaderText="Video">
            <ItemTemplate>
                <asp:LinkButton ID="linkDetails" runat="server" CommandName="viewDetails" CommandArgument='<%#Container.DataItemIndex%>'><%# Eval(vtt_Video.ColumnNames.Title) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
