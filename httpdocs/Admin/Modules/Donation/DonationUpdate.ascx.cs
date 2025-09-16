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

public partial class Admin_Modules_Donation_DonationUpdate : System.Web.UI.UserControl
{
    public string UserName;

    public int DonationID
    {
        get
        {
            return (ViewState["__DonationID"] != null) ? int.Parse(ViewState["__DonationID"].ToString()) : 0;
        }
        set
        {
            ViewState["__DonationID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_full_name"] != null && !Session["user_full_name"].ToString().Equals(string.Empty))
        {
            this.UserName = Session["user_full_name"].ToString();

        }

    }
    protected void rdIsCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.trSchoolYear.Visible = !bool.Parse(rdIsCompany.SelectedValue);
    }

    public override void DataBind()
    {
        BDonate obj = new BDonate();

        DataRow dr = obj.LoadByID(DonationID);

        if (dr != null)
        {
            this.txtName.Text = dr[vtt_Donate.ColumnNames.Name].ToString();
            this.rdIsCompany.Text = dr[vtt_Donate.ColumnNames.IsCompany].ToString();
            this.trSchoolYear.Visible = !bool.Parse(dr[vtt_Donate.ColumnNames.IsCompany].ToString());
            this.txtSchoolYear.Text = dr[vtt_Donate.ColumnNames.Year].ToString();
            this.txtAmount.Text = dr[vtt_Donate.ColumnNames.Amount].ToString();
            this.txtNote.Text = dr[vtt_Donate.ColumnNames.Note].ToString();
        }
    }
    
    public void ClearData()
    {
        this.DonationID = 0;
        this.txtName.Text = "";
        this.txtSchoolYear.Text = "";
        this.txtAmount.Text = "";
        this.txtNote.Text = "";
    }
    public bool Save()
    {

        BDonate obj = new BDonate();

        if (bool.Parse(rdIsCompany.SelectedValue))

            return obj.CompanyDonateUpdate(this.DonationID, this.txtName.Text.Trim(), this.txtAmount.Text.Trim(), this.txtNote.Text.Trim());
        else
            return obj.PersonalDonateUpdate(this.DonationID, this.txtName.Text.Trim(), this.txtSchoolYear.Text, this.txtAmount.Text.Trim(), this.txtNote.Text.Trim());
    }
}
