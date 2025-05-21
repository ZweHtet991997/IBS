using Microsoft.Data.SqlClient;
using System.Data;

namespace IBS_BackendApi.Services.Common
{
    public class GetConnectionService
    {
        private SqlConnection connection;
        private IConfiguration configuration;

        public GetConnectionService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SqlConnection OpenConnection()
        {
            string conStr = configuration.GetConnectionString("IBSBackendDBConnection");
            this.connection = new SqlConnection(conStr);
            this.connection.Open();
            return this.connection;
        }

        public void CloseConnection()
        {
            if (this.connection.State == ConnectionState.Open)
            {
                this.connection.Close();
            }
        }
    }
}
