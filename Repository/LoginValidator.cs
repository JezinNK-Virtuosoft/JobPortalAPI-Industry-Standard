﻿using JobPortalAPI_1.Model;
using JobPortalAPI_1.ViewModel;
using System.Data.SqlClient;

namespace JobPortalAPI_1.Repository
{
    public class LoginValidator:ILoginValidator
    {
        private readonly IConfiguration _configuration;
        public LoginValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int ValidateUser(LoginCredentials credintials)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UL.UserLoginID FROM UserLoginDetails UL JOIN UserDetails UD ON UL.UserLoginID=UD.UserLoginID WHERE UD.Email=@Email AND UL.Password=@Password";
                using (SqlCommand command=new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Email", credintials.Email);
                    command.Parameters.AddWithValue("@Password", credintials.Password);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        int result = Convert.ToInt32(dataReader["UserLoginID"]);

                        return result;
                    }
                   
                }
            }
            return -1;
        }
        public TokenUserDetails GetUserDetailsByID(int UserLoginID)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UD.UserID,UD.FirstName,UD.Email,UL.UserTypeID From UserDetails UD JOIN UserLoginDetails UL ON UD.UserLoginID=UL.UserLoginID WHERE UL.UserLoginID=@UserLoginID";
                TokenUserDetails tokUserDetail;
                using (SqlCommand command=new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@UserLoginID", UserLoginID);
                    SqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.Read())
                    {
                        tokUserDetail = new TokenUserDetails()
                        {
                            UserID = Convert.ToInt32(dataReader["UserID"]),
                            FirstName = dataReader["FirstName"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            UserTypeID = Convert.ToInt32(dataReader["UserTypeID"])
                        };
                        return tokUserDetail;
                    }
                     
                    
                }
                return null;
            }
        }

        public int ValidateAdmin(LoginCredentials credintials)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                string Query = "SELECT AL.AdminLoginID FROM AdminLoginDetails AL JOIN Admin A ON AL.AdminLoginID=A.AdminLoginID WHERE A.Email=@Email AND AL.Password=@Password";
                using (SqlCommand command=new SqlCommand(Query,connection))
                {
                    command.Parameters.AddWithValue("@Email", credintials.Email);
                    command.Parameters.AddWithValue("@Password", credintials.Password);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) 
                    {
                        int AdminID = Convert.ToInt32(reader["AdminLoginID"]);
                        return AdminID;
                    }
                }
            }
            return -1;
        }
        
        public TokenAdminDetails GetAdminDetailsByID(int AdminID) 
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection(connectionString))
            {   connection.Open();
                string Query = "SELECT A.AdminID,A.Email,AL.UserTypeID FROM Admin A JOIN AdminLoginDetails AL ON A.AdminLoginID= AL.AdminLoginID WHERE AL.AdminLoginID=@AdminID";
                using (SqlCommand command=new SqlCommand(Query,connection))
                {
                    command.Parameters.AddWithValue("@AdminID", AdminID);
                    SqlDataReader reader= command.ExecuteReader();
                    if (reader.Read()) 
                    {
                        TokenAdminDetails tokenAdminDetails = new TokenAdminDetails()
                        {
                            AdminID = Convert.ToInt32(reader["AdminID"]),
                            Email = reader["Email"].ToString(),
                            UserTypeID = Convert.ToInt32(reader["UserTypeID"])
                        };
                        return tokenAdminDetails;
                    }
                }
                return null;
            }
        }
    }
}
