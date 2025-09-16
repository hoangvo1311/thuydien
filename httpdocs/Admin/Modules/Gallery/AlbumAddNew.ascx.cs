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
using System.IO;
using MMC.VTT.BLL;

public partial class Admin_Modules_Gallery_AlbumAddNew : System.Web.UI.UserControl
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
    public void ClearData()
    {
        this.AlbumID = 0;
        this.txtAlbumName.Text = "";
        this.txtDescription.Text = "";
        this.ddlPublished.Text = "True";
       
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
          
            string GalleryPath = Server.MapPath("~/Gallery");
            string FolderName = PublicFuntions.Convert2PathName(this.txtAlbumName.Text);
            

            if (!Directory.Exists(Server.MapPath("~/Gallery")))
                Directory.CreateDirectory(Server.MapPath("~/Gallery"));

            if (!Directory.Exists(Server.MapPath("~/Gallery/" + FolderName)))
                Directory.CreateDirectory(Server.MapPath("~/Gallery/" + FolderName));

            BAlbums obj = new BAlbums();

            AlbumID = obj.insertAlbum(this.txtAlbumName.Text, this.txtDescription.Text, _IsPublished, FolderName, UserName);

            return AlbumID > 0;
        }
        else
        {
            PublicFuntions.Alert(this, myList);
            return false;
        }
    }
}
