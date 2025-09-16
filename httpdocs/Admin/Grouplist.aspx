<%@ Page Language="C#" MasterPageFile="~/Admin/AdminVTT.master" AutoEventWireup="true" CodeFile="Grouplist.aspx.cs" Inherits="Grouplist" Title="" %>
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
        function onl()
        {
            var ReturnValue = confirm('Bạn có muốn xóa nhóm này không?');
            document.cookie="CookieName=" + ReturnValue;
        }
    </script>

    <div class="panel panel-primary">
        <div class="panel-heading"><span class="icon icon-back"></span><a href="ManagePermission.aspx">Về trước</a> | Quản lý nhóm người sử dụng</div>
        <div class="panel-body">
            <table style="width: 100%">    
    <tr>
        <td>
            <b>Danh mục nhóm:&nbsp;</b>
            <asp:DropDownList ID="cmbGroups" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="cmbGroups_SelectedIndexChanged" OnTextChanged="cmbGroups_TextChanged">
            </asp:DropDownList> 
        </td>
        <td align="right">
            <span class="glyphicon glyphicon-plus"></span> <asp:LinkButton ID="butAddGroup" runat="server" Text="Thêm nhóm" OnClick="DoAddGroup" />&nbsp|&nbsp; 
            <span class="glyphicon glyphicon-edit"></span> <asp:LinkButton ID="butEditGroup" runat="server" Text="Sửa tên nhóm" OnClick="DoEditGroup" />&nbsp;|&nbsp; 
            <span class="glyphicon glyphicon-remove"></span> <asp:LinkButton ID="butDeleteGroup" runat="server" Text="Xóa nhóm" OnClick="DoDeleteGroup" OnClientClick="onl()"/>&nbsp;|&nbsp;
            <span class="glyphicon glyphicon-cog"></span> <asp:LinkButton ID="butGroupPermission" runat="server" Text="Cấp phát quyền" OnClick="DoGroupPermission" />                       
        </td>
    </tr>        
               
    <tr>
        <td colspan="2" align="right">                
            <table width="100%">
                <tr>                    
                    <td>
                        <asp:Panel ID="panelAddGroup" runat="server" Visible="false">
                            <br/>
                            <div class="alert alert-info alert-dismissible" role="alert">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>                              
                                <b>Tên nhóm:&nbsp;</b><asp:TextBox ID="txtGroupName" runat="server" CssClass="NormalTextBox" Width="180px" />
                                <asp:Button ID="butAdd" runat="server" CssClass="NormalButton" Width="80px" Text="Bổ sung" OnClick="butAdd_Click" />
                            </div>                            
                        </asp:Panel>                                            
                        <asp:Panel ID="panelEditGroup" runat="server" Visible="false">
                            <br/>
                            <div class="alert alert-info alert-dismissible" role="alert">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>                              
                                <b>Tên nhóm:&nbsp;</b><asp:TextBox ID="txtEditGroupName" runat="server" CssClass="NormalTextBox" Width="180px" />
                                <asp:Button ID="butEdit" runat="server" CssClass="NormalButton" Width="80px" Text="Cập nhật" OnClick="butEdit_Click" />
                                <asp:HiddenField ID="fieldEditGroupID" runat="server" Value="0" />
                            </div>                             
                        </asp:Panel>                                                                    
                    </td>
                </tr>
            </table>        
        </td>
    </tr>
    <tr>
      <td colspan="2">
          <hr/>
        <asp:GridView ID="gridUser" runat="server" 
            AutoGenerateColumns="False" 
            Width="100%" Caption="<b>Danh sách người dùng trong nhóm</b>" AllowPaging="True" OnPageIndexChanging="gridUser_PageIndexChanging" PageSize="20" OnRowDataBound="gridUser_RowDataBound">      
            <HeaderStyle  CssClass="gridHeader" />
                                <RowStyle CssClass="gridItem"></RowStyle>
                                <AlternatingRowStyle CssClass="gridAltItem"></AlternatingRowStyle>                             
            <Columns>                        
                <asp:TemplateField HeaderText="T&#234;n đăng nhập">
                    <HeaderStyle Width="25%" />
                    <ItemTemplate>
                        <asp:Label ID="lblUserID" runat="server" Text='<%#Eval("Ten_user")%>' Width="90%" />  
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Id_user") %>' />                                  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="H&#7885; t&#234;n user">
                    <HeaderStyle Width="60%" />
                    <ItemTemplate>
                        <asp:Label ID="lblLastName" runat="server" Text='<%#Eval("Hoten_user")%>' Width="60%" />                                      
                    </ItemTemplate>
                </asp:TemplateField>                            
                <asp:TemplateField>
                    <HeaderStyle Width="15%" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" ToolTip="Xoá khỏi nhóm" runat="server" ImageUrl="images/delete.png" OnClick="DoDelete"/>
                        <asp:HyperLink ID="linkUserPermissions" NavigateUrl='<%#"UserPermissions.aspx?Id_user="+Eval("Id_user")%>' runat="server">
                            <asp:Image ID="Image1" ImageUrl="images/shield_go.png" runat="server" Height="15px" />
                        </asp:HyperLink>                             
                    </ItemTemplate>                        
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle CssClass="DongDuocChon"/>
                            <PagerStyle CssClass="pager-row"></PagerStyle>
        </asp:GridView>
      </td>
     </tr>
    </table>
        </div>
    </div>


     
</asp:Content>

