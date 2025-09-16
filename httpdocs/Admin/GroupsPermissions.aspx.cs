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
using System.IO;

public partial class GroupsPermissions : System.Web.UI.Page
{
    string connectionString;
    int groupID;
    Groups objGroups;
    Menus objmenu;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Quan ly quyen";

        //Check permission on Page
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        Menus objMenu = new Menus(connectionString);
        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            if (objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "GroupsPermissions.aspx").Tables[0].Rows.Count == 0)
                Response.Redirect("AdminControlPanel.aspx");

        }
        else
            Response.Redirect("login.aspx");



        connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        groupID = int.Parse(Request.QueryString["GroupID"]);
        objGroups = new Groups(connectionString);
        objmenu = new Menus(connectionString);

        DataRow dr = objGroups.GetDetails(groupID);
        if (dr == null)
        {
            Response.Redirect("GroupsList.aspx");
            return;
        }
        else
        {
            this.lblGroupName.Text = dr["GroupName"].ToString();
        }
        GetMenuGroups();
        //ListMenu();
    }

    protected void GetMenuGroups()
    {
        if (!Page.IsPostBack)
        {
            this.cmbMenu.DataSource = objmenu.ListMenuGroups();
            this.cmbMenu.DataTextField = "MenuGroupName";
            this.cmbMenu.DataValueField = "MenuGroupID";
            this.cmbMenu.DataBind();
            if (cmbMenu.SelectedItem != null)
            {
                int menuGroupID = int.Parse(cmbMenu.SelectedItem.Value);
                ListMenu(menuGroupID);
            }
        }
    }

    private void ListMenu(int menuGroupID)
    {
        DataSet dsMenus = objmenu.GetMenusOfMenuGroups(menuGroupID);
        this.lstMenu.DataSource = dsMenus;
        this.lstMenu.DataTextField = "MenuName";
        this.lstMenu.DataValueField = "MenuID";
        this.lstMenu.DataBind();

        DataSet dsPermitedMenus = objmenu.ListPermitedMenusByMenuGroups(groupID, menuGroupID);
        this.lstPermitMenu.DataSource = dsPermitedMenus;
        this.lstPermitMenu.DataTextField = "MenuName";
        this.lstPermitMenu.DataValueField = "MenuID";
        this.lstPermitMenu.DataBind();
    }

    protected void butGrant_Click(object sender, EventArgs e)
    {
        if (lstMenu.SelectedItem == null)
            return;

        int grantMenuID = int.Parse(lstMenu.SelectedItem.Value);
        string grantMenuName = lstMenu.SelectedItem.Text;
        bool ok = objGroups.AssignMenuToGroup(groupID, grantMenuID);
        if (ok)
        {
            ListItem newItem = new ListItem();
            newItem.Text = grantMenuName;
            newItem.Value = grantMenuID.ToString();
            lstPermitMenu.Items.Add(newItem);
        }
    }

    protected void butRevoke_Click(object sender, EventArgs e)
    {
        if (lstPermitMenu.SelectedItem != null)
        {
            ListItem revokeItem = lstPermitMenu.SelectedItem;
            int revokeMenuID = int.Parse(revokeItem.Value);
            string revokeMenuName = revokeItem.Text;
            bool ok = objGroups.UnassignMenuToGroup(groupID, revokeMenuID);
            if (ok)
            {
                lstPermitMenu.Items.Remove(revokeItem);
            }
        }
    }

    protected void cmbMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbMenu.SelectedItem != null)
        {
            int menuGroupID = int.Parse(cmbMenu.SelectedItem.Value);
            ListMenu(menuGroupID);
            unCheck();
        }
    }

    protected void lstPermitMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.lbltest.Text = this.lstPermitMenu.SelectedValue.ToString();
        unCheck();
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        DataSet dsBaseMenu = objmenu.BasMenuQuyen(groupID, menuID);
        this.ckbnhap.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Nhap"].ToString());
        this.ckbsua.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Sua"].ToString());
        this.ckbxoa.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Xoa"].ToString());
        this.ckbEnabled.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Enabled"].ToString());
        this.ckbApproved.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Approved"].ToString());
        this.ckbPublished.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Published"].ToString());
        //this.ckbxem.Checked = bool.Parse(dsBaseMenu.Tables[0].Rows[0]["Xem"].ToString());
    }
    protected void unCheck()
    {
        this.fiel.Visible = true;
        this.ckbnhap.Checked = false;
        this.ckbsua.Checked = false;
        this.ckbxoa.Checked = false;
        this.ckbEnabled.Checked = false;
        this.ckbApproved.Checked = false;
        this.ckbPublished.Checked = false;
        //this.ckbxem.Checked = false;
    }

    protected void ckbnhap_CheckedChanged(object sender, EventArgs e)
    {
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        objmenu.UpdateBasPermissionNhap(groupID, menuID, bool.Parse(this.ckbnhap.Checked.ToString()));
    }

    protected void ckbsua_CheckedChanged(object sender, EventArgs e)
    {
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        objmenu.UpdateBasPermissionSua(groupID, menuID, bool.Parse(this.ckbsua.Checked.ToString()));
    }

    protected void ckbxoa_CheckedChanged(object sender, EventArgs e)
    {
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        objmenu.UpdateBasPermissionXoa(groupID, menuID, bool.Parse(this.ckbxoa.Checked.ToString()));
    }

    protected void ckbxem_CheckedChanged(object sender, EventArgs e)
    {
        //int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        //objmenu.UpdateBasPermissionXem(groupID, menuID, bool.Parse(this.ckbxem.Checked.ToString()));
    }
    protected void ckbEnabled_CheckedChanged(object sender, EventArgs e)
    {
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        objmenu.UpdateBasPermissionEnabled(groupID, menuID, bool.Parse(this.ckbEnabled.Checked.ToString()));
    }
    protected void ckbApproved_CheckedChanged(object sender, EventArgs e)
    {
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        objmenu.UpdateBasPermissionApproved(groupID, menuID, bool.Parse(this.ckbApproved.Checked.ToString()));
    }
    protected void ckbPublished_CheckedChanged(object sender, EventArgs e)
    {
        int menuID = int.Parse(this.lstPermitMenu.SelectedValue.ToString());
        objmenu.UpdateBasPermissionPublished(groupID, menuID, bool.Parse(this.ckbPublished.Checked.ToString()));
    }
}
