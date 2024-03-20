using JobPortalAPI_1.ViewModel;
using System.CodeDom.Compiler;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace JobPortalAPI_1.Repository
{
    public class Registration:IRegistration
    {
        private readonly IConfiguration _configuration;
        public Registration(IConfiguration configuration)
        {
                _configuration = configuration;
        }

        public async Task<bool> Register(UserRegistrationDetails registrationDetails) 
        {
            string ConnectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection=new SqlConnection())
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
                    var tempPassword= await GenerateSalt(registrationDetails.Email,registrationDetails.PhoneNumber);

                    registrationDetails.Password =await  ToHashSHA1(tempPassword);

                    int result= await command.ExecuteNonQueryAsync();
                    return result > 0;
                
                }
            }
        }
        public async Task<string> ToHashSHA1(string Password) 
        {
            return await Task.Run(() =>
            {
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    byte[] bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(Password));
                    StringBuilder shaBuilder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        shaBuilder.Append(b.ToString("X2"));
                    }
                    return shaBuilder.ToString();
                }
            });
        }
        public async Task<string> GenerateSalt(string email,long phoneNumber)
        {
            string combinedData = email + phoneNumber;
            byte[] salt = new byte[16];
            await Task.Run(() =>
            {
                using (var rng = new RNGCryptoServiceProvider())
                {
                    byte[] combinedBytes = Encoding.UTF8.GetBytes(combinedData);
                    byte[] randomBytes = new byte[16 - combinedBytes.Length];
                    rng.GetBytes(randomBytes);

                    Buffer.BlockCopy(combinedBytes, 0, salt, 0, combinedBytes.Length);
                    Buffer.BlockCopy(randomBytes, 0, salt, combinedBytes.Length, randomBytes.Length);
                }
            });
            
            return Convert.ToBase64String(salt);
        }
    }
}
