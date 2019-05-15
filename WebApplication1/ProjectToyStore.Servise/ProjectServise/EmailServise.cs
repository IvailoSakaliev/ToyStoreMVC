using ProjectToyStore.Data.Models;
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
            _admin = GetDecriptedInformationForAdmin();
            _encript = new EncriptServises();
            _userEmail = _encript.DencryptData(user.Email);
            _userID = user.ID;
            _smtpClient = new SmtpClient();
            _basicCredential =
                new NetworkCredential(_admin.Email, _admin.Password);
            _message = new MailMessage();
            _fromAddress = new MailAddress(_userEmail);
        }

        public void SendEmail(int mode)
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
                default:
                    break;
            }

        }

        private void SendRestorPasswordEmail()
        {
            _message.Subject = "Restor Password";
            _message.Body = "Please enter link to restor your password in StudentSystem http://studentsystem.azurewebsites.net/SingIN/ChangePassword/" + _userID;
            _message.To.Add(_userEmail);
            _smtpClient.Send(_message);
        }

        private void SendConfirmEmail()
        {
            _message.Subject = "Confirm registration";
            _message.Body = "Please to confirm your registration in StudentSystem http://studentsystem.azurewebsites.net/SingIN/Confirm/" + _userID;
            _message.To.Add(_userEmail);
            _smtpClient.Send(_message);
        }

        private Login GetDecriptedInformationForAdmin()
        {
            var admin = GetByID(1);
            Login result = new Login();
            result.Email = _encript.DencryptData(admin.Email);
            result.Password = _encript.DencryptData(admin.Password);
            return result;
        }
    }
}
