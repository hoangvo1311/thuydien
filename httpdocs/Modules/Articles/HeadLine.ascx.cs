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

public partial class Modules_Articles_HeadLine : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Load Articles
        BArticles objArticles = new BArticles();


        DataTable dtArticles = new DataTable();
        dtArticles = objArticles.Cache_HotArticles(3);

        if (dtArticles != null)
        {
            string strPath = MMC.VTT.Properties.WEB_CONFIG.ApplicationPath;
            if (strPath[strPath.Length - 1] == '/') strPath = strPath.Remove(strPath.Length - 1, 1);

            this.lnkHot1.Text = dtArticles.Rows[0][vtt_Articles.ColumnNames.Title].ToString();
            this.lnkHot1.NavigateUrl = strPath + "/News.aspx?aid=" + dtArticles.Rows[0][vtt_Articles.ColumnNames.ArticleID].ToString();
            this.lblAbstract1.Text = Server.HtmlDecode(dtArticles.Rows[0][vtt_Articles.ColumnNames.Abstract].ToString());
            
            if (dtArticles.Rows.Count >= 2)
            {
                this.lnkHot2.Text = dtArticles.Rows[1][vtt_Articles.ColumnNames.Title].ToString();
                this.lnkHot2.NavigateUrl = strPath + "/News.aspx?aid=" + dtArticles.Rows[1][vtt_Articles.ColumnNames.ArticleID].ToString();
                this.lblAbstract2.Text = Server.HtmlDecode(dtArticles.Rows[1][vtt_Articles.ColumnNames.Abstract].ToString());
            }

            if (dtArticles.Rows.Count >= 3)
            {
                this.lnkHot3.Text = dtArticles.Rows[2][vtt_Articles.ColumnNames.Title].ToString();
                this.lnkHot3.NavigateUrl = strPath + "/News.aspx?aid=" + dtArticles.Rows[2][vtt_Articles.ColumnNames.ArticleID].ToString();
                //this.lnkHot3.PostBackUrl = strPath + "/News.aspx?aid=" + dtArticles.Rows[2][vtt_Articles.ColumnNames.ArticleID].ToString();
                this.lblAbstract3.Text = Server.HtmlDecode(dtArticles.Rows[2][vtt_Articles.ColumnNames.Abstract].ToString());
            }
        }
    }
}
