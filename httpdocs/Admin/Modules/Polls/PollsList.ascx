<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollsList.ascx.cs" Inherits="Admin_Modules_Polls_PollsList" %>
<%@ Import Namespace="MMC.VTT.DAL" %>
<script type="text/javascript">
        function SelectAll(id)
        {
            try
            {
                //get reference of GridView control
                var grid = document.getElementById("<%= grdPolls.ClientID %>");
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

<asp:HiddenField ID="hdSelectedID" runat="server" />
<hr class="admin-hr" />
<asp:GridView ID="grdPolls" runat="server" AllowPaging="True" AllowSorting="True"
    AlternatingRowStyle-CssClass="gridAltItem" AutoGenerateColumns="False" CssClass="grid"
    DataKeyNames="PollID" GridLines="Both" HeaderStyle-CssClass="gridHeader" PagerStyle-CssClass="grdPager"
    RowStyle-CssClass="gridItem" EmptyDataText="Không có dữ liệu" OnRowCommand="grdPolls_RowCommand" OnSorting="grdPolls_Sorting" OnPageIndexChanging="grdPolls_PageIndexChanging" OnRowDataBound="grdPolls_RowDataBound">
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
        <asp:TemplateField HeaderText="C&#226;u hỏi" SortExpression="QuestionText" >
            <ItemTemplate>
                <asp:LinkButton ID="link" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="viewDetails" ><%# Eval(vtt_Polls.ColumnNames.QuestionText) %></asp:LinkButton>

            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ng&#224;y bắt đầu" SortExpression="StartDate" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <%# DateTime.Parse(Eval(vtt_Polls.ColumnNames.StartDate).ToString()).ToString("dd/MM/yyyy")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ng&#224;y kết th&#250;c" SortExpression="EndDate" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <%# DateTime.Parse(Eval(vtt_Polls.ColumnNames.EndDate).ToString()).ToString("dd/MM/yyyy")%>
            </ItemTemplate>
        </asp:TemplateField>        
        <asp:TemplateField HeaderText="Bắt buộc" SortExpression="IsRequired" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <asp:ImageButton ID="imgNotRequired" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setRequired" ImageUrl="~/images/locked_16.gif" Visible='<%# !bool.Parse(Eval(vtt_Polls.ColumnNames.IsRequired).ToString()) %>' />
                <asp:ImageButton ID="imgRequired" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="removeRequired" ImageUrl="~/images/unlocked_16.gif" Visible='<%# bool.Parse(Eval(vtt_Polls.ColumnNames.IsRequired).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Đang sử dụng" SortExpression="IsCurrent" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <asp:ImageButton ID="imgNCurrent" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setCurrent" ImageUrl="~/images/locked_16.gif" Visible='<%# !bool.Parse(Eval(vtt_Polls.ColumnNames.IsCurrent).ToString()) %>' />
                <asp:ImageButton ID="imgCurrent" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="removeCurrent" ImageUrl="~/images/unlocked_16.gif" Visible='<%# bool.Parse(Eval(vtt_Polls.ColumnNames.IsCurrent).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Nhiều c&#226;u trả lời" SortExpression="MultiAnswer" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
            <ItemTemplate>
                <asp:ImageButton ID="imgSingleAnswer" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="setMultiAnswer" ImageUrl="~/images/locked_16.gif" Visible='<%# !bool.Parse(Eval(vtt_Polls.ColumnNames.MultiAnswer).ToString()) %>' />
                <asp:ImageButton ID="imgMultiAnswer" runat="server" CommandArgument='<%#Container.DataItemIndex%>'
                    CommandName="removeMultiAnswer" ImageUrl="~/images/unlocked_16.gif" Visible='<%# bool.Parse(Eval(vtt_Polls.ColumnNames.MultiAnswer).ToString()) %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="gridItem" />
    <HeaderStyle CssClass="gridHeader" />
    <AlternatingRowStyle CssClass="gridAltItem" />
</asp:GridView>

