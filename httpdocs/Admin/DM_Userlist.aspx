<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="DM_Userlist.aspx.cs" Inherits="DM_Userlist" Title="" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
		var lastColor;
        function DG_changeBackColor(row, highlight)
        {
        if (highlight)
        {
        lastColor = row.style.backgroundColor;
        row.style.backgroundColor = '#afa';
        }
        else
        row.style.backgroundColor = lastColor;
        }
    </script>
 <div class="panel panel-primary">
        <div class="panel-heading"><span class="icon icon-back"></span><a href="ManagePermission.aspx">Về trước</a> | Quản lý người sử dụng</div>
        <div class="panel-body">
<asp:Panel runat="server" ID="panelListUsers" Width="100%">
   
        <asp:Table id="tableListUser" runat="server" width="100%">
        <asp:TableRow>
            <asp:TableCell>
                Đơn vị:
                <asp:DropDownList ID="listDonvi" runat="server" Width="280px" AutoPostBack="true" OnSelectedIndexChanged="ListUser" />&nbsp;&nbsp;
                <asp:Button ID="butListUser" runat="server" Text="Hiển thị" OnClick="ListUser" />
            </asp:TableCell>    
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                &nbsp;
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:GridView ID="gridUser" 
                    runat="server" 
                    AllowPaging="False" 
                    AutoGenerateColumns="False"
                    GridLines="Both"
                    Width="100%" OnRowDataBound="gridUser_RowDataBound">
                    <HeaderStyle  CssClass="gridHeader" />
                                <RowStyle CssClass="gridItem"></RowStyle>
                                <AlternatingRowStyle CssClass="gridAltItem"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="STT">
                            <HeaderStyle Width="40px" />
                            <ItemTemplate>
                                <%#i++ %>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Id_user") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                            
                        <asp:TemplateField HeaderText="Họ v&#224; t&#234;n">
                            <HeaderStyle Width="250px" />                    
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Width="180px" Text='<%#Eval("Hoten_user")%>'>
                                </asp:Label>                       
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="T&#234;n đăng nhập">
                            <HeaderStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server"
                                     Text='<%#Eval("Ten_User")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                
                        <asp:TemplateField>
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" ToolTip="Sửa đổi thông tin" 
                                     runat="server" ImageUrl="Images/edit_16.png" OnClick="DoEdit" />&nbsp;
                                <asp:ImageButton ID="imgChangePassword" ToolTip="Đổi mật khẩu" 
                                     runat="server" Height="15px" ImageUrl="Images/change_password.png" OnClick="DoChangePassword" />&nbsp;                                                           
                                <asp:ImageButton ID="ImageButton1" ToolTip="Cấp phát quyền" 
                                     runat="server" Height="15px" 
                                     ImageUrl="Images/shield_go.png" OnClick="DoSetPermission" />&nbsp;                                                                                                
                                <asp:ImageButton ID="imgLock" runat="server" ImageUrl="~/images/locked_16.gif" CommandName="Unlock" Visible='<%# !bool.Parse(Eval("xoa").ToString()) %>' OnClick="DoUnDelete"/>
                                <asp:ImageButton ID="imgUnLock" runat="server" ImageUrl="~/images/unlocked_16.gif" CommandName="Lock" Visible='<%# bool.Parse(Eval("xoa").ToString()) %>' OnClick="DoDelete"/>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle CssClass="DongDuocChon"/>
                            <PagerStyle CssClass="pager-row"></PagerStyle>
                </asp:GridView>
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>    
    </asp:Panel>
        
      
<!--
Sửa đổi thông tin nhân viên (không sửa đổi mật khẩu)
-->    
    <asp:Panel ID="panelEditUser" runat="server" Width="99%" Visible="false">
    <table style="width: 100%">
        <tr>
            <td colspan="2">
                <strong>Thông tin chung</strong>
            </td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap" height="35px">
                Tên đăng nhập:</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtUserID" runat="server" CssClass="NormalTextBox form-control" Enabled="False"></asp:TextBox>
                <asp:HiddenField ID="fieldDeptID" runat="server" />
                <asp:HiddenField ID="fieldId_user" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap" height="35px">
                Họ ten user:</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtLastName" runat="server" CssClass="NormalTextBox form-control"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="TDInputLeft" nowrap="noWrap" height="35px">
                Điện thoại:</td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="NormalTextBox form-control"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 136px; height: 36px;" nowrap="noWrap">
                Đơn vị:</td>
            <td style="height: 26px;">
                <asp:DropDownList ID="cmbCurrentDept" runat="server" CssClass=" form-control" AutoPostBack="false" />                
        </tr>        
        <tr>
            <td class="TDInputLeft">
            </td>
            <td class="TDInputRight">
            </td>
        </tr> 
        <tr>
            <td class="TDInputLeft">
            </td>
            <td class="TDInputRight">
                <p style="text-align:left">
                    <asp:Button ID="butInput" runat="server" CssClass="NormalButton btn btn-primary" Text=" Cập nhật " OnClick="UpdateUser" />
                </p>                    
             </td>
                   
        </tr>
    </table>
    </asp:Panel>

    <asp:Panel ID="panelChangePassword" runat="server" Width="98%" Visible="false">
    <table style="width:100%" class="table table-bordered table-condensed table-striped">
        <tr>
            <td colspan="2" width="100%">
                <b>Sửa đổi mật khẩu</b>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="TDInputLeft" height="35px">
                Tên truy cập:
            </td>
            <td class="TDInputRight">
                <asp:Label ID="lblChangePassID" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="10pt">
                </asp:Label><asp:HiddenField ID="fieldId_userchange" runat="server" />
            </td>
        </tr>                    
        <tr>
            <td class="TDInputLeft" height="35px">
                Mật khẩu cũ:
            </td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="NormalTextBox form-control" TextMode="Password">
                </asp:TextBox>            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblErrorOldPass" runat="server" ForeColor="red" Text="Không đổi được mật khẩu!" Visible="False">
                </asp:Label>                    
            </td>
        </tr>
        <tr>
            <td class="TDInputLeft" height="35px">
                Mật khẩu mới:
            </td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="NormalTextBox form-control" TextMode="Password">
                </asp:TextBox>            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>        
        <tr>
            <td class="TDInputLeft" height="35px">
                Nhập lại mật khẩu:
            </td>
            <td class="TDInputRight">
                <asp:TextBox ID="txtRePassword" runat="server" CssClass="NormalTextBox form-control" TextMode="Password">
                </asp:TextBox>                        
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRePassword"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:Label ID="lblErrChangePass" runat="server" ForeColor="red" Text="Mật khẩu không trùng khớp!" Visible="False">
                </asp:Label>
             </td>
        </tr>                
        <tr>
            <td class="TDInputLeft">
            </td>
            <td class="TDInputRight">
                <asp:Button ID="butChangePassword" runat="server" CssClass="NormalButton btn btn-primary" Text="Đổi mật khẩu" OnClick="ChangeUserPassword" />
             </td>
        </tr>
    </table>                        
    </asp:Panel>
        
 </div>
    </div> 

</asp:Content>

