using System;
using System.Collections.Specialized;
using System.Web;

namespace Machine.Fakes
{
    public class MockHttpResponse
    {
        readonly IFakeAccessor _accessor;
        readonly HttpResponseBase _response;

        public MockHttpResponse(IFakeAccessor accessor)
        {
            _accessor = accessor;
            _response = accessor.An<HttpResponseBase>();
            MockIt();
        }

        void MockIt()
        {
            var headers = new NameValueCollection();
            _response.WhenToldTo(c => c.Headers).Return(headers);

            var cookies = new HttpCookieCollection();
            _response.WhenToldTo(c => c.Cookies).Return(cookies);

        }

        public HttpResponseBase Response
        {
            get { return _response; }
        }
    }
}