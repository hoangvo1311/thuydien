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

public partial class Modules_Articles_Home_TinMoiNhat : System.Web.UI.UserControl
{
    public string ViewDetailsURL
    {
        get
        {
            return (ViewState[this.ClientID + "_ViewDetailsURL"] != null) ? (string)ViewState[this.ClientID + "_ViewDetailsURL"] : "~/News.aspx";
        }
        set
        {
            ViewState[this.ClientID + "_ViewDetailsURL"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BArticles objArt = new BArticles();

        DataTable dtArt = objArt.Cache_LastestArticles(5);

        if (dtArt != null && dtArt.Rows.Count >= 1)
        {
            this.rptArticlesHot.DataSource = dtArt;
            this.rptArticlesHot.DataBind();
        }
    }
}
