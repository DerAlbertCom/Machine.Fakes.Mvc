using System;
using System.Web;

namespace Machine.Fakes
{
    public class MockHttpRequest
    {
        private readonly IFakeAccessor _accessor;
        private readonly HttpRequestBase _request;

        public MockHttpRequest(IFakeAccessor accessor)
        {
            _accessor = accessor;
            _request = accessor.An<HttpRequestBase>();
            MockIt();
        }

        void MockIt()
        {
            MockCookies();
            MockBrowser();

            _request.WhenToldTo(r=>r.UserAgent).Return("Mozilla");
        }

        void MockBrowser()
        {
            var browserCaps = _accessor.An<HttpBrowserCapabilitiesBase>();
            _request.WhenToldTo(r=>r.Browser).Return(browserCaps);
        }

        void MockCookies()
        {
            var cookies = new HttpCookieCollection();
            _request.WhenToldTo(r=>r.Cookies).Return(cookies);
        }

        public HttpRequestBase Request
        {
            get { return _request; }
        }
    }
}