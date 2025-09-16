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

public partial class DM_Userlist : System.Web.UI.Page
{
    string connectionString;
    protected int i = 1;
    protected DM_DONVI objDepts;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Danh sach nguoi dung";

        //Check permission on Page
        connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        Menus objMenu = new Menus(connectionString);
        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            if (objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "DM_Userlist.aspx").Tables[0].Rows.Count == 0)
                Response.Redirect("AdminControlPanel.aspx");

        }
        else
            Response.Redirect("login.aspx");



        objDepts = new DM_DONVI(connectionString);
        GetDepts();
    }

    private void GetDepts()
    {
        if (!Page.IsPostBack)
        {
            DataSet ds = objDepts.List();
            this.listDonvi.DataSource = ds;
            this.listDonvi.DataTextField = "Ten_donvi";
            this.listDonvi.DataValueField = "Ma_donvi";
            this.listDonvi.DataBind();
            this.panelListUsers.Visible = true;
            this.panelEditUser.Visible = false;
            ViewUsers(listDonvi.SelectedValue);
        }
    }

    protected void ListUser(object sender, EventArgs e)
    {
        string deptID = listDonvi.SelectedValue;
        ViewUsers(deptID);
    }

    protected void ViewUsers(string deptID)
    {
        DataSet ds = objDepts.ListUser(int.Parse(deptID));
        this.gridUser.DataSource = ds;
        this.gridUser.DataBind();
        this.panelListUsers.Visible = true;
        this.panelEditUser.Visible = false;
        this.panelChangePassword.Visible = false;
    }

    protected void DoDelete(object sender, ImageClickEventArgs e)
    {
        ImageButton clickedObject = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)clickedObject.NamingContainer;
        HiddenField lblUserID = (HiddenField)currentRow.FindControl("HiddenField1");
        if (lblUserID != null)
        {
            string UserID = lblUserID.Value;
            DM_USER objUser = new DM_USER(connectionString);
            objUser.Delete(int.Parse(UserID), false);
            ViewUsers(listDonvi.SelectedValue);
        }
    }

    protected void DoUnDelete(object sender, ImageClickEventArgs e)
    {
        ImageButton clickedObject = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)clickedObject.NamingContainer;
        HiddenField lblUserID = (HiddenField)currentRow.FindControl("HiddenField1");
        if (lblUserID != null)
        {
            string UserID = lblUserID.Value;
            DM_USER objUser = new DM_USER(connectionString);
            objUser.Delete(int.Parse(UserID), true);
            ViewUsers(listDonvi.SelectedValue);
        }
    }

    protected void DoChangePassword(object sender, ImageClickEventArgs e)
    {
        ImageButton clickedObject = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)clickedObject.NamingContainer;
        Label lblUserID = (Label)currentRow.FindControl("lblUserID");
        if (lblUserID != null)
        {
            DM_USER objUser = new DM_USER(connectionString);
            DataRow dr = objUser.GetDetails(int.Parse(((HiddenField)currentRow.FindControl("HiddenField1")).Value));
            this.fieldId_userchange.Value = dr["Id_User"].ToString();
            string UserID = lblUserID.Text;
            lblChangePassID.Text = UserID;
            panelChangePassword.Visible = true;
            panelEditUser.Visible = false;
            panelListUsers.Visible = false;
        }
    }

    protected void DoSetPermission(object sender, ImageClickEventArgs e)
    {
        ImageButton clickedObject = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)clickedObject.NamingContainer;
        HiddenField lblUserID = (HiddenField)currentRow.FindControl("HiddenField1");
        if (lblUserID != null)
        {
            string UserID = lblUserID.Value;
            Response.Redirect("UserPermissions.aspx?Id_user=" + UserID);
        }
    }

    protected void DoEdit(object sender, ImageClickEventArgs e)
    {
        ImageButton clickedObject = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)clickedObject.NamingContainer;
        Label lblUserID = (Label)currentRow.FindControl("lblUserID");
        if (lblUserID != null)
        {
            string UserID = lblUserID.Text;
            DM_USER objUser = new DM_USER(connectionString);
            DataRow dr = objUser.GetDetails(int.Parse(((HiddenField)currentRow.FindControl("HiddenField1")).Value));
            this.panelListUsers.Visible = false;
            this.panelEditUser.Visible = true;
            this.panelChangePassword.Visible = false;
            this.fieldId_user.Value = dr["Id_User"].ToString();
            this.fieldDeptID.Value = dr["Ma_donvi"].ToString();
            this.txtUserID.Text = dr["Ten_User"].ToString();
            this.txtLastName.Text = dr["Hoten_user"].ToString();
            this.txtPhoneNumber.Text = dr["SDT"].ToString();
            DataSet ds = objDepts.List();
            this.cmbCurrentDept.DataSource = ds;
            this.cmbCurrentDept.DataTextField = "Ten_donvi";
            this.cmbCurrentDept.DataValueField = "Ma_donvi";
            this.cmbCurrentDept.DataBind();

            foreach (ListItem itemDept in cmbCurrentDept.Items)
            {
                if (itemDept.Value == fieldDeptID.Value)
                {
                    itemDept.Selected = true;
                }
            }
        }
    }

    protected void UpdateUser(object sender, EventArgs e)
    {
        DM_USER updatedUser = new DM_USER(connectionString);
        string UserID = txtUserID.Text;
        string deptID = cmbCurrentDept.SelectedValue;
        string lastName = txtLastName.Text;
        string phoneNumber = txtPhoneNumber.Text;

        updatedUser.Update(int.Parse(this.fieldId_user.Value), UserID, lastName, int.Parse(deptID), true, phoneNumber);

        ViewUsers(deptID);
    }

    protected void ChangeUserPassword(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserID = lblChangePassID.Text;
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtPassword.Text;
            string newRePassword = txtRePassword.Text;
            if (newPassword != newRePassword)
            {
                lblErrChangePass.Visible = true;
                return;
            }
            lblErrChangePass.Visible = false;
            string encryptOldPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "SHA1");
            string encryptNewPwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "SHA1");
            DM_USER objUser = new DM_USER(connectionString);
            bool ok = objUser.ChangePassword(int.Parse(fieldId_userchange.Value), encryptOldPwd, encryptNewPwd);
            if (!ok)
            {
                lblErrorOldPass.Visible = true;
                return;
            }
            lblErrChangePass.Visible = false;
            lblErrorOldPass.Visible = false;
            ViewUsers(listDonvi.SelectedValue);
        }
    }

    protected void gridUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:DG_changeBackColor(this, true);");
            e.Row.Attributes.Add("onmouseout", "javascript:DG_changeBackColor(this,false);");
        }
    }
}
