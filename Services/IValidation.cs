﻿namespace JobPortalAPI_1.Services
{
    public interface IValidation
    {
        Task<bool> EmailValidator(string Email);
        Task<bool> EmailExists(string Email);
    }
}