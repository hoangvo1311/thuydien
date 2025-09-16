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

public partial class NewsByCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Params
        int _CategoryID = 0;
        if (Request.Params["CategoryID"] != null)
            int.TryParse(Request.Params["CategoryID"], out _CategoryID);
        
       

        //if (_CategoryID <= 0)
        //    Response.Redirect("~");

        //Set Title Page
        BCategories obj = new BCategories();
        DataRow dr = obj.LoadByID(_CategoryID);

        if (dr != null)
        {
            this.Title = PublicFuntions.ChuyenTuCoDauSangKoDau(dr[vtt_Categories.ColumnNames.Title].ToString());
        }

        this.ArticleList1.DataBind();
 
    }
}
