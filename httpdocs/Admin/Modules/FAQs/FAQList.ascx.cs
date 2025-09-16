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

public partial class Admin_Modules_FAQ_FAQList : System.Web.UI.UserControl
{
    public ArrayList SelectedIDs
    {
        get
        {
            ArrayList _SelectedIds = new ArrayList();

            for (int i = 0; i < grdFAQs.Rows.Count; i++)
            {
                bool isSelected = ((CheckBox)grdFAQs.Rows[i].FindControl("chk")).Checked;
                if (isSelected)
                    _SelectedIds.Add(grdFAQs.DataKeys[i].Value);
            }

            return _SelectedIds;
        }
    }
    public byte SelectedID
    {
        get
        {
            return (ViewState["SelectedID"] != null) ? byte.Parse(ViewState["SelectedID"].ToString()) : (byte)0;
        }
        set
        {
            ViewState["SelectedID"] = value;
        }

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
        BFAQs obj = new BFAQs();

        this.grdFAQs.DataSource = obj.LoadAll();
        this.grdFAQs.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdFAQs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chk")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chk")).ClientID + "')");
        }

    }
    protected void grdFAQs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewDetails")
        {

            BLinks obj = new BLinks();

            int index = int.Parse(e.CommandArgument.ToString());
            this.SelectedID = byte.Parse(this.grdFAQs.DataKeys[index].Value.ToString());

            onDetailsClick();
        }
    }
}
