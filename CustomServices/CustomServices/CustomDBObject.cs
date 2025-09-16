using System;
using System.Data;
using System.Data.SqlClient;

namespace CustomServices
{
    public abstract class CustomDBObject
    {
        protected SqlConnection _connection;
        private string _connectionString;

        public event EventHandler RunCommandError;

        public event EventHandler RunCommandSuccess;

        public CustomDBObject(string connectionString)
        {
            this._connectionString = connectionString;
            this._connection = new SqlConnection(this._connectionString);
        }

        protected string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        private SqlCommand BuildQueryCommand(
          string procedureName,
          IDataParameter[] parameters)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = procedureName;
            sqlCommand.Connection = this._connection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
                sqlCommand.Parameters.Add(parameter);
            return sqlCommand;
        }

        private SqlCommand BuildIntCommand(string procedureName, IDataParameter[] parameters)
        {
            SqlCommand sqlCommand = this.BuildQueryCommand(procedureName, parameters);
            sqlCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, (byte)0, (byte)0, string.Empty, DataRowVersion.Default, (object)null));
            return sqlCommand;
        }

        protected int RunProcedure(
          string procedureName,
          IDataParameter[] parameters,
          out int rowsAffected)
        {
            this._connection.Open();
            SqlCommand sqlCommand = this.BuildIntCommand(procedureName, parameters);
            rowsAffected = sqlCommand.ExecuteNonQuery();
            int num = (int)sqlCommand.Parameters["ReturnValue"].Value;
            this._connection.Close();
            if (num == 0)
            {
                EventArgs e = new EventArgs();
                if (this.RunCommandError != null)
                    this.RunCommandError((object)this, e);
            }
            else
            {
                EventArgs e = new EventArgs();
                if (this.RunCommandSuccess != null)
                    this.RunCommandSuccess((object)this, e);
            }
            return num;
        }

        protected SqlDataReader RunProcedure(
          string procedureName,
          IDataParameter[] parameters)
        {
            this._connection.Open();
            SqlCommand sqlCommand = this.BuildQueryCommand(procedureName, parameters);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        protected DataSet RunProcedure(
          string procedureName,
          IDataParameter[] parameters,
          string tableName)
        {
            DataSet dataSet = new DataSet();
            this._connection.Open();
            new SqlDataAdapter()
            {
                SelectCommand = this.BuildQueryCommand(procedureName, parameters)
            }.Fill(dataSet, tableName);
            this._connection.Close();
            return dataSet;
        }

        protected void RunProcedure(
          string procedureName,
          IDataParameter[] parameters,
          DataSet dataSet,
          string tableName)
        {
            this._connection.Open();
            new SqlDataAdapter()
            {
                SelectCommand = this.BuildIntCommand(procedureName, parameters)
            }.Fill(dataSet, tableName);
            this._connection.Close();
        }
    }
}
