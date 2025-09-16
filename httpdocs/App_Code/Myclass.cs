using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for Myclass
/// </summary>
public class Myclass
{
    public void Alert(Control p, string Message)
    {
        Message = Message.Replace("\r", "").Replace("\n", "").Replace("'", "''");
        ScriptManager.RegisterClientScriptBlock(p, this.GetType(), "Alert", "alert('" + Message + "')", true);
    }
}
