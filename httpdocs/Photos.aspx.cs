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

public partial class Photos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["AlbumID"] != null)
        {
            int _AlbumID = 0;
            int.TryParse(Request.Params["AlbumID"].ToString(), out _AlbumID);

            if (_AlbumID > 0)
            {
                BAlbums obj = new BAlbums();

                DataRow drAlbum = obj.LoadByID(_AlbumID);
                if (drAlbum != null)
                {
                    this.lblAlbumName.Text = drAlbum[vtt_Albums.ColumnNames.Name].ToString();
                }
            }
        }


    }
}
