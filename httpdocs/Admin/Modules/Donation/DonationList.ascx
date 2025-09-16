<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DonationList.ascx.cs" Inherits="Admin_Modules_Donation_DonationList" %>
<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdDonation.ClientID %>");
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

&nbsp;<asp:RadioButtonList ID="rdIsCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdIsCompany_SelectedIndexChanged"
    RepeatDirection="Horizontal" ValidationGroup="Donation">
    <asp:ListItem Selected="True" Value="True">Cơ quan, đo&#224;n thể</asp:ListItem>
    <asp:ListItem Value="False">C&#225; nh&#226;n</asp:ListItem>
</asp:RadioButtonList>

<asp:GridView ID="grdDonation" runat="server" AllowPaging="True" AlternatingRowStyle-CssClass="gridAltItem"
    AutoGenerateColumns="False" CssClass="grid" DataKeyNames="ID" GridLines="Both"
    HeaderStyle-CssClass="gridHeader"
    OnRowDataBound="grdDonation_RowDataBound" OnSelectedIndexChanged="grdDonation_SelectedIndexChanged"
    RowStyle-CssClass="gridItem" EmptyDataText="Không có dữ liệu">
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
        <asp:TemplateField HeaderText="Người ủng hộ">
            <ItemTemplate>
                <asp:LinkButton ID="link" runat="server" CommandName="select"><%#  Eval(MMC.VTT.DAL.vtt_Donate.ColumnNames.Name) %></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Số tiền">
            <ItemTemplate>
                <%#  Eval(MMC.VTT.DAL.vtt_Donate.ColumnNames.Amount)%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ghi ch&#250;">
            <ItemTemplate>
                <%#  Eval(MMC.VTT.DAL.vtt_Donate.ColumnNames.Note)%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>
