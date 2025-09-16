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

public partial class Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int _ArticleID = 0;
        int.TryParse(Request.Params["ArticleID"], out _ArticleID);

        if (_ArticleID <= 0)
            Response.Redirect("~");

        //Load Article Details
        BArticles obj = new BArticles();

        DataRow dr = obj.Cache_LoadByID(_ArticleID);

        this.lblTitle.Text = dr[vtt_Articles.ColumnNames.Title].ToString();
        this.lblAddedDate.Text = DateTime.Parse(dr[vtt_Articles.ColumnNames.AddedDate].ToString()).ToString("dd/MM/yyyy");
        this.lblContent.Text = Server.HtmlDecode(dr[vtt_Articles.ColumnNames.Body].ToString());


    }
}
