<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true"
    CodeFile="GroupsPermissions.aspx.cs" Inherits="GroupsPermissions" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="group-permission" class="panel panel-primary">
        <div class="panel-heading"><span class="icon icon-back"></span><a href="Grouplist.aspx">Về trước</a> | Cấp phát quyền sử dụng menu cho nhóm: &nbsp;[<asp:Label ID="lblGroupName" runat="server" />]</div>
        <div class="panel-body">
        <table id="tableGroupPermission" width="99%" border="0" cellpadding="0" cellspacing="0">        
        <tr>
            <td>
                &nbsp;&nbsp;Nhóm menu:
                <asp:DropDownList ID="cmbMenu" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="cmbMenu_SelectedIndexChanged">
                </asp:DropDownList>
                <hr/>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0" align="left">
                    <tr>
                        <td width="30%" align="center" valign="top">
                            <b>Menu trong nhóm</b>
                        </td>
                        <td width="20%" valign="top"></td>
                        <td width="30%" align="center" valign="top">
                            <b>Menu được sử dụng</b>
                        </td>
                        <td width="20%" valign="top"></td>
                    </tr>
                    <tr>
                        <td width="30%" valign="top">
                            <asp:ListBox ID="lstMenu" runat="server" Width="100%" Height="200px" BackColor="White"
                                CssClass="NormalListBox"></asp:ListBox>
                        </td>
                        <td width="20%" valign="top" align="center">
                            <asp:Button ID="butGrant" runat="server" Text="Cấp phát" Width="90%" OnClick="butGrant_Click" />
                            <br /><br />
                            <asp:Button ID="butRevoke" runat="server" Text="Thu hồi" Width="90%" OnClick="butRevoke_Click" />
                        </td>
                        <td width="30%" valign="top">
                            <asp:ListBox ID="lstPermitMenu" runat="server" Width="100%" Height="200px" CssClass="NormalListBox"
                                OnSelectedIndexChanged="lstPermitMenu_SelectedIndexChanged" AutoPostBack="True">
                            </asp:ListBox>
                        </td>
                        <td width="20%" valign="top">
                            <fieldset runat="server" id="fiel" visible="false">
                                <legend>Quyền cơ bản</legend>
                                <asp:CheckBox ID="ckbnhap" runat="server" Text="Nhập" AutoPostBack="true" OnCheckedChanged="ckbnhap_CheckedChanged" /><br />
                                <asp:CheckBox ID="ckbsua" runat="server" Text="Sửa" AutoPostBack="true" OnCheckedChanged="ckbsua_CheckedChanged" /><br />
                                <asp:CheckBox ID="ckbxoa" runat="server" Text="Xóa" AutoPostBack="true" OnCheckedChanged="ckbxoa_CheckedChanged" /><br />
<%--                                <asp:CheckBox ID="ckbxem" runat="server" Text="Xem" AutoPostBack="true" OnCheckedChanged="ckbxem_CheckedChanged" /><br />
--%>                                <asp:CheckBox ID="ckbEnabled" runat="server" Text="Cài đặt Hiển thị" AutoPostBack="true" OnCheckedChanged="ckbEnabled_CheckedChanged"/><br />
                                <asp:CheckBox ID="ckbApproved" runat="server" Text="Cài đặt Phê chuẩn" AutoPostBack="true" OnCheckedChanged="ckbApproved_CheckedChanged" /><br />
                                <asp:CheckBox ID="ckbPublished" runat="server" Text="Cài đặt Xuất bản" AutoPostBack="true" OnCheckedChanged="ckbPublished_CheckedChanged"/><br />
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
        </div>
    </div>


    
</asp:Content>
