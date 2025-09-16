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
using MMC.VTT.DAL;

public partial class Ads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["AdsID"] != null)
        {
            int iAdsID = 0;

            int.TryParse(Request.Params["AdsID"].ToString(), out iAdsID);
            
            BAds obj = new BAds();
            obj.addCounter(iAdsID);

            DataRow dr = obj.LoadByID(iAdsID);

            if (dr != null)
                Response.Redirect(dr[vtt_Ads.ColumnNames.JumpURL].ToString());
        }


    }
}
