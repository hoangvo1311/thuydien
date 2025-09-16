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

public partial class Grouplist : System.Web.UI.Page
{
    protected string connectionString;
    protected Groups objGroups;
    DataSet dsUser;
    Myclass myclass = new Myclass();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Quan ly quyen";
        //Check permission on Page
        string connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        Menus objMenu = new Menus(connectionString);
        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            if (objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "Grouplist.aspx").Tables[0].Rows.Count == 0)
                Response.Redirect("AdminControlPanel.aspx");

        }
        else
            Response.Redirect("login.aspx");



        connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];
        objGroups = new Groups(connectionString);
        LoadData();
    }

    protected void LoadData()
    {
        if (!Page.IsPostBack)
        {
            DataSet dsGroups = objGroups.List();
            this.cmbGroups.DataSource = dsGroups;
            this.cmbGroups.DataTextField = "GroupName";
            this.cmbGroups.DataValueField = "GroupID";
            this.cmbGroups.DataBind();
            ListUser(int.Parse(cmbGroups.SelectedValue));
        }

    }

    protected void ListUser(int groupID)
    {
        dsUser = objGroups.ListUserByGroup(groupID);
        this.gridUser.DataSource = dsUser;
        this.gridUser.DataBind();
        this.panelAddGroup.Visible = false;
        this.panelEditGroup.Visible = false;
    }

    protected void bindGrid(int groupID)
    {
        dsUser = objGroups.ListUserByGroup(groupID);
        this.gridUser.DataSource = dsUser;
        this.gridUser.DataBind();
        this.panelAddGroup.Visible = false;
        this.panelEditGroup.Visible = false;
    }

    protected void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        ListUser(int.Parse(cmbGroups.SelectedValue));
    }

    protected void cmbGroups_TextChanged(object sender, EventArgs e)
    {
        ListUser(int.Parse(cmbGroups.SelectedValue));
    }

    protected void DoDelete(object sender, ImageClickEventArgs e)
    {
        ImageButton clickedObject = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)clickedObject.NamingContainer;
        HiddenField lblUserID = (HiddenField)currentRow.FindControl("HiddenField1");
        if (lblUserID != null)
        {
            string UserID = lblUserID.Value;
            objGroups.DeleteUserFromGroup(int.Parse(UserID), int.Parse(cmbGroups.SelectedValue));
            bindGrid(int.Parse(cmbGroups.SelectedValue));
        }
    }

    protected void DoAddGroup(object sender, EventArgs e)
    {
        this.panelAddGroup.Visible = true;
        this.panelEditGroup.Visible = false;
    }

    protected void butAdd_Click(object sender, EventArgs e)
    {
        if (txtGroupName.Text.Trim() != "")
        {
            string newGroupName = txtGroupName.Text.Trim();
            int newGroupID = objGroups.Create(txtGroupName.Text);

            if (newGroupID > 0)
            {
                ListItem newItem = new ListItem(txtGroupName.Text, newGroupID.ToString());
                this.cmbGroups.Items.Add(newItem);
                myclass.Alert(this, "Tạo nhóm thành công");
                DataSet dsGroups = objGroups.List();
                this.cmbGroups.DataSource = dsGroups;
                this.cmbGroups.DataTextField = "GroupName";
                this.cmbGroups.DataValueField = "GroupID";
                this.cmbGroups.DataBind();
                this.panelAddGroup.Visible = false;
            }
            else
            {
                myclass.Alert(this, "Tên nhóm này đã tồn tại");
                this.panelAddGroup.Visible = true;
            }
        }
    }

    protected void butEdit_Click(object sender, EventArgs e)
    {
        if (txtEditGroupName.Text.Trim() != "")
        {
            string newGroupName = txtEditGroupName.Text.Trim();
            int editGroupID = int.Parse(fieldEditGroupID.Value);
            bool ok = objGroups.Update(editGroupID, newGroupName);
            if (ok)
            {
                this.cmbGroups.SelectedItem.Text = newGroupName;
            }
            this.panelEditGroup.Visible = false;
        }
    }

    protected void DoDeleteGroup(object sender, EventArgs e)
    {
        if (cmbGroups.SelectedItem == null)
            return;
        if (Request.Cookies["CookieName"].Value == "true")
        {
            ListItem delItem = cmbGroups.SelectedItem;
            string delGroupID = delItem.Value;
            objGroups.Delete(int.Parse(delGroupID));
            cmbGroups.Items.Remove(delItem);
            LoadData();
        }
    }

    protected void DoEditGroup(object sender, EventArgs e)
    {
        this.panelAddGroup.Visible = false;
        this.panelEditGroup.Visible = true;
        this.txtEditGroupName.Text = cmbGroups.SelectedItem.Text;
        this.fieldEditGroupID.Value = this.cmbGroups.SelectedItem.Value;
    }

    protected void DoGroupPermission(object sender, EventArgs e)
    {
        string groupID = this.cmbGroups.SelectedItem.Value;
        Response.Redirect("GroupsPermissions.aspx?GroupID=" + groupID);
    }
    protected void gridUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int newPage;
        newPage = e.NewPageIndex;
        if (newPage < 0 || newPage >= gridUser.PageCount)
        {
            e.Cancel = true;
        }
        else
        {
            gridUser.PageIndex = newPage;
            gridUser.DataBind();
        }
    }
    protected void gridUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "javascript:DG_changeBackColor(this, true);");
            e.Row.Attributes.Add("onmouseout", "javascript:DG_changeBackColor(this,false);");

            ImageButton l = (ImageButton)e.Row.FindControl("imgDelete");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Bạn có muốn xóa người này ra khỏi nhóm không?')");
        }
    }
}
