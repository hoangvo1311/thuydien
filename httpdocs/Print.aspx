<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Print" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print</title>
    <link href="css/print.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="header"><img src="images\vnpt_logo.jpg" /></div><!-- enddiv: header -->
        <div id="print-toolbox">
            <asp:Button ID="Button1" runat="server" Text="In trang!" OnClientClick="JavaScript:window.print();" />
        </div><!-- enddiv: print-toolbox -->
        <div id="content">
        
            <div id="news-title">
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                <span class="news-datetime">&nbsp;-&nbsp;(<asp:Label ID="lblAddedDate" runat="server" Text=""></asp:Label>)</span></div>
            <div id="news-content">
                <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
            </div>
        </div><!-- enddiv: content -->
        <div id="footer">
            <hr />
            <ul id="copyright">
                <li>Bản quyền © 2009 VNPT - THỪA THIÊN HUẾ. Tất cả các quyền đều được đăng ký</li>
                <li>® VNPT - THỪA THIÊN HUẾ giữ bản quyền nội dung trên website này.</li>
            </ul>
        </div><!-- enddiv: footer -->
    </div><!-- enddiv: container -->
    </form>
</body>
</html>
