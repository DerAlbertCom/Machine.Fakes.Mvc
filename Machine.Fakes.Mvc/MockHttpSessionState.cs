using System;
using System.Web;
using System.Web.SessionState;

namespace Machine.Fakes
{
    public class MockHttpSessionState
    {
        private readonly IFakeAccessor _accessor;
        private readonly HttpSessionStateBase _session;

        public MockHttpSessionState(IFakeAccessor accessor)
        {
            _accessor = accessor;
            _session = new FakeHttpSessionState();
        }

        public HttpSessionStateBase Session
        {
            get { return _session; }
        }
    }
}