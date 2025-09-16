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

public partial class Admin_Modules_Ads_AdsList : System.Web.UI.UserControl
{
    public ArrayList SelectedIDs
    {
        get
        {
            ArrayList _SelectedIds = new ArrayList();

            for (int i = 0; i < grdAds.Rows.Count; i++)
            {
                bool isSelected = ((CheckBox)grdAds.Rows[i].FindControl("chk")).Checked;
                if (isSelected)
                    _SelectedIds.Add(grdAds.DataKeys[i].Value);
            }

            return _SelectedIds;
        }
    }
    public int SelectedID
    {
        get
        {
            return (ViewState["SelectedID"] != null) ? int.Parse(ViewState["SelectedID"].ToString()) : (int)0;
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
        string sPos = "main";

        if (ddlAdsPosition.SelectedIndex > 0)
            sPos = ddlAdsPosition.SelectedValue;

        BAds obj = new BAds();

        this.grdAds.DataSource = obj.LoadByAdsPostion(sPos);
        this.grdAds.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void grdAds_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Find the checkbox control in header and add an attribute
            ((CheckBox)e.Row.FindControl("chk")).Attributes.Add("onclick", "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chk")).ClientID + "')");
        }

    }
    protected void grdAds_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "viewDetails"
           || e.CommandName == "setEnabled"
           || e.CommandName == "setUnabled")
        {

            BAds obj = new BAds();

            int index = int.Parse(e.CommandArgument.ToString());
            this.SelectedID = byte.Parse(this.grdAds.DataKeys[index].Value.ToString());

            switch (e.CommandName)
            {
                case "viewDetails":
                    onDetailsClick();
                    break;
                case "setEnabled":
                    obj.setEnabled(SelectedID, true);
                    break;
                case "setUnabled":
                    obj.setEnabled(SelectedID, false);
                    break;
            }

            this.DataBind();
            //this.grdAds.DataSource = obj.LoadAll();
            //this.grdAds.DataBind();


        }

    }
    protected void ddlAdsPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DataBind();
    }
}
