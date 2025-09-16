<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAQUpdate.ascx.cs" Inherits="Admin_Modules_FAQ_FAQUpdate" %>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    EnableViewState="False" ShowMessageBox="True" ShowSummary="False" ValidationGroup="FAQ" />
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<table style="width: 100%" class="table table-condensed">
    <tr>
        <td style="width: 150px; height: 26px" valign="top">
            Câu hỏi <span style="color: red">(*)</span></td>
        <td style="height: 26px">
            <asp:TextBox ID="txtQuestion" runat="server" MaxLength="256" ValidationGroup="FAQ"
                 CssClass="form-control"></asp:TextBox><asp:RequiredFieldValidator ID="rqQuestion" runat="server"
                    ControlToValidate="txtQuestion" Display="None" ErrorMessage="Bạn chưa nhập tên liên kết"
                    SetFocusOnError="True" ValidationGroup="FAQ"></asp:RequiredFieldValidator></td>
    </tr>
    <tr style="color: #000000">
        <td style="width: 150px" valign="top">
            Trả lời <span style="color: red">(*)</span></td>
        <td>
            <FTB:FreeTextBox ID="txtAnwser" runat="server" ButtonOverImage="True" DownLevelCols="50"
                DownLevelRows="10" Height="300px" ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage,InsertRule|Cut,Copy,Paste;Undo,Redo,Printn,NetSpell"
                Width="100%">
            </FTB:FreeTextBox>
            <asp:RequiredFieldValidator ID="rqAnswer" runat="server" ControlToValidate="txtAnwser"
                Display="None" ErrorMessage="Bạn chưa nhập câu trả lời" SetFocusOnError="True"
                ValidationGroup="FAQ"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
