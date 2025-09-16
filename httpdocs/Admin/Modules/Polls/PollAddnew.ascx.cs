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

public partial class Admin_Modules_Polls_PollAddnew : System.Web.UI.UserControl
{
    public string UserName;

    public int PollID
    {
        get
        {
            int _PollID = 0;
            int.TryParse(hdPollID.Value, out _PollID);

            return _PollID;
        }
        set
        {
            hdPollID.Value = value.ToString();
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
        this.ddlIsCurrent.SelectedValue = "Yes";
        this.ddlIsRequired.SelectedValue = "No";
        this.ddlMultiAnswer.SelectedValue = "No";
        this.txtQuestionText.Text = string.Empty;
        this.dtpStartDate.setEmptyDate();
        this.dtpStartDate.setEmptyDate();
    }
    public bool Save()
    {
        bool isValid = true;

        ArrayList myList = new ArrayList();


        //Validation

        if (string.IsNullOrEmpty(txtQuestionText.Text.Trim()))
        {
            isValid = false;
            myList.Add(Polls.V_POL02);
        }

        if (dtpStartDate.isEmptyDate)
        {
            isValid = false;
            myList.Add(Polls.V_POL04);
        }
        else if (!dtpStartDate.isValidDate)
        {
            isValid = false;
            myList.Add(Polls.V_POL05);

        }

        if (dtpEndDate.isEmptyDate)
        {
            isValid = false;
            myList.Add(Polls.V_POL06);
        }
        else if (!dtpEndDate.isValidDate)
        {
            isValid = false;
            myList.Add(Polls.V_POL07);

        }

        if (dtpStartDate.isValidDate && dtpEndDate.isValidDate && dtpStartDate.SelectedDate > dtpEndDate.SelectedDate)
        {
            isValid = false;
            myList.Add(Polls.V_POL03);
        }

        if (!isValid)
        {
            PublicFuntions.Alert(this, myList);
            return false;
        }

        //Insert
        bool _IsCurrent= (ddlIsCurrent.SelectedValue.ToLower() == "yes")?true:false;
        bool _IsRequired = (ddlIsRequired.SelectedValue.ToLower() == "yes")?true:false;
        bool _MultiAnswer = (ddlMultiAnswer.SelectedValue.ToLower() == "yes")?true:false;

        BPolls obj = new BPolls();

        PollID = obj.insertPoll(txtQuestionText.Text.Trim(), _IsCurrent, _IsRequired, _MultiAnswer,
            dtpStartDate.SelectedDate, dtpEndDate.SelectedDate, this.UserName);

        return PollID > 0;
    }
}
