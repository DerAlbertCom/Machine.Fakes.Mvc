using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Machine;
using System.Web.SessionState;

namespace Machine.Fakes
{
    public class MockHttpContext
    {
        private readonly HttpContextBase _httpContext;
        private readonly IFakeAccessor _accessor;
        private readonly IDictionary _items = new HybridDictionary();
        private readonly Lazy<HttpRequestBase> _httpRequest;
        private readonly Lazy<HttpSessionStateBase> _httpSessionState;
        private readonly Lazy<HttpResponseBase> _httpResponse;

        public MockHttpContext(IFakeAccessor accessor)
        {
            _accessor = accessor;
            _httpContext = _accessor.An<HttpContextBase>();
            _httpRequest = new Lazy<HttpRequestBase>(() => new MockHttpRequest(accessor).Request);
            _httpResponse = new Lazy<HttpResponseBase>(()=> new MockHttpResponse(accessor).Response);
            _httpSessionState=new Lazy<HttpSessionStateBase>(()=>new MockHttpSessionState(accessor).Session);
            MockIt();
        }

        private void MockIt()
        {
            _httpContext.WhenToldTo(c=>c.Session).Return(_httpSessionState.Value);
            _httpContext.WhenToldTo(c=>c.Request).Return(_httpRequest.Value);
            _httpContext.WhenToldTo(c=>c.Response).Return(_httpResponse.Value);

            MockItems();
        }

        void MockItems()
        {
            IDictionary items = new Dictionary<object, object>();
            _httpContext.WhenToldTo(c=>c.Items).Return(items);
        }

        public HttpContextBase HttpContext
        {
            get { return _httpContext; }
        }

    }
}