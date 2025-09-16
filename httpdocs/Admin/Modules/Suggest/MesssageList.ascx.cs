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

public partial class Admin_Modules_Suggest_MessageList : System.Web.UI.UserControl
{
    public ArrayList SelectedIDs
    {
        get
        {
            ArrayList _SelectedIds = new ArrayList();

            for (int i = 0; i < grdMessages.Rows.Count; i++)
            {
                bool isSelected = ((CheckBox)grdMessages.Rows[i].FindControl("chk")).Checked;
                if (isSelected)
                    _SelectedIds.Add(grdMessages.DataKeys[i].Value);
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
        BSuggest obj = new BSuggest();

        this.grdMessages.DataSource = obj.LoadAll();
        this.grdMessages.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdCategories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdMessages.PageIndex = e.NewPageIndex;
        BSuggest obj = new BSuggest();

        this.grdMessages.DataSource = obj.LoadAll();
        this.grdMessages.DataBind();
    }
    protected void grdMessages_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SelectedID = int.Parse(grdMessages.SelectedValue.ToString());
        onDetailsClick();
    }
    protected void grdMessages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chk")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chk")).ClientID + "')");
        }
    }
}
