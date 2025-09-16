<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleUpdate.ascx.cs"
    Inherits="Admin_Modules_Articles_ArticleUpdate" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:HiddenField ID="hdArticleID" runat="server" Value="0" />
<asp:HiddenField ID="hdCategoryID" runat="server" Value="0" />
<asp:ValidationSummary ID="vldSum" runat="server" DisplayMode="List" EnableViewState="False"
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Article" />
<table style="width: 100%;" class="table">
    <tr>
        <td style="width: 150px; height: 26px">
            Tiêu đề <span style="color: red">(*)</span></td>
        <td style="height: 26px">
            <asp:TextBox ID="txtTitle" MaxLength="256" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqTitle" runat="server" ErrorMessage="Bạn chưa nhập tiêu đề"
                ControlToValidate="txtTitle" Display="None" ValidationGroup="Article"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 150px;">
            Phê chuẩn</td>
        <td>
            <asp:DropDownList ID="ddlApproved" runat="server" CssClass="form-control">
                <asp:ListItem Text="Kh&#244;ng" Value="No"></asp:ListItem>
                <asp:ListItem Text="C&#243;" Value="Yes" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    
    <tr>
        <td style="width: 150px;">
            Xuất bản</td>
        <td>
            <asp:DropDownList ID="ddlPublished" runat="server" CssClass="form-control">
                <asp:ListItem Text="Kh&#244;ng" Value="No"></asp:ListItem>
                <asp:ListItem Text="C&#243;" Value="Yes" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 150px">
            Tóm tắt <span style="color: red"> (*)</span>
        </td>
        <td>
            <fckeditorv2:fckeditor id="txtAbstract" runat="server" BasePath="~/public_controls/fckeditor/"
                ToolbarSet="MyToolbar"></fckeditorv2:fckeditor>
        </td>
    </tr>
    <tr>
        <td style="width: 150px">
            Nội dung <span style="color: red"> (*)</span>
        </td>
        <td>
            <fckeditorv2:fckeditor id="txtBody" runat="server" BasePath="~/public_controls/fckeditor/"
                ToolbarSet="MyToolbar" Height="600px"></fckeditorv2:fckeditor>
        </td>
    </tr>
</table>
<div class="news-comment-title-area hidden">
    <img src="/images/bl_recent_news.gif" />
    <div class="news-comment-title">
        Các hồi đáp bài viết</div>
</div>
<asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand">
    <ItemTemplate>
        <ul class="comment-entries">
            <li>
                <asp:LinkButton ID="btnDeleteComment" runat="server" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Comments.ColumnNames.CommentID)%>'>Delete</asp:LinkButton>
                        <cc1:ConfirmButtonExtender ID="confirmDel2" runat="server" TargetControlID="btnDeleteComment" ConfirmText="Bạn có muốn xóa?">
        </cc1:ConfirmButtonExtender>

                </li>
            <li><span class="comment-username">
                <%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Comments.ColumnNames.AddedBy)%>
            </span>- <span class="comment-address">
                <%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Comments.ColumnNames.AddedByEmail)%>
            </span>&nbsp; <span class="sbox-date">(<%#DateTime.Parse(DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Comments.ColumnNames.AddedDate).ToString()).ToString("dd/MM/yyyy")%>9)</span></li>
            <li>
                <%# DataBinder.Eval(Container.DataItem, MMC.VTT.DAL.vtt_Comments.ColumnNames.Body)%>
            </li>
            <li>
                <hr class="entries-hr" />
            </li>
        </ul>
    </ItemTemplate>
</asp:Repeater>
