<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactUs.ascx.cs" Inherits="Modules_ContactUs_ContactUs" %>

        <ul id="contact-us" class="list-unstyled">           
            <li>
                <label>
                    Họ và tên:
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtContactName" ErrorMessage="(*)" Font-Bold="True" ValidationGroup="ContactUs"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" MaxLength="50" ValidationGroup="ContactUs"></asp:TextBox>                
            </li>
            <li>
                <label>
                    Địa chỉ Email:
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                        runat="server" ControlToValidate="txtEmail" ErrorMessage="(*)"
                        Font-Bold="True" ValidationGroup="ContactUs"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50" ValidationGroup="ContactUs"></asp:TextBox>
            </li>
            <li>
                <label>
                    Tiêu đề:
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ControlToValidate="txtSubject" ErrorMessage="(*)"
                        Font-Bold="True" ValidationGroup="ContactUs"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" MaxLength="455" ValidationGroup="ContactUs"></asp:TextBox>                
            </li>
            <li>
                <label>
                        Nội dung cần liên lạc:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtContent" ErrorMessage="(*)" Font-Bold="True" ValidationGroup="ContactUs"></asp:RequiredFieldValidator>
                </label>
                <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" Columns="55" Rows="4" TextMode="MultiLine" ValidationGroup="ContactUs"></asp:TextBox>                
            </li>
            <li>
                <%--<a class="btn btn-primary">Gởi đi</a>--%>
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Gởi đi" OnClick="btnSubmit_Click" />
            </li>
        </ul>        
   