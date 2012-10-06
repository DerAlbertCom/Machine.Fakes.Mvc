using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;

namespace Machine.Fakes
{
    public class MockHttpContext
    {
        readonly IFakeAccessor fakes;
        readonly HttpContextBase httpContext;
        readonly Lazy<HttpRequestBase> httpRequest;
        readonly Lazy<HttpResponseBase> httpResponse;
        readonly Lazy<HttpSessionStateBase> httpSessionState;
        readonly IDictionary items = new HybridDictionary();

        public MockHttpContext(IFakeAccessor fakes)
        {
            this.fakes = fakes;
            httpContext = this.fakes.An<HttpContextBase>();
            httpRequest = new Lazy<HttpRequestBase>(() => new MockHttpRequest(fakes).Request);
            httpResponse = new Lazy<HttpResponseBase>(() => new MockHttpResponse(fakes).Response);
            httpSessionState = new Lazy<HttpSessionStateBase>(() => new MockHttpSessionState(fakes).Session);
            MockIt();
        }

        public HttpContextBase HttpContext
        {
            get { return httpContext; }
        }

        void MockIt()
        {
            httpContext.WhenToldTo(c => c.Session).Return(httpSessionState.Value);
            httpContext.WhenToldTo(c => c.Request).Return(httpRequest.Value);
            httpContext.WhenToldTo(c => c.Response).Return(httpResponse.Value);

            httpContext.WhenToldTo(c => c.Items).Return(new Dictionary<object, object>());
            MockPrincipal();
        }

        void MockPrincipal()
        {
            var principal = fakes.An<IPrincipal>();
            principal.WhenToldTo(p=>p.Identity).Return(fakes.An<IIdentity>());
        }
    }
}