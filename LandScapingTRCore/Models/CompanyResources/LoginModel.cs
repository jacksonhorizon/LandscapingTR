namespace LandscapingTR.Core.Models.CompanyResources
{

    public class LoginModel
    {
        public string username { get; set; }
        public string password { get; set; }

        public LoginModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
