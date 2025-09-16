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

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Doi mat khau";

        if (string.IsNullOrEmpty(Session["user_login_name"].ToString()) ||
               !Request.IsAuthenticated)
            Response.Redirect("login.aspx");

        if (!Page.IsPostBack)
        {
            tb_username.Text = Session["user_login_name"].ToString().Trim();
            tb_username.ReadOnly = true;
        }
    }
    protected void cst_valid_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        BaseServices.DM_USER _user =
            new BaseServices.DM_USER(System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"].ToString());

        string decryptPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tb_oldpass.Text.Trim(), "SHA1");

        if (!_user.Authenticate(tb_username.Text.Trim(), decryptPassword))
            args.IsValid = false;
    }
    protected void bt_save_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            BaseServices.DM_USER _user =
                new BaseServices.DM_USER(System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"].ToString());

            if (_user.ChangePassword(Convert.ToInt32(_user.GetId_User(tb_username.Text.Trim()).Tables[0].Rows[0][0].ToString()),
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tb_oldpass.Text.Trim(), "SHA1"),
                System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tb_newpass.Text.Trim(), "SHA1")))
            {
                lb_stt.ForeColor = System.Drawing.Color.Black;
                lb_stt.Text = "Đổi mật khẩu thành công !";
            }
            else
            {
                lb_stt.ForeColor = System.Drawing.Color.Red;
                lb_stt.Text = "Có lỗi xảy ra, đổi mật khẩu không thành công !";
            }
        }
        else
            cst_valid.ErrorMessage = "Mật khẩu cũ không đúng !";
    }
    protected void bt_cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminControlPanel.aspx");
    }
}
