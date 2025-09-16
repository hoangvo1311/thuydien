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
using MMC.VTT.DAL;

public partial class Admin_Modules_Articles_ArticleUpdate : System.Web.UI.UserControl
{
    public string UserName;

    public int ArticleID
    {
        get
        {
            int _ArticleID = 0;
            int.TryParse(hdArticleID.Value, out _ArticleID);

            return _ArticleID;
        }
        set
        {
            hdArticleID.Value = value.ToString();
        }

    }
    public int CategoryID
    {
        get
        {
            int _CategoryID = 0;
            int.TryParse(hdCategoryID.Value, out _CategoryID);

            return _CategoryID;
        }
        set
        {
            hdCategoryID.Value = value.ToString();
        }

    }
    private bool _Enabled = false;
    public bool AllowEnabled
    {
        get { return _Enabled; }
        set { _Enabled = value; }
    }
    private bool _Approved = false;
    public bool AllowApproved
    {
        get { return _Approved; }
        set { _Approved = value; }
    }
    private bool _Published = false;
    public bool AllowPublished
    {
        get { return _Published; }
        set { _Published = value; }
    }
    private bool _Edit = false;
    public bool AllowEdit
    {
        get { return _Edit; }
        set { _Edit = value; }
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
        BArticles obj = new BArticles();

        DataRow dr = obj.LoadByID(ArticleID);

        if (dr != null)
        {
            this.hdCategoryID.Value = dr[vtt_Articles.ColumnNames.CategoryID].ToString();
            this.txtTitle.Text = dr[vtt_Articles.ColumnNames.Title].ToString();
            this.ddlApproved.Text = (bool)dr[vtt_Articles.ColumnNames.Approved] ? "Yes" : "No";
            this.ddlPublished.Text = (bool)dr[vtt_Articles.ColumnNames.Published] ? "Yes" : "No";
            this.txtAbstract.Value = Server.HtmlDecode(dr[vtt_Articles.ColumnNames.Abstract].ToString());
            this.txtBody.Value = Server.HtmlDecode(dr[vtt_Articles.ColumnNames.Body].ToString());

        }

        BComments objComments = new BComments();
        //Load List Comments
        DataTable dtComments = objComments.listComments(this.ArticleID);

        rptComments.DataSource = dtComments;
        rptComments.DataBind();
    }
    public void ClearData()
    {
        this.hdArticleID.Value = "0";
        this.hdCategoryID.Value = "0";
        this.txtTitle.Text = string.Empty;
        this.ddlApproved.Text = "Yes" ;
        this.txtAbstract.Value = string.Empty;
        this.txtBody.Value = string.Empty;
    }
    public bool Save(int _CategoryID)
    {
        bool isValid = true;

        ArrayList myList = new ArrayList();


        //Validation

        if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
        {
            isValid = false;
            myList.Add(Articles.V_ATC02);
        }

        if (string.IsNullOrEmpty(txtAbstract.Value.Trim()))
        {
            isValid = false;
            myList.Add(Articles.V_ATC04);
        }
        else if (txtAbstract.Value.Trim().Length > 4000)
        {
            isValid = false;
            myList.Add(Articles.V_ATC03);
        }

        if (string.IsNullOrEmpty(txtBody.Value.Trim()))
        {
            isValid = false;
            myList.Add(Articles.V_ATC05);
        }

        if (!isValid)
        {
            PublicFuntions.Alert(this, myList);
            return false;
        }

        //Insert
        bool _Approved = (ddlApproved.SelectedValue.ToLower() == "yes") ? true : false;
        bool _Published = (ddlPublished.SelectedValue.ToLower() == "yes") ? true : false;
        
        BArticles obj = new BArticles();

        return obj.updateArticle(this.ArticleID, _CategoryID, this.txtTitle.Text.Trim(), Server.HtmlEncode(txtAbstract.Value.Trim()),
                                    Server.HtmlEncode(txtBody.Value.Trim()), _Approved, false, _Published);
    }

    protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            BComments obj = new BComments();

            if (obj.deleteComment(int.Parse(e.CommandArgument.ToString())))
            {
                DataTable dtComments = obj.listComments(this.ArticleID);

                rptComments.DataSource = dtComments;
                rptComments.DataBind();

            }
        }
    }
}
