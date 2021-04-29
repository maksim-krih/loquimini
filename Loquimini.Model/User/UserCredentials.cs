using Loquimini.Model.User;

namespace Loquimini.Model.User
{
    public class UserCredentials
    {
        public AccessToken AccessToken { get; set; }

        public string RefreshToken { get; set; }
 
        public bool IsMasterApi { get; set; }

        public CredentialsInfo CredentialsInfo { get; set; }
    }
}
