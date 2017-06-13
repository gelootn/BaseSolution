using System.Security.Principal;

namespace BaselineSolution.WebApp.Infrastructure.Models.Authentication
{
    public class AnonymousIdentity : IIdentity
    {
        public string Name { get { return "Anonymous"; } }
        public string AuthenticationType { get { return "None"; } }
        public bool IsAuthenticated { get { return false; } }
    }
}