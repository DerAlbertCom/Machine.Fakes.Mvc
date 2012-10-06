using System;
using System.Collections.Specialized;
using System.Web;

namespace Machine.Fakes
{
    public class MockHttpResponse
    {
        readonly IFakeAccessor fakes;
        readonly HttpResponseBase response;

        public MockHttpResponse(IFakeAccessor fakes)
        {
            this.fakes = fakes;
            response = fakes.An<HttpResponseBase>();
            MockIt();
        }

        public HttpResponseBase Response
        {
            get { return response; }
        }

        void MockIt()
        {
            var headers = new NameValueCollection();
            response.WhenToldTo(c => c.Headers).Return(headers);

            var cookies = new HttpCookieCollection();
            response.WhenToldTo(c => c.Cookies).Return(cookies);
        }
    }
}