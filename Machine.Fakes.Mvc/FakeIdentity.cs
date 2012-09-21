using System;
using System.Security.Principal;

namespace Machine.Fakes
{
    public class FakeIdentity : IIdentity
    {
        readonly string userName;

        public FakeIdentity(string userName)
        {
            this.userName = userName;
        }

        public string Name
        {
            get { return userName; }
        }

        public string AuthenticationType
        {
            get { return GetType().Name; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(userName); }
        }
    }
}