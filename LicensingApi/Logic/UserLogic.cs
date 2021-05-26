using System;
using System.Collections.Generic;
using LicensingApi.Models;

namespace LicensingApi.Logic
{
    public class UserLogic
    {
        private string username, application;
        public UserLogic(string username, string application)
        {
            this.username = username;
            this.application = application;
        }
        public User GetUser()
        {
            User user = new User()
            {
                Username = username,
                Licenses = new List<License>()
            };
            user.Licenses.Add(new License()
            {
                Application = application,
                Key = null,
                NumberOfUsers = 0,
                Type = "Business"
            });
            if (!CheckIfUserExist())
            {
                throw new KeyNotFoundException($"Cannot find username : {username} for application : {application}.Please call the create endpoint and then license the user.");
            }
            //TODO: write this function
            return user;
        }

        private bool CheckIfUserExist()
        {
            //TODO: Add logic to this method
            return true;
        }
    }
}