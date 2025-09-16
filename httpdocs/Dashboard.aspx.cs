using System;
using System.Data;
using System.Web.UI;
using BaseServices;
using CustomServices;
using CustomServices.Model;

public partial class Dashboard : System.Web.UI.Page
{

    Menus objmenu;
    public Measurement measurement;
    string connectionString = System.Configuration.ConfigurationManager.AppSettings["KHL_THEKH"];

    protected void Page_Load(object sender, EventArgs e)
    {
		int companyID = Convert.ToInt32(Request["CompanyID"]);
        //this.lblCompanyName.Text = "Thủy điện Hoàng Anh Tô Na";
        this.Header.Title = MMC.VTT.Properties.WEB_CONFIG.ApplicationName + ":: Quan Ly Van Hanh";

        //Check permission on Page
        Menus objMenu = new Menus(connectionString);

        if (Session["user_login_name"] != null && !Session["user_login_name"].ToString().Equals(string.Empty))
        {
            if (companyID == 1 && objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "Dashboard.aspx?CompanyID=1").Tables[0].Rows.Count == 0)
			{
                Response.Redirect("AdminControlPanel.aspx");
			}
            else if (companyID == 2 && objMenu.GetAccessMenu(Session["user_login_name"].ToString(), "Dashboard.aspx?CompanyID=2").Tables[0].Rows.Count == 0)
			{
				Response.Redirect("AdminControlPanel.aspx");
			}
        }
		else
            Response.Redirect("login.aspx?ReturnUrl=Dashboard.aspx?CompanyID=" + companyID);
        

        LoadData();
  
    }

    protected void cmbPlants_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMeasurementInfo(int.Parse(cmbPlants.SelectedValue));
    }

    protected void cmbPlants_TextChanged(object sender, EventArgs e)
    {
        LoadMeasurementInfo(int.Parse(cmbPlants.SelectedValue));
    }

    protected void LoadData()
    {
        if (!Page.IsPostBack)
        {
            PlantService plantService = new PlantService(connectionString);
            int companyID = Convert.ToInt32(Request["CompanyID"]);
            DataSet dsPlants = plantService.GetPlantsByCompany(companyID);
            this.cmbPlants.DataSource = dsPlants;
            this.cmbPlants.DataTextField = "Name";
            this.cmbPlants.DataValueField = "ID";
            this.cmbPlants.DataBind();
            LoadMeasurementInfo(int.Parse(cmbPlants.SelectedValue));
        }

    }

    protected void LoadMeasurementInfo(int plantID)
    {
        MeasurementService measurementService = new MeasurementService(connectionString);
        measurement = measurementService.GetLatestMeasurementValue(plantID);
        Page.DataBind();
    }

}
