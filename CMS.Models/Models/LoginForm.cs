namespace CMS.Models
{
    public class LoginForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string ChangePasswordKey { get; set; }
        public string OldPassword { get; set; }
    }
}
