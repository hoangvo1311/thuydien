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

public partial class UserPermissions : System.Web.UI.Page
{
    string connectionString;
    string UserID;
    Groups objGroups;
    DM_USER objUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Quan ly quyen";

        //Check permission on Page
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        Menus objMenu = new Menus(connectionString);
        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            if (objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "UserPermissions.aspx").Tables[0].Rows.Count == 0)
                Response.Redirect("AdminControlPanel.aspx");

        }
        else
            Response.Redirect("login.aspx");



        connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        UserID = Request.QueryString["Id_user"];
        if (UserID == null)
            Response.Redirect("DM_Userlist.aspx");
        objUser = new DM_USER(connectionString);
        DataRow dr = objUser.GetDetails(int.Parse(UserID));
        if (dr == null)
            Response.Redirect("DM_Userlist.aspx");

        this.lblUserID.Text = dr["Ten_User"].ToString();
        this.lblUserName.Text = dr["Hoten_user"].ToString();

        objGroups = new Groups(connectionString);
        LoadData();
    }

    protected void LoadData()
    {
        if (!Page.IsPostBack)
        {
            DataSet ds1 = objGroups.List();
            DataSet ds2 = objGroups.ListGroupsByUser(int.Parse(UserID));
            this.lstGroups.DataSource = ds1;
            this.lstGroups.DataTextField = "GroupName";
            this.lstGroups.DataValueField = "GroupID";
            this.lstPermitedGroups.DataSource = ds2;
            this.lstPermitedGroups.DataTextField = "GroupName";
            this.lstPermitedGroups.DataValueField = "GroupID";
            this.DataBind();
        }
    }

    protected void butAddGroup_Click(object sender, EventArgs e)
    {
        if (lstGroups.SelectedItem == null)
            return;

        string groupName = lstGroups.SelectedItem.Text;
        string groupID = lstGroups.SelectedItem.Value;
        bool ok = objGroups.AddUserToGroup(int.Parse(UserID), int.Parse(groupID));
        if (ok)
        {
            ListItem itemAdd = new ListItem(groupName, groupID);
            this.lstPermitedGroups.Items.Add(itemAdd);
        }
    }
    protected void butRemoveGroup_Click(object sender, EventArgs e)
    {
        if (lstPermitedGroups.SelectedItem == null)
            return;
        string groupID = lstPermitedGroups.SelectedItem.Value;
        bool ok = objGroups.DeleteUserFromGroup(int.Parse(UserID), int.Parse(groupID));
        if (ok)
        {
            ListItem itemRemove = lstPermitedGroups.SelectedItem;
            this.lstPermitedGroups.Items.Remove(itemRemove);
        }
    }
}
