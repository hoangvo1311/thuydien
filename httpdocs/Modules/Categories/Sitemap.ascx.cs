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
using MMC.VTT.DAL;
using MMC.VTT.BLL;

public partial class Modules_Categories_HorizalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl ul = new HtmlGenericControl("ul");
        //ul.Attributes["class"] = "menuzord-menu";

        BCategories obj = new BCategories();

        DataTable rootMenu = obj.Cache_LoadByParrentId(0, "left");

        for (int i = 0; i < rootMenu.Rows.Count; i++)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");            
            HtmlGenericControl sub_menu = getSubMenu(int.Parse(rootMenu.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString()));
            HtmlAnchor a = new HtmlAnchor();
            a.Attributes["class"] = "main-node";
            
            if (rootMenu.Rows[i][vtt_Categories.ColumnNames.NavigateURL].ToString().Trim() == "")
                a.HRef = "~/NewsByCategory.aspx?CategoryID=" + rootMenu.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString();
            else
                a.HRef = rootMenu.Rows[i][vtt_Categories.ColumnNames.NavigateURL].ToString();

            a.InnerText = rootMenu.Rows[i][vtt_Categories.ColumnNames.Title].ToString();            
            li.Controls.Add(a);

            if (i == rootMenu.Rows.Count)
                li.Attributes.Add("class", "last-child");

            if (sub_menu.Controls.Count > 0)
            {                
                li.Controls.Add(sub_menu);
            }

            ul.Controls.Add(li);            
        }
        
        // add main conponents
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.Attributes["ID"] = "sitemap";        
        div.Controls.Add(ul);        

        PlaceHolder1.Controls.Add(div);
    }

    protected HtmlGenericControl getSubMenu(int parentID)
    {
        BCategories obj = new BCategories();

        DataTable subMenuItems = obj.LoadByParrentId(parentID);

        HtmlGenericControl ul = new HtmlGenericControl("ul");        

        if (subMenuItems != null)
        {
            for (int i = 0; i < subMenuItems.Rows.Count; i++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");

                HtmlAnchor a = new HtmlAnchor();

                if (subMenuItems.Rows[i][vtt_Categories.ColumnNames.NavigateURL].ToString().Trim() == "")
                    a.HRef = "~/NewsByCategory.aspx?CategoryID=" + subMenuItems.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString();
                else
                    a.HRef = subMenuItems.Rows[i][vtt_Categories.ColumnNames.NavigateURL].ToString();

                a.InnerHtml = subMenuItems.Rows[i][vtt_Categories.ColumnNames.Title].ToString();

                li.Controls.Add(a);

                HtmlGenericControl sub_menu = getSubMenu(int.Parse(subMenuItems.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString()));
                if (sub_menu.Controls.Count > 0)
                    li.Controls.Add(sub_menu);

                ul.Controls.Add(li);

            }
        }
        return ul;
    }
}
