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

public partial class Admin_Modules_Suggest_MessageView : System.Web.UI.UserControl
{
    public int MessageID
    {
        get
        {
            return (ViewState["_MessageID"] != null) ? (int)ViewState["_MessageID"] : 0;
        }
        set
        {
            ViewState["_MessageID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public override void DataBind()
    {
        if (this.MessageID > 0)
        {
            BSuggest obj = new BSuggest();


            DataRow dr = obj.LoadByID(MessageID);

            if (dr != null)
            {
                this.txtName.Text = dr[vtt_Suggest.ColumnNames.Name].ToString();
                this.txtEmail.Text = dr[vtt_Suggest.ColumnNames.Email].ToString();
                this.txtIP.Text = dr[vtt_Suggest.ColumnNames.IP].ToString();
                this.txtSubject.Text = dr[vtt_Suggest.ColumnNames.Subject].ToString();
                this.txtContent.Text = dr[vtt_Suggest.ColumnNames.Content].ToString();
            }
        }
    }
}
