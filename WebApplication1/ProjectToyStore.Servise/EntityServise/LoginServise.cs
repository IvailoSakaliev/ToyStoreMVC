using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.ProjectServise;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Servise.EntityServise
{
    public  class LoginServise
        :GenericServise<Login>
    {
        public IEncriptServises _encritp { get; set; }
        public LoginServise()
            :base()
        {

        }
        public void ConfirmedRegistration(int? id)
        {
            Login user = GetByID(id);
            user.isRegisted = true;
            Save(user);
        }
        public bool IsConfirmRegistartion(Login user)
        {
            if (user.isRegisted)
            {
                return user.isRegisted;
            }
            return user.isRegisted;
        }


        public void ChangePassword(int id, string password)
        {
            var user = GetByID(id);
            user.Password = _encritp.EncryptData(password);
            Save(user);
        }

        public Login ResetPassword(List<Login> users, string email)
        {
            string decriptedEmail;
            for (int i = 0; i < users.Count; i++)
            {
                decriptedEmail = _encritp.DencryptData(users[i].Email);
                if (email == decriptedEmail)
                {
                    return users[i];
                }
            }
            return null;
        }

       
    }
}
