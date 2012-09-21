using System;
using System.Web;

namespace Machine.Fakes
{
    public class BehaviorHttpContext
    {
        static volatile int _useCounter;

        OnEstablish _context = accessor =>
                               {
                                   if (_useCounter == 0)
                                       _mock = new MockHttpContext(accessor);
                                   _useCounter++;
                               };

        OnCleanup _cleanup = subject => _useCounter--;

        static MockHttpContext _mock;

        public HttpContextBase HttpContext
        {
            get { return _mock.HttpContext; }
        }

        public HttpRequestBase HttpRequest
        {
            get { return _mock.HttpContext.Request; }
        }

        public HttpSessionStateBase HttpSessionState
        {
            get { return _mock.HttpContext.Session; }
        }

        public HttpResponseBase HttpResponse
        {
            get { return _mock.HttpContext.Response; }
        }
    }
}