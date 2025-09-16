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
using MMC.VTT.BLL;

public partial class Admin_Modules_OldMembers_OldMemberList : System.Web.UI.UserControl
{
    public ArrayList SelectedIDs
    {
        get
        {
            ArrayList _SelectedIds = new ArrayList();

            for (int i = 0; i < grdMembers.Rows.Count; i++)
            {
                bool isSelected = ((CheckBox)grdMembers.Rows[i].FindControl("chk")).Checked;
                if (isSelected)
                    _SelectedIds.Add(grdMembers.DataKeys[i].Value);
            }

            return _SelectedIds;
        }
    }
    public int SelectedID
    {
        get
        {
            return (ViewState["_SelectedID"] != null) ? (int)ViewState["_SelectedID"] : 0;
        }
        set
        {
            ViewState["_SelectedID"] = value;
        }

    }

    private bool _Delete = false;
    public bool AllowDelete
    {
        get { return _Delete; }
        set { _Delete = value; }
    }
    public delegate void EventHandler(Object obj, EventArgs e);
    public event EventHandler DetailsClick;

    private void onDetailsClick()
    {
        if (DetailsClick != null)
        {
            DetailsClick(this, new EventArgs());
        }
    }
    public override void DataBind()
    {
        BOldMembers obj = new BOldMembers();

        string strMemberType = "HS";

        if (ddlMemberType.SelectedValue == "GV")
            strMemberType = "GV";

        this.grdMembers.DataSource = obj.LoadAll(strMemberType);
        this.grdMembers.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void grdCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdMembers.PageIndex = e.NewPageIndex;
        this.DataBind();

        
    }
    protected void grdMembers_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SelectedID = int.Parse(grdMembers.SelectedValue.ToString());
        onDetailsClick();
    }
    protected void grdMembers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chk")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chk")).ClientID + "')");
        }
    }

    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }


}
