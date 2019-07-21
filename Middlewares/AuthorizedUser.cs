using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                this.Authorized = true;
                this.Username = username;
                this.PageActionsList = new List<PageAction>();
                this.PageActionsList.Add(new PageAction() { Route = "UserDataController.Get" });
            }
        }

        public AuthorizedUser(string username)
        {
            this.Authorized = true;
            this.Username = username;
            this.PageActionsList = new List<PageAction>();
            this.PageActionsList.Add(new PageAction() { Route = "UserDataController.Get" });
        }
    }

    public class PageAction
    {
        public string Route { get; set; }
    }
}
