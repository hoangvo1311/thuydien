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
using BaseServices;

public partial class Admin_ManagePermission : System.Web.UI.Page
{
    Menus objMenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Quan ly quyen";

        string connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        objMenu = new Menus(connectionString);
        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            if (objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManagePermission.aspx").Tables[0].Rows.Count == 0)
                Response.Redirect("AdminControlPanel.aspx");

        }
        else
            Response.Redirect("login.aspx");

    }
    protected bool getPermission(string strMenuName)
    {

        return objMenu.GetAccessMenu(Session["user_login_name"].ToString(), strMenuName).Tables[0].Rows.Count != 0;

    }
}
