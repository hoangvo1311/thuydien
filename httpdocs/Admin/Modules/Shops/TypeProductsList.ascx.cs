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

public partial class Admin_Modules_Products_TypeProductsList : System.Web.UI.UserControl
{
    public ArrayList SelectedIDs
    {
        get
        {
            ArrayList _SelectedIds = new ArrayList();

            for (int i = 0; i < grdProductTypes.Rows.Count; i++)
            {
                bool isSelected = ((CheckBox)grdProductTypes.Rows[i].FindControl("chk")).Checked;
                if (isSelected)
                    _SelectedIds.Add(grdProductTypes.DataKeys[i].Value);
            }

            return _SelectedIds;
        }
    }
    public int SelectedID
    {
        get
        {
            return (ViewState["SelectedID"] == null) ? 0 : (int)ViewState["SelectedID"];
        }
        set
        {
            ViewState["SelectedID"] = value;
        }
    }

    #region Events
    public delegate void EventHandler(Object obj, EventArgs e);
    public event EventHandler DetailsClick;

    private void onDetailsClick()
    {
        if (DetailsClick != null)
        {
            DetailsClick(this, new EventArgs());
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void DataBind()
    {
        BProductTypes obj = new BProductTypes();

        this.grdProductTypes.DataSource = obj.Load_All();
        this.grdProductTypes.DataBind();
    }
    protected void grdProductTypes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chk")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chk")).ClientID + "')");
        }


    }
    protected void grdProductTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewDetails")
        {

            int index = int.Parse(e.CommandArgument.ToString());
            this.SelectedID = int.Parse(this.grdProductTypes.DataKeys[index].Value.ToString());

            onDetailsClick();

        }
    }
}
