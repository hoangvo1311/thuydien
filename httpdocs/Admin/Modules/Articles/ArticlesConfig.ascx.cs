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
using System.Xml;
using MMC.VTT.DAL;

public partial class Admin_Modules_Categories_ArticlesConfig : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    public override void DataBind()
    {
        bindCombox(ddlCategories_Left);
        bindCombox(ddlCategories_Center);
        bindCombox(ddlCategories_Right);

        loadArticleConfig();
    }
    private void loadArticleConfig()
    {
        XmlDocument mod_conf = new XmlDocument();
        mod_conf.Load(Server.MapPath("~/Modules.config"));


       XmlNodeList listBlock = mod_conf.SelectNodes("//Articles//block");
       foreach (XmlNode node in listBlock)
        {
            switch (node.Attributes["id"].Value)
            {
                case "left":
                 
                    try
                    {
                        ddlCategories_Left.Text = node.SelectSingleNode("./CategoryID").InnerText;
                    }
                    catch
                    {
                        ddlCategories_Left.SelectedIndex = 0;
                    }

                    break;
                case "home_lichcongtac":
                  try
                    {
                        ddlCategories_Center.Text = node.SelectSingleNode("./CategoryID").InnerText;
                    }
                    catch
                    {
                        ddlCategories_Center.SelectedIndex = 0;
                    }

                      break;
                  case "home_vanbanmoi":

                    try
                    {
                        ddlCategories_Right.Text = node.SelectSingleNode("./CategoryID").InnerText;
                    }
                    catch
                    {
                        ddlCategories_Right.SelectedIndex = 0;
                    }
                    break;
            }
        }
        
    }

    private void bindCombox(DropDownList dll)
    {
        DataTable dtCategories = new DataTable();

        dtCategories.Columns.Add(vtt_Categories.ColumnNames.CategoryID);
        dtCategories.Columns.Add(vtt_Categories.ColumnNames.Title);

        childCategories(0, "   ", ref dtCategories);

        dll.DataSource = dtCategories;

        dll.DataValueField = vtt_Categories.ColumnNames.CategoryID;
        dll.DataTextField = vtt_Categories.ColumnNames.Title;

        dll.DataBind();
    }
    private void childCategories(int _parrentID, string _space, ref DataTable _dtCategories)
    {

        BCategories obj = new BCategories();

        DataTable dt = obj.LoadByParrentId(_parrentID);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i][vtt_Categories.ColumnNames.Title] = _space + dt.Rows[i][vtt_Categories.ColumnNames.Title].ToString();
            _dtCategories.ImportRow(dt.Rows[i]);

            childCategories((int)dt.Rows[i][vtt_Categories.ColumnNames.CategoryID], _space + "___", ref _dtCategories);
        }
    }

    public void Save()
    {
        XmlDocument mod_conf = new XmlDocument();
        mod_conf.Load(Server.MapPath("~/Modules.config"));

        XmlNodeList listBlock = mod_conf.SelectNodes("//Articles//block");
        foreach (XmlNode node in listBlock)
        {
            switch (node.Attributes["id"].Value)
            {
                case "left":
                    node.SelectSingleNode("./CategoryID").InnerText = ddlCategories_Left.SelectedValue;

                    break;
                case "home_lichcongtac":
                    node.SelectSingleNode("./CategoryID").InnerText = ddlCategories_Center.SelectedValue;
                    break;
                case "home_vanbanmoi":
                    node.SelectSingleNode("./CategoryID").InnerText = ddlCategories_Right.SelectedValue;
                    break;
            }
        }

        mod_conf.Save(Server.MapPath("~/Modules.config"));

    }
}
