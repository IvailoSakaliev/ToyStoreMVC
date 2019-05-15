using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using System.Collections.Generic;

namespace ProjectToyStore.Servise.ProjectServise
{
    public class AuthenticationServises
        : GenericServise<Login>
    {
        public Login LoggedUser { get; set; }
        public int Login_id { get; set; }
        private List<Login> list { get; set; }
        public LoginServise singIn { get; set; }
        private IEncriptServises _encript { get; set; }

        public AuthenticationServises()
        {
            this.singIn = new LoginServise();
        }
        public bool AuthenticateUser(string email, string password, int state)
        {
            string decriptedEmail = "";
            string decriptedPassword = "";
            this.list = GetAll();
            foreach (var item in list)
            {
                decriptedEmail = _encript.DencryptData(item.Email);
                decriptedPassword = _encript.DencryptData(item.Password);
                if (decriptedEmail == email && decriptedPassword == password)
                {
                    this.LoggedUser = item;
                    
                    return AddSession(state); ;
                }
                else
                {
                    this.LoggedUser = null;
                   
                }
            }

            return false;

        }

        private bool AddSession(int state)
        {
            if (this.LoggedUser != null)
            {
                if (state == 1)
                {
                    if (singIn.IsConfirmRegistartion(this.LoggedUser) == true)
                    {
                        return true;
                    }
                }
                else if (state == 2)
                {
                    ReturnIdFromUser();
                }
            }
            return false;
        }
        private void ReturnIdFromUser()
        {
            this.Login_id = LoggedUser.ID;
        }

        private void GoToSession()
        {

            //HttpContext.Current.Session["LoggedUser"] = LoggedUser.Role;
            //HttpContext.Current.Session["UserFirstName"] = this.singIn.EncriptServise.DencryptData(LoggedUser.Name);
            //HttpContext.Current.Session["User_ID"] = LoggedUser.ID;
        }

        public void LoggOut()
        {

            this.LoggedUser = null;
            //HttpContext.Current.Session["LoggedUser"] = null;
            //HttpContext.Current.Session["UserFirstName"] = "";
            //HttpContext.Current.Session["User_ID"] = null;
            //CoockieServises cookie = new CoockieServises();
            //cookie.DeleteCoockie("UserInformation");

        }
    }
}
