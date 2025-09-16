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

public partial class Admin_Modules_Gallery_AlbumUpdate : System.Web.UI.UserControl
{
    public string UserName;

    public int AlbumID
    {
        get
        {
            return (ViewState["__AlbumID"] != null) ? int.Parse(ViewState["__AlbumID"].ToString()) : 0;
        }
        set
        {
            ViewState["__AlbumID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_full_name"] != null && !Session["user_full_name"].ToString().Equals(string.Empty))
        {
            this.UserName = Session["user_full_name"].ToString();

        }

    }
    public bool Save()
    {
        bool isValid = true;

        ArrayList myList = new ArrayList();


        ////Validation

        if (string.IsNullOrEmpty(txtAlbumName.Text.Trim()))
        {
            isValid = false;
            myList.Add(Albums.V_ALB01);
        }
        if (isValid)
        {
            //Insert
            bool _IsPublished = this.ddlPublished.SelectedValue == "True";


            BAlbums obj = new BAlbums();

            return obj.updateAlbum(this.AlbumID, this.txtAlbumName.Text, this.txtDescription.Text, _IsPublished);

        }
        else
        {
            PublicFuntions.Alert(this, myList);
            return false;
        }
    }
    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        BAlbums objAlbum = new BAlbums();

        DataRow drAlbum = objAlbum.LoadByID(this.AlbumID);
        
        if (drAlbum != null)
        {
            this.txtAlbumName.Text = drAlbum[vtt_Albums.ColumnNames.Name].ToString();
            this.txtDescription.Text = drAlbum[vtt_Albums.ColumnNames.Description].ToString();
            this.ddlPublished.Text = bool.Parse(drAlbum[vtt_Albums.ColumnNames.IsPublished].ToString()) ? "True" : "False";
        }
    }
}
