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

public partial class Modules_Categories_VerticalMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PlaceHolder1.Controls.Clear();

        HtmlGenericControl ul = new HtmlGenericControl("ul");
        ul.Attributes["ID"] = "menu-content";
        ul.Attributes["class"] = "menu-content out";

        BCategories obj = new BCategories();
        
        DataTable rootMenu = obj.Cache_LoadByParrentId(0, "left");

        for (int i = 0; i < rootMenu.Rows.Count; i++)
        {
            //string css_li = "";
            //string css_a = "p-mn-txt";
            
            ////Set css a element for first child and last child
            //if (i == 0)
            //    css_a += " p-mn-first";
            //if (i == rootMenu.Rows.Count - 1)
            //    css_a += " p-mn-last"; 

            
            //Sub Menu
            HtmlGenericControl sub_menu = getSubMenu(int.Parse(rootMenu.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString()));

            //Set parent style when have children menu
            //if (sub_menu.Controls.Count > 0)
            //{
            //    css_li += (css_li == "")? "p-submenu" : " p-submenu";
            //    css_a += " menu-arrow";

            //}


            HtmlAnchor a = new HtmlAnchor();
            //a.Attributes["class"] = css_a;

            if (sub_menu.Controls.Count > 0)
            {
                a.HRef = "javascript:void(0)";
                //a.InnerText = " <span class='arrow'></span>";
                //a.Attributes["onClick"] = " return false;";
                a.InnerHtml = rootMenu.Rows[i][vtt_Categories.ColumnNames.Title].ToString() + " <span class='arrow'></span>";
            }
            else
            {
                if (rootMenu.Rows[i][vtt_Categories.ColumnNames.NavigateURL].ToString().Trim() == "")
                    a.HRef = "~/NewsByCategory.aspx?CategoryID=" + rootMenu.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString();
                else
                    a.HRef = rootMenu.Rows[i][vtt_Categories.ColumnNames.NavigateURL].ToString();
                a.InnerHtml = rootMenu.Rows[i][vtt_Categories.ColumnNames.Title].ToString();
            }
            

            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes["data-toggle"] = "collapse";
            li.Attributes["data-target"] = "#asmn" + rootMenu.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString();
            //li.Attributes["class"] = css_li;
            
            li.Controls.Add(a);
            if (sub_menu.Controls.Count > 0)
            li.Controls.Add(sub_menu);
            
            //subMenu(li, int.Parse(rootMenu.Rows[i][vtt_Categories.ColumnNames.CategoryID].ToString()));

            ul.Controls.Add(li);
            PlaceHolder1.Controls.Add(ul);
        }
    }
    protected HtmlGenericControl getSubMenu(int parentID)
    {
        BCategories obj = new BCategories();

        DataTable subMenuItems = obj.Cache_LoadByParrentId(parentID,"left");

        HtmlGenericControl ul = new HtmlGenericControl("ul");
        ul.Attributes["class"] = "sub-menu collapse in";
        ul.Attributes["ID"] = "asmn"+parentID;         

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

                //a.Attributes["class"] = "menu-vertical-link";
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
