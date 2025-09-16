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
using MMC.VTT.Messages;
using MMC.VTT.BLL;

public partial class Admin_Modules_FAQ_FAQAddNew : System.Web.UI.UserControl
{
    
    public string UserName;


    public int FAQID
    {
        get
        {
            return (ViewState["__FAQID"] != null) ? int.Parse(ViewState["__FAQID"].ToString()) : 0;
        }
        set
        {
            ViewState["__FAQID"] = value;
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
        this.txtQuestion.Text = "";
        this.txtAnwser.Text = "";
        this.FAQID = 0;
        
    }
    public bool Save()
    {
        bool isValid = true;

        ArrayList myList = new ArrayList();


        //Validation

        if (string.IsNullOrEmpty(txtQuestion.Text.Trim()))
        {
            isValid = false;
            myList.Add(FAQs.V_FAQ01);
        }

        if (string.IsNullOrEmpty(txtAnwser.Text.Trim()))
        {
            isValid = false;
            myList.Add(FAQs.V_FAQ02);
        }

        if (!isValid)
        {
            PublicFuntions.Alert(this, myList);
            return false;
        }

        BFAQs obj = new BFAQs();

        this.FAQID = obj.insertFAQ(this.txtQuestion.Text.Trim(), Server.HtmlEncode(this.txtAnwser.Text.Trim()), UserName);

        return FAQID > 0;

    }
}
