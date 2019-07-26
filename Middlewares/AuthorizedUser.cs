using System.Collections.Generic;
using System.Linq;

namespace JwtTokenDemo.Middlewares
{
    public class AuthorizedUser
    {
        public bool Authorized { get; set; }
        public string Username { get; set; }
        public List<PageAction> PageActionsList { get; set; }

        private AuthorizedUser()
        {
            this.Authorized = false;
            this.Username = string.Empty;
            this.PageActionsList = new List<PageAction>();
        }

        public AuthorizedUser(string username, string password)
        {
            if (username == "Admin" && password == "Password")
            {
                AuthorizedUser user = CreateMock();
                this.Authorized = user.Authorized;
                this.Username = user.Username;
                this.PageActionsList = user.PageActionsList;
            }
        }

        public AuthorizedUser(string username)
        {
            AuthorizedUser user = CreateMock();
            this.Authorized = user.Authorized;
            this.Username = user.Username;
            this.PageActionsList = user.PageActionsList;
        }

        public static bool VerifyCredentials(string username, string password)
        {
            //Vediamo se le credenziali fornite sono valide
            AuthorizedUser currUser = new AuthorizedUser(username, password);
            return currUser.Authorized;
        }

        public static bool canExecute(string username, string controller, string action)
        {
            bool ret = false;
            AuthorizedUser curreUser = new AuthorizedUser(username);
            if (curreUser.PageActionsList.Any(p => p.Controller == controller && p.Action == action))
                ret = true;
            return ret;
        }

        private static AuthorizedUser CreateMock()
        {
            return new AuthorizedUser()
            {
                Authorized = true,
                Username = "Admin",
                PageActionsList = new List<PageAction>() { new PageAction() { Controller = "UserData", Action = "Get" } }
            };
        }
    }
}

public class PageAction
{
    public string Controller { get; set; }
    public string Action { get; set; }
}

