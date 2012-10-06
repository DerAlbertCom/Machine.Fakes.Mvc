using System;
using System.Web;

namespace Machine.Fakes
{
    public class BehaviorHttpContext
    {
        static volatile int _useCounter;

        static MockHttpContext _mock;

        OnCleanup cleanup = subject =>
        {
            _useCounter--;
            if (_useCounter == 0)
                _mock = null;
        };

        OnEstablish context = accessor =>
        {
            if (_mock == null)
                _mock = new MockHttpContext(accessor);
            _useCounter++;
        };

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