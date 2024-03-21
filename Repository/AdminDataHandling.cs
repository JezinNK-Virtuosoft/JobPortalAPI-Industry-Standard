using JobPortalAPI_1.Model;
using System.Data.SqlClient;

namespace JobPortalAPI_1.Repository
{
    public class AdminDataHandling:IAdminDataHandling
    {
        private readonly IConfiguration _configuration;
        public AdminDataHandling(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        public async Task<IEnumerable<UserDetails>> GetUserDetails()
        {   List<UserDetails> userList=new List<UserDetails>();
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = "SELECT UserID,FirstName,LastName,Email,PhoneNumber,UserLoginID,Status,Remark FROM UserDetails";
                using (SqlCommand command=new SqlCommand(query,connection))
                {
                    using (SqlDataReader dataReader=await command.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                            UserDetails user = new UserDetails()
                            {
                                UserID = Convert.ToInt32(dataReader["UserID"]),
                                FirstName = dataReader["FirstName"].ToString(),
                                LastName = dataReader["LastName"].ToString(),
                                Email = dataReader["Email"].ToString(),
                                PhoneNumber = Convert.ToInt64(dataReader["PhoneNumber"]),
                                UserLoginID = Convert.ToInt32(dataReader["UserLoginID"]),
                                Status = Convert.ToInt32(dataReader["Status"]),
                                Remark = dataReader["Remark"].ToString()
                            };
                            userList.Add(user);
                        }
                    }
                }
            }
            return userList;
        }
    }
}
