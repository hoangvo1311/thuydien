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

public partial class Admin_AdminControlPanel : System.Web.UI.Page
{
    Menus objMenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + " :: Admin";

        string connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        objMenu = new Menus(connectionString);
        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            string tam = Request.Url.ToString();
            string[] temp = tam.Split('/');
            string id = temp[temp.Length - 1];

            //Kiem quyen tren trang
            //if (objMenu.GetAccessMenu(Session["user_login_name"].ToString(), id).Tables[0].Rows.Count == 0)
            //    Response.Redirect("AdminControlPanel.aspx");

            this.ManageCategories.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageCategories.aspx").Tables[0].Rows.Count != 0;
            this.ManageArticles.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageArticles.aspx").Tables[0].Rows.Count != 0;
            this.ManagePolls.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManagePolls.aspx").Tables[0].Rows.Count != 0;
            this.ManageVideo.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageVideo.aspx").Tables[0].Rows.Count != 0;
            this.ManagePermission.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManagePermission.aspx").Tables[0].Rows.Count != 0;
            this.ManageLinks.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageLinks.aspx").Tables[0].Rows.Count != 0;
            this.ManageFAQs.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageFAQs.aspx").Tables[0].Rows.Count != 0;
            this.ManageSuggest.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageSuggest.aspx").Tables[0].Rows.Count != 0;
            this.ManageContactUs.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageContactUs.aspx").Tables[0].Rows.Count != 0;
            this.ManageAds.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageAds.aspx").Tables[0].Rows.Count != 0;
            this.ManageGallery.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageGallery.aspx").Tables[0].Rows.Count != 0;
            this.ManageOldMembers.Visible = objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "ManageOldMembers.aspx").Tables[0].Rows.Count != 0;
        }
        else
            Response.Redirect("login.aspx");

        Session.Remove("Position");
    }

    protected bool getPermission(string strMenuName)
    {

        return objMenu.GetAccessMenu(Session["user_login_name"].ToString(), strMenuName).Tables[0].Rows.Count != 0;

    }
}
