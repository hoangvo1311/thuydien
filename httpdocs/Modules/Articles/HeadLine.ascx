<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeadLine.ascx.cs" Inherits="Modules_Articles_HeadLine" %>
<div id="home-headline" class="box box-shadow box-content clearfix">
    <div class="row">
        <div class="col-xs-6">
            <div id="big-headline" class="clearfix">
                <h3><asp:HyperLink ID="lnkHot1" runat="server"></asp:HyperLink><%--<asp:LinkButton ID="lnkHot1" runat="server"></asp:LinkButton>--%></h3>
                <asp:Literal ID="lblAbstract1" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="col-xs-6">
            <ul class="list-unstyled headline-section">
                <li>
                    <h4><asp:HyperLink ID="lnkHot2" runat="server"></asp:HyperLink><%--<asp:LinkButton ID="lnkHot2" runat="server"></asp:LinkButton>--%><img class="badge-new" alt="new" src="images/new.gif"/></h4>
                    <asp:Literal ID="lblAbstract2" runat="server"></asp:Literal>
                </li>
                <li>
                    <%--<h4><asp:LinkButton ID="lnkHot3" runat="server"></asp:LinkButton><img class="badge-new" alt="new" src="images/new.gif"/></h4>--%>
                    <h4><asp:HyperLink ID="lnkHot3" runat="server"></asp:HyperLink><img class="badge-new" alt="new" src="images/new.gif"/></h4>                    
                    <asp:Literal ID="lblAbstract3" runat="server"></asp:Literal>
                </li>
            </ul>
        </div>
    </div>
</div>
