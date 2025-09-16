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
using System.IO;
using MMC.VTT.BLL;

public partial class Admin_Modules_Video_VideoAddnew : System.Web.UI.UserControl
{
    public string UserName;

    public int VideoID
    {
        get
        {
            return (ViewState["_VideoID"] != null) ? (int)ViewState["_VideoID"] : 0;
        }
        set
        {
            ViewState["_VideoID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_full_name"] != null && !Session["user_full_name"].ToString().Equals(string.Empty))
        {
            this.UserName = Session["user_full_name"].ToString();
        }
    }
    public void SetScripts()
    {
        //Hàm javascript nhận giá trị trả về của filemanager FCK.
        string script_SetURL = "function SetUrl( url, width, height, alt ){	document.getElementById('" + this.txtFile.ClientID + "').value = url ;}";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetUrl", script_SetURL, true);


        string lFileManager = "/public_controls/fckeditor/editor/filemanager/browser/default/browser.html?Type=Media&Connector=%2Fpublic_controls%2Ffckeditor%2Feditor%2Ffilemanager%2Fconnectors%2Faspx%2Fconnector.aspx";
        this.btnBrowser.OnClientClick = string.Format("windowPopup('{0}',600,500); return false;", lFileManager);
    }

    public bool Save()
    {

        BVideo obj = new BVideo();

        this.VideoID = obj.insertVideo(this.txtTitle.Text,this.lblFile.Text + this.txtFile.Text.Trim(), UserName);

        return (VideoID > 0);

    }
    public void ClearData()
    {
        this.txtTitle.Text = "";
        this.txtFile.Text = "";
        this.VideoID = 0;
    }


}
