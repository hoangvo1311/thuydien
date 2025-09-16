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

public partial class Modules_Ads_SubMain : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BAds obj = new BAds();
        DataTable dt = obj.Cache_LoadByAdsPostion("main");

        if (dt != null)
        {
            this.rpt.DataSource = dt;
            this.rpt.DataBind();
            this.indicatorRepeater.DataSource = dt;
            this.indicatorRepeater.DataBind();
        }
    }
    protected string GetItemClass(int itemIndex)
    {
        if (itemIndex == 0)
            return "active";
        else
            return "";
    }
    //void indicatorRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    string s = "";
    //}
    public string IndicatorClass(int itemIndex)
    {
        if (itemIndex == 0)
        {
            return "active";
        }
        else
        {
            return "";
        }
    }
}
