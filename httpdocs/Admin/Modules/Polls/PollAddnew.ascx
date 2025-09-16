<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollAddnew.ascx.cs" Inherits="Admin_Modules_Polls_PollAddnew" %>
<%@ Register Src="~/public_controls/wcDateTimePicker.ascx" TagName="wcDateTimePicker"
    TagPrefix="uc1" %>
<table style="width:100%;">
    <tr>
        <td>
            Câu hỏi thăm dò <span style="color: red">(*)</span></td>
        <td>
            <asp:HiddenField ID="hdPollID" runat="server" />
            <asp:TextBox ID="txtQuestionText" runat="server" MaxLength="256" Width="300px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Kích hoạt <span style="color: #ff0000"></span></td>
        <td>
            <asp:DropDownList ID="ddlIsCurrent" runat="server" Width="100px">
                <asp:ListItem Text="Kh&#244;ng" Value="No"></asp:ListItem>
                <asp:ListItem Text="C&#243;" Value="Yes" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Bắt buộc <span style="color: #ff0000"></span></td>
        <td><asp:DropDownList ID="ddlIsRequired" runat="server" Width="100px">
            <asp:ListItem Selected="True" Text="Kh&#244;ng" Value="No"></asp:ListItem>
            <asp:ListItem Text="C&#243;" Value="Yes"></asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            Nhiều lựa chọn <span style="color: #ff0000"></span></td>
        <td><asp:DropDownList ID="ddlMultiAnswer" runat="server" Width="100px">
            <asp:ListItem Selected="True" Text="Kh&#244;ng" Value="No"></asp:ListItem>
            <asp:ListItem Text="C&#243;" Value="Yes"></asp:ListItem>
        </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            Ngày bắt đầu <span style="color: #ff0000">(*)</span></td>
        <td>
            <uc1:wcDateTimePicker ID="dtpStartDate" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Ngày kết thúc <span style="color: #ff0000">(*)</span></td>
        <td>
            <uc1:wcDateTimePicker ID="dtpEndDate" runat="server" />
        </td>
    </tr>
</table>