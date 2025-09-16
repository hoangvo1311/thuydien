using System;
using System.Data;
using System.Data.SqlClient;
using CustomServices.Model;

namespace CustomServices
{
    public class PlantService : CustomDBObject
    {
        public PlantService(string connectionString)
             : base(connectionString)
        {
        }

        public DataSet GetPlantsByCompany(int companyID)
        {
            DataSet dataSet = new DataSet();
            SqlParameter[] sqlParameterArray = new SqlParameter[1]
              {
                new SqlParameter("@CompanyID", SqlDbType.Int)
              };
            sqlParameterArray[0].Value = (object)companyID;
            return this.RunProcedure("sp_Plants_GetPlantsByCompany", 
                (IDataParameter[])sqlParameterArray, "Plants");
        }
    }
}
