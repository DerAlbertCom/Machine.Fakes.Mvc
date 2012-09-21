using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Machine.Fakes
{
    public class FakePrincipal : IPrincipal
    {
        readonly IEnumerable<string> roles;

        public FakePrincipal(string userName, IEnumerable<string> roles)
        {
            this.roles = roles;
            Identity = new FakeIdentity(userName);
        }

        public bool IsInRole(string role)
        {
            return roles.Any(s => s == role);
        }

        public IIdentity Identity { get; private set; }
    }
}