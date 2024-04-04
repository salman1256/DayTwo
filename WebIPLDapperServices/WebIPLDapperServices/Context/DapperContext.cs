using Microsoft.Data.SqlClient;
using System.Data;

namespace WebIPLDapperServices.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connnectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connnectionString = _configuration.GetConnectionString("IPLConStr");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connnectionString);
        
    }
}
