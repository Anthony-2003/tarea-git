using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public abstract class Repository

    {
        private readonly string connectionString;

        public Repository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["connRestauranteLaParrilla"].ToString();
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
