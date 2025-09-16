<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAQList.ascx.cs" Inherits="Admin_Modules_FAQ_FAQList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>

<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdFAQs.ClientID %>");
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

<asp:GridView ID="grdFAQs" runat="server" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="ID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader" OnRowCommand="grdFAQs_RowCommand" OnRowDataBound="grdFAQs_RowDataBound"
    RowStyle-CssClass="gridItem" EmptyDataText="Chưa có FAQ">
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
        <asp:TemplateField HeaderText="C&#226;u hỏi">
            <ItemTemplate>
                <asp:LinkButton ID="linkDetails" runat="server" CommandName="viewDetails" CommandArgument='<%#Container.DataItemIndex%>'><%# Eval(vtt_FAQs.ColumnNames.FAQ) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
