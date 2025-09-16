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
using System.Xml;
using MMC.VTT.BLL;
using MMC.VTT.DAL;

public partial class Modules_Articles_Home_GioiThieu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int CategoryID = 0;
        //Get CategoryID

        XmlDocument mod_conf = new XmlDocument();
        mod_conf.Load(Server.MapPath("~/Modules.config"));

        XmlNodeList listBlock = mod_conf.SelectNodes("//Articles//block");
        foreach (XmlNode node in listBlock)
        {
            if (node.Attributes["id"].Value.ToLower() == "home_gioithieu")
            {
                CategoryID = int.Parse(node.SelectSingleNode("./CategoryID").InnerText);
                break;
            }

        }

        BArticles obj = new BArticles();

        DataRow dr = obj.Cache_LastestArticle(CategoryID);
        if (dr != null)
        {
            this.lblTitle.Text = dr[vtt_Articles.ColumnNames.Title].ToString();
            this.lblContent.Text = Server.HtmlDecode(dr[vtt_Articles.ColumnNames.Abstract].ToString());
            this.linkViewDetails.PostBackUrl = "~/News.aspx?aid=" + dr[vtt_Articles.ColumnNames.ArticleID].ToString();

        }
    }
}
