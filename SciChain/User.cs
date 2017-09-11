
namespace SciChain
{
   public class User
    {
        public string login {get; set;}
        public string password { get; set; }

        public User(string log, string pass)
        {
            login = log;
            pass = password;
        }
    }
}
