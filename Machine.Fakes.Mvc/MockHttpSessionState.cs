using System;
using System.Collections.Generic;
using System.Web;

namespace Machine.Fakes
{
    public class MockHttpSessionState
    {
        readonly IFakeAccessor fakes;
        readonly FakeHttpSessionStateBase session;

        public MockHttpSessionState(IFakeAccessor fakes)
        {
            this.fakes = fakes;
            session = fakes.An<FakeHttpSessionStateBase>();
            session.WhenToldTo(s=>s.SyncRoot).Return(new object());
        }

        public HttpSessionStateBase Session
        {
            get { return session; }
        }
    }
}