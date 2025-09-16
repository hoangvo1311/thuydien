<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollOptionsList.ascx.cs" Inherits="Admin_Modules_Polls_PollOptionsList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Import Namespace="MMC.VTT.DAL" %>
<div>
    <div>
        <fieldset>
            <legend> Tùy chọn thăm dò </legend>
            <asp:HiddenField ID="hdPollID" runat="server" />
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        Thăm dò</td>
                    <td>
                        <asp:Label ID="lblQuestionText" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Tùy chọn</td>
                    <td>
                        <asp:TextBox ID="txtOptionText" runat="server" MaxLength="256" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        Thứ tự</td>
                    <td>
                        <asp:TextBox ID="txtSortOrder" runat="server" MaxLength="3" Width="80px">0</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="Thêm" Width="80px" OnClick="btnAdd_Click" /></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <asp:GridView ID="grdPolls" runat="server"
            AlternatingRowStyle-CssClass="gridAltItem" AutoGenerateColumns="False" CssClass="grid"
            DataKeyNames="OptionID" EmptyDataText="Không có dữ liệu" HeaderStyle-CssClass="gridHeader" RowStyle-CssClass="gridItem" OnRowDeleting="grdPolls_RowDeleting" OnRowCancelingEdit="grdPolls_RowCancelingEdit" OnRowEditing="grdPolls_RowEditing" OnRowUpdating="grdPolls_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="STT">
                    <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>            
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                     <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="C&#226;u hỏi b&#236;nh chọn" >
                    <ItemTemplate>
                           <%# Eval(vtt_PollOptions.ColumnNames.OptionText)%>
                                
                     </ItemTemplate>
                     <EditItemTemplate>
                        <asp:TextBox ID="txtOptionText" runat="server" MaxLength="256" Width="300px" Text='<%# Eval(vtt_PollOptions.ColumnNames.OptionText)%>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Thứ tự" >
                    <HeaderTemplate>
                        Thứ tự 
                        <asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/images/save.gif" OnClick="imgSave_Click"  />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtSortOrder_grd" runat="server" MaxLength="256" Width="300px" Text='<%# Eval(vtt_PollOptions.ColumnNames.SortOrder)%>'></asp:TextBox>
                                
                     </ItemTemplate>
                     <EditItemTemplate>
                        <asp:TextBox ID="txtSortOrder_grd" runat="server" MaxLength="256" Width="300px" Text='<%# Eval(vtt_PollOptions.ColumnNames.SortOrder)%>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sửa">
                     <ItemTemplate>
                            <asp:ImageButton ID="imgEdit" ImageUrl="~/images/edit.gif" runat="server" CommandName="Edit"/>                         
                     </ItemTemplate>
                     <EditItemTemplate>
                            <asp:ImageButton ID="imgSave" ImageUrl="~/images/save.gif" runat="server" CommandName="Update"/>                         
                     </EditItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                     <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Xo&#225;">
                     <ItemTemplate>
                            <asp:ImageButton ID="imgXoa" ImageUrl="~/images/delete.gif" runat="server" CommandName="Delete"/>             
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Bạn có muốn xoá?" TargetControlID="imgXoa">
                            </cc1:ConfirmButtonExtender>
                     </ItemTemplate>
                     <EditItemTemplate>
                            <asp:ImageButton ID="imgCancel" ImageUrl="~/images/close.gif" runat="server" CommandName="Cancel"/>                         
                     </EditItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                     <HeaderStyle Width="50px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                Không có tùy chọn thăm dò nào
            </EmptyDataTemplate>
    
            <RowStyle CssClass="gridItem" />
            <HeaderStyle CssClass="gridHeader" />
            <AlternatingRowStyle CssClass="gridAltItem" />
        </asp:GridView>
        
    </div>
</div>
