using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Admin_AdminVTT : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_full_name"] != null && !Session["user_full_name"].ToString().Equals(string.Empty))
        {
            this.lblUserName.Text = Session["user_full_name"].ToString();

        }
    }
    protected void linkLogOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        FormsAuthentication.SignOut();
        Response.Redirect("login.aspx");
    }
}
