﻿using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using System.Net;
using System.Net.Mail;

namespace ProjectToyStore.Servise.ProjectServise
{
    public class EmailServises : GenericServise<Login>
    {
        public Login _admin;
        private LoginServise singIn = new LoginServise();
        private IEncriptServises _encript;
        private string _userEmail;
        private int _userID;
        private SmtpClient _smtpClient;
        private NetworkCredential _basicCredential;
        private MailMessage _message;
        private MailAddress _fromAddress;

        public EmailServises()
            : base()
        {

        }

        public EmailServises(Login user)
        {
            _encript = new EncriptServises();
            _admin = GetDecriptedInformationForAdmin();
            _userEmail = _encript.DencryptData(user.Email);
            _userID = user.ID;
            _smtpClient = new SmtpClient();
            _basicCredential =
                new NetworkCredential(_admin.Email, _admin.Password);
            _message = new MailMessage();
            _fromAddress = new MailAddress(_admin.Email);
        }

        public void SendEmail(int mode)
        {
            EmailINfo(mode, null);

        }
        public void SendEmailFromAdmin(int mode, Contact model)
        {
            EmailINfo(mode, model);
        }
        private void EmailINfo(int mode, Contact model)
        {
            _smtpClient.Host = "smtp.gmail.com";
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = _basicCredential;
            _smtpClient.EnableSsl = true;
            _smtpClient.Port = 587;

            _message.From = _fromAddress;
            _message.IsBodyHtml = true;

            switch (mode)
            {
                case 1:
                    SendConfirmEmail();
                    break;
                case 2:
                    SendRestorPasswordEmail();
                    break;
                case 3:
                    SendEmailToUser(model);
                    break;
                default:
                    break;
            }
        }

        private void SendRestorPasswordEmail()
        {
            _message.Subject = "Restor Password";
            _message.Body = "Please enter link to restor your password in StudentSystem http://wwww.vavilonci.com/Login/RestorePassword?id=" + _encript.EncryptData(_userID.ToString());
            _message.To.Add(_userEmail);
            _smtpClient.Send(_message);
        }

        private void SendConfirmEmail()
        {
            _message.Subject = "Confirm registration";
            _message.Body = "Please to confirm your registration in Vavilonci http://wwww.vavilonci.com/Login/EnableAccount?id=" + _userID;
            _message.To.Add(_userEmail);
            _smtpClient.Send(_message);
        }
        private void SendEmailToUser(Contact model)
        {
            _message.Subject = model.Name;
            _message.Body = model.Message;
            _message.To.Add(_userEmail);
            _smtpClient.Send(_message);
        }

        private Login GetDecriptedInformationForAdmin()
        {
            var admin = GetByID(1);
            Login result = new Login();
            result.Email = _encript.DencryptData(admin.Email.ToString());
            result.Password = _encript.DencryptData(admin.Password.ToString());
            return result;
        }
    }
}
