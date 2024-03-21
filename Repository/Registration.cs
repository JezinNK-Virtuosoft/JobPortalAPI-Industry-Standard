using JobPortalAPI_1.Services;
using JobPortalAPI_1.ViewModel;
using System.CodeDom.Compiler;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace JobPortalAPI_1.Repository
{
    public class Registration:IRegistration
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public Registration(IConfiguration configuration,IEmailSender emailSender)
        {
                _configuration = configuration;
                _emailSender = emailSender;
        }
        // Inserting the UserDetails
        public async Task<bool> Register(UserRegistrationDetails registrationDetails) 
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            Dictionary<string, string> MailCredintials = new Dictionary<string, string>();
            Dictionary<string, string> MailContents;
            using (SqlConnection connection=new SqlConnection(ConnectionString))
            {   
                connection.Open();
                string query = "spAddUser";
                using (SqlCommand command=new SqlCommand(query,connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", registrationDetails.FirstName);
                    command.Parameters.AddWithValue("@LastName",registrationDetails.LastName);
                    command.Parameters.AddWithValue("@Email", registrationDetails.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", registrationDetails.PhoneNumber);
                    command.Parameters.AddWithValue("@UserTypeID", registrationDetails.UserTypeID);
                    var tempPassword= await PasswordHandler.GenerateSalt();
                    MailCredintials.Add("Name", registrationDetails.FirstName);
                    MailCredintials.Add("Email", registrationDetails.Email);
                    MailCredintials.Add(registrationDetails.Email, tempPassword);

                    registrationDetails.Password =await  PasswordHandler.ToHashSHA1(tempPassword);
                    command.Parameters.AddWithValue("@Password", registrationDetails.Password);

                    int result= await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        MailContents = await _emailSender.SendMessage(MailCredintials);
                        if(await _emailSender.SendEmail(MailContents))
                        {
                            MailContents.Clear();
                            MailCredintials.Clear();
                        }
                    }
                    return result > 0;
                
                }
            }
        }

        //Checking the Email Already Exists
        public async Task<bool> IsUserEmailExists(string Email)
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection(ConnectionString))
            {
                string query = "SELECT Email FROM UserDetails WHERE Email=@Email";
                using (SqlCommand command=new SqlCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);

                    SqlDataReader dataReader= await command.ExecuteReaderAsync();

                    return dataReader.HasRows;
                }
            }
        }
        
    }
}
