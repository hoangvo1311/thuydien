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

public partial class Admin_Modules_Products_ProductAddnew : System.Web.UI.UserControl
{
    
    public string UserName;

    public int ProductID
    {
        get
        {
            int _ProductID = 0;
            int.TryParse(hdProductID.Value, out _ProductID);

            return _ProductID;
        }
        set
        {
            hdProductID.Value = value.ToString();
        }

    }
    private bool _Published = false;
    public bool AllowPublished
    {
        get { return _Published; }
        set { _Published = value; }
    }
    private DataTable dsImages
    {
        get
        {
            DataTable dt = new DataTable();
            DataColumn col = new DataColumn("Name");
            dt.Columns.Add(col);
            return (DataTable)ViewState["dsImages"] ?? dt;
        }
        set
        {
            ViewState["dsImages"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_full_name"] != null && !Session["user_full_name"].ToString().Equals(string.Empty))
        {
            this.UserName = Session["user_full_name"].ToString();

        }

        this.ddlPublished.Enabled = this.AllowPublished;

        string strURL = MMC.VTT.Properties.WEB_CONFIG.ApplicationPath + "/Admin/ManageFiles.aspx?sub=" + this.Button1.ClientID;
        this.btnSelectImage.Attributes["OnClick"] = " windowPopup('" + strURL + "',0,0);return false;";

    }
    public void ClearData()
    {
        this.txtProductName.Text = "";
        this.txtCurrentPrice.Text = "";
        this.txtPromotePrice.Text = "";
        this.txtDescription.Text = "";
        this.ddlPublished.Text = "false";
        this.dsImages.Clear();
        this.LargeImage.ImageUrl = "";
        this.hdLargeImage.Value = "";

        this.rptImages.DataSource = null;
        this.rptImages.DataBind();
    }
    public bool Save(int ProductTypeID)
    {
        bool isValid = true;

        ArrayList myList = new ArrayList();


        //Validation
        
        if (string.IsNullOrEmpty(txtProductName.Text.Trim()))
        {
            isValid = false;
            myList.Add(Products.V_PRD01);
        }

        int _CurentPrice = 0;
        if (!int.TryParse(this.txtCurrentPrice.Text, out _CurentPrice))
        {
            isValid = false;
            myList.Add(Products.V_PRD04);
        }

        int _PromotePrice = 0;
        if (!int.TryParse(this.txtPromotePrice.Text, out _PromotePrice))
        {
            isValid = false;
            myList.Add(Products.V_PRD05);
        }

        if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
        {
            isValid = false;
            myList.Add(Products.V_PRD03);
        }
        if (!isValid)
        {
            PublicFuntions.Alert(this, myList);
            return false;
        }

        //Insert
        BProducts obj = new BProducts();

        if (!obj.Exists_ProductType(ProductTypeID))
        {
            PublicFuntions.Alert(this, Products.V_PRD02);
            return false;
        }
        string strOtherImages = "";
        for (int i =0; i<dsImages.Rows.Count;i++)
        {
            strOtherImages += (strOtherImages != "") ? ",":"";
            strOtherImages += dsImages.Rows[i]["Name"].ToString();
        }

        ProductID = obj.Insert_Product(ProductTypeID,
                                            txtProductName.Text.Trim(),
                                            Server.HtmlEncode(txtDescription.Text.Trim()),
                                            _CurentPrice,
                                            _PromotePrice,
                                            hdLargeImage.Value,
                                            hdLargeImage.Value,
                                            strOtherImages,
                                            bool.Parse(ddlPublished.SelectedValue),
                                            UserName);



        return ProductID > 0;
    }



    protected void btnSetPrimary_Click(object sender, EventArgs e)
    {
        string strImageName = ((Button)sender).CommandArgument;

        if (!string.IsNullOrEmpty(hdLargeImage.Value.Trim()))
        {
            DataRow dr = dsImages.NewRow();
            dr["Name"] = hdLargeImage.Value;
            dsImages.Rows.Add(dr);
        }

        this.hdLargeImage.Value = strImageName;
        this.LargeImage.ImageUrl = "~/images/products/thumbnails/" + strImageName;

        //Remove Large image in list
        for (int i = 0; i < dsImages.Rows.Count; i++)
        {
            if (dsImages.Rows[i]["Name"].ToString() == strImageName)
            {
                dsImages.Rows.Remove(dsImages.Rows[i]);
                break;
            }
        }

        this.rptImages.DataSource = dsImages;
        this.rptImages.DataBind();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string strImageName = ((Button)sender).CommandArgument;

        for (int i = 0; i < dsImages.Rows.Count; i++)
        {
            if (dsImages.Rows[i]["Name"].ToString() == strImageName)
            {
                dsImages.Rows.Remove(dsImages.Rows[i]);
                break;
            }
        }

        this.rptImages.DataSource = dsImages;
        this.rptImages.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["SelectedImages"] != null)
        {
            ArrayList arrImages = (ArrayList)Session["SelectedImages"];
            DataTable dt = this.dsImages;


            for (int i = 0; i < arrImages.Count; i++)
            {
                bool haven = false;
                if (arrImages[i].ToString() == hdLargeImage.Value.Trim())
                {
                    haven = true;
                }
                else
                {
                    for (int j = 0; j < dsImages.Rows.Count; j++)
                    {
                        if (dsImages.Rows[j]["Name"].ToString() == arrImages[i].ToString())
                        {
                            haven = true;
                            break;
                        }
                    }
                }

                if (!haven)
                {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = arrImages[i].ToString();

                    dt.Rows.Add(dr);
                }
            }

            Session.Remove("SelectedImages");
            this.dsImages = dt;

            this.rptImages.DataSource = dsImages;
            this.rptImages.DataBind();

        }
    }
}
