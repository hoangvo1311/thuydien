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

public partial class Modules_Counter_Counter : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MMC.VTT.DAL.vtt_Counter obj = new MMC.VTT.DAL.vtt_Counter();        

        int number = 1234567890;
        Convert.ToDecimal(number).ToString("#,##0.00");

        if (obj.LoadByPrimaryKey(1))
            this.lblHitCounter.Text = Convert.ToDecimal(obj.Counter).ToString("#,##0");
            //this.lblHitCounter.Text = obj.Counter.ToString();
        //this.lblHitCounter.Text = (Application["HitCounter"] != null) ? Application["HitCounter"].ToString() : new MMC.VTT.BLL.BCounter(Server.MapPath("~") + "/images/").getCounterFromFile().ToString();
        //this.lblVistorOnline.Text = (Application["VisitorOnline"] != null) ? Application["VisitorOnline"].ToString() : "0";

    }
}
