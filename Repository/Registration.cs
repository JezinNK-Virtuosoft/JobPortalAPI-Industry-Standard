﻿using JobPortalAPI_1.ViewModel;
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
                    
                    MailCredintials.Add("Email", registrationDetails.Email);
                    MailCredintials.Add(registrationDetails.Email, tempPassword);

                    registrationDetails.Password =await  ToHashSHA1(tempPassword);

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
        //Converting to SHA1Hash
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
                        shaBuilder.Append(b.ToString("x2"));
                    }
                    return shaBuilder.ToString();
                }
            });
        }

        

        
        // Random Password Generator using Salt
        public async Task<string> GenerateSalt(string email,long phoneNumber)
        {   string regex=@"[^\w\d]";
            string combinedData = email + phoneNumber;
            combinedData=Regex.Replace(combinedData,regex,string.Empty);
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