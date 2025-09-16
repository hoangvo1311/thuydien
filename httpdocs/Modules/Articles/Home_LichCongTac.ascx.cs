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
using System.Xml;

public partial class Modules_Articles_Home_LichCongTac : System.Web.UI.UserControl
{
    public string ViewDetailsURL
    {
        get
        {
            return (ViewState[this.ClientID + "_ViewDetailsURL"] != null) ? (string)ViewState[this.ClientID + "_ViewDetailsURL"] : "News.aspx";
        }
        set
        {
            ViewState[this.ClientID + "_ViewDetailsURL"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //int CategoryID = 0;
        ////Get CategoryID

        //XmlDocument mod_conf = new XmlDocument();
        //mod_conf.Load(Server.MapPath("~/Modules.config"));

        //XmlNodeList listBlock = mod_conf.SelectNodes("//Articles//block");
        //foreach (XmlNode node in listBlock)
        //{
        //    if (node.Attributes["id"].Value.ToLower() == "home_lichcongtac")
        //    {
        //        CategoryID = int.Parse(node.SelectSingleNode("./CategoryID").InnerText);
        //        break;
        //    }

        //}

        //BArticles obj = new BArticles();

        //DataRow dr = obj.Cache_LastestArticle(CategoryID);
        //if (dr != null)
        //    this.lblLichCongTac.Text = Server.HtmlDecode(dr[vtt_Articles.ColumnNames.Body].ToString());


        int CategoryID = 0;
        int ArticleID = 0;
        //Get CategoryID

        XmlDocument mod_conf = new XmlDocument();
        mod_conf.Load(Server.MapPath("~/Modules.config"));

        XmlNodeList listBlock = mod_conf.SelectNodes("//Articles//block");
        foreach (XmlNode node in listBlock)
        {
            if (node.Attributes["id"].Value.ToLower() == "home_lichcongtac")
            {
                CategoryID = int.Parse(node.SelectSingleNode("./CategoryID").InnerText);
                break;
            }

        }


        BArticles obj = new BArticles();

        this.rptArticles.DataSource = obj.Cache_LoadByParrentId(CategoryID, 5);
        this.rptArticles.DataBind();

    }
}
