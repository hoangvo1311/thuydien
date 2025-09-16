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
using MMC.VTT.Messages;

public partial class Admin_Modules_Products_TypeProductUpdate : System.Web.UI.UserControl
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
    public override void DataBind()
    {
        BProductTypes obj = new BProductTypes();

        DataRow dr = obj.Load_ByID(ProductTypeID);

        if (dr != null)
        {
            this.txtProductTypeName.Text = dr[Shop_ProductTypes.ColumnNames.ProductTypeName].ToString();
            this.txtDescription.Text = dr[Shop_ProductTypes.ColumnNames.Description].ToString() ;
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

        return obj.Update_ProductType(this.ProductTypeID,this.txtProductTypeName.Text, this.txtDescription.Text);

    }
}
