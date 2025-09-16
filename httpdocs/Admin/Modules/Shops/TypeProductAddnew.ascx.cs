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

public partial class Admin_Modules_Products_TypeProductAddnew : System.Web.UI.UserControl
{
    
    public string UserName;

    public int ProductTypeID
    {
        get
        {
            int _ProductTypeID = 0;
            int.TryParse(hdProductTypeID.Value, out _ProductTypeID);

            return _ProductTypeID;
        }
        set
        {
            hdProductTypeID.Value = value.ToString();
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
        this.hdProductTypeID.Value = "0";
        this.txtProductTypeName.Text = "";
        this.txtDescription.Text = "";
    }
    public bool Save()
    {
       //Validation

        if (string.IsNullOrEmpty(txtProductTypeName.Text.Trim()))
        {
            PublicFuntions.Alert(this, ProductTypes.V_PDT01);
            return false;
        }



        BProductTypes obj = new BProductTypes();

        ProductTypeID = obj.Insert_ProductType(this.txtProductTypeName.Text, this.txtDescription.Text, UserName);

        return ProductTypeID > 0;
    }
}
