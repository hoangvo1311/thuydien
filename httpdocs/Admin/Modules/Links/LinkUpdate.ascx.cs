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

public partial class Admin_Modules_Links_LinkUpdate : System.Web.UI.UserControl
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
    public override void DataBind()
    {
        BLinks obj = new BLinks();

        DataRow dr = obj.LoadByID(this.LinkID);

        if (dr != null)
        {
            this.txtLinkName.Text = dr[vtt_Links.ColumnNames.LinkName].ToString();
            this.txtURL.Text = dr[vtt_Links.ColumnNames.JumpURL].ToString();
        }
    }
    public void ClearData()
    {
        this.txtLinkName.Text = "";
        this.txtURL.Text = "";
    }
    public bool Save()
    {
        if (string.IsNullOrEmpty(txtLinkName.Text.Trim()))
        {
            PublicFuntions.Alert(this, MMC.VTT.Messages.Links.V_LIN01);
            return false;
        }

        BLinks obj = new BLinks();

        return obj.updateLink(this.LinkID, this.txtLinkName.Text, this.txtURL.Text);
    }
}
