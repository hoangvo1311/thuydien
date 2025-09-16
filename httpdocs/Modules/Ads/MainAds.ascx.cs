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

public partial class Modules_Ads_MainAds : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BAds obj = new BAds();
        DataTable dt = obj.Cache_LoadByAdsPostion("main");

        if (dt != null)
        {
            this.rpt.DataSource = dt;
            this.rpt.DataBind();
        }
    }
}
