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

public partial class Modules_Articles_ArticleList : System.Web.UI.UserControl
{
    public int CategoryID
    {
        get
        {
            int CategoryID = 0;
            if (Request.Params["CategoryID"] != null)
                int.TryParse(Request.Params["CategoryID"], out CategoryID);

            return CategoryID;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void DataBind()
    {

        //Load Articles
        BArticles objArticles = new BArticles();


        DataTable dtArticles = new DataTable();
        dtArticles = objArticles.Cache_LoadByParrentId(CategoryID,true);

        if (dtArticles != null)
        {
            PagedDataSource objPds = new PagedDataSource();

            objPds.DataSource = new DataView(dtArticles);
            objPds.AllowPaging = true;
            objPds.PageSize = 10;

            int currentPage = 1;
            if (Request.Params["Page"] != null)
                int.TryParse(Request.Params["Page"].ToString(), out currentPage);

            objPds.CurrentPageIndex = currentPage - 1;

            
            this.rptArticles.DataSource = objPds;
            this.rptArticles.DataBind();

            int totalPage = dtArticles.Rows.Count / 10 + 1;

            int beginPage = (currentPage / 5) * 5 + 1;
            int endPage = (beginPage + 5 < totalPage) ? beginPage + 5 : totalPage;

            DataTable pages = new DataTable();
            pages.Columns.Add("link", typeof(System.String));
            pages.Columns.Add("page", typeof(System.String));
            pages.Columns.Add("css", typeof(System.String));


            string strPath = MMC.VTT.Properties.WEB_CONFIG.ApplicationPath;
            if (strPath[strPath.Length - 1] == '/') strPath = strPath.Remove(strPath.Length - 1, 1);


            for (int p = beginPage; p < endPage; p++)
            {
                DataRow dr = pages.NewRow();

                dr["page"] = p.ToString();

                if (p == currentPage)
                {
                    dr["link"] = "#";
                    dr["css"] = "active";
                }
                else
                {
                    dr["link"] = strPath + "/NewsByCategory.aspx?CategoryID=" + CategoryID.ToString() + "&Page=" + Convert.ToString(p);
                    dr["css"] = "p-cat-lnk";
                }

                pages.Rows.Add(dr);
            }

            if (totalPage > 5)
            {
                DataRow first = pages.NewRow();
                first["page"] = "&lt;&lt;";
                first["link"] = strPath + "/NewsByCategory.aspx?CategoryID=" + CategoryID.ToString() + "&Page=1";

                pages.Rows.InsertAt(first, 0);

                if (beginPage > 5)
                {
                    DataRow pre = pages.NewRow();
                    pre["page"] = "&lt;";
                    pre["link"] = strPath + "/NewsByCategory.aspx?CategoryID=" + CategoryID.ToString() + "&Page=" + Convert.ToString(beginPage - 5);
                    pages.Rows.InsertAt(pre, 1);
                }

                if (endPage < totalPage)
                {
                    DataRow next = pages.NewRow();
                    next["page"] = "&gt;";
                    next["link"] = strPath + "/NewsByCategory.aspx?CategoryID=" + CategoryID.ToString() + "&Page=" + Convert.ToString(endPage);
                    pages.Rows.Add(next);

                    DataRow last = pages.NewRow();
                    last["page"] = "&gt;&gt;";
                    last["link"] = strPath + "/NewsByCategory.aspx?CategoryID=" + CategoryID.ToString() + "&Page=" + Convert.ToString((int)(totalPage / 5) * 5); ;
                    pages.Rows.Add(last);
                }

            }

            if (pages.Rows.Count > 0)
            {
                this.rptPager.DataSource = pages;
                this.rptPager.DataBind();
            }
        }
    }
}
