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

public partial class Admin_Modules_Links_LinkAddnew : System.Web.UI.UserControl
{
    
    public string UserName;


    public byte LinkID
    {
        get
        {
            return (ViewState["__LinkID"] != null) ? byte.Parse(ViewState["__LinkID"].ToString()) : (byte)0;
        }
        set
        {
            ViewState["__LinkID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_full_name"] != null && !Session["user_full_name"].ToString().Equals(string.Empty))
        {
            this.UserName = Session["user_full_name"].ToString();

        }

    }
    public void ClearData()
    {
        this.txtLinkName.Text = "";
        this.txtURL.Text = "";
        this.LinkID = 0;
    }
    public bool Save()
    {
        if (string.IsNullOrEmpty(txtLinkName.Text.Trim()))
        {
            PublicFuntions.Alert(this, MMC.VTT.Messages.Links.V_LIN01);
            return false;
        }

        BLinks obj = new BLinks();

        this.LinkID = obj.insertLink(this.txtLinkName.Text.Trim(), this.txtURL.Text.Trim(), UserName);

        return LinkID > 0;

    }
}
