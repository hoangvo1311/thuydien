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

public partial class Admin_Modules_Donation_DonationAddnew : System.Web.UI.UserControl
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

            DonationID = obj.CompanyDonate(this.txtName.Text.Trim(), this.txtAmount.Text.Trim(), this.txtNote.Text.Trim());
        else
            DonationID = obj.PersonalDonate(this.txtName.Text.Trim(),this.txtSchoolYear.Text, this.txtAmount.Text.Trim(), this.txtNote.Text.Trim());


        return DonationID > 0;
    }
}
