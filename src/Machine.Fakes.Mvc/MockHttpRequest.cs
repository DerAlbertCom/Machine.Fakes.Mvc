using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Machine.Fakes
{
    public class MockHttpRequest
    {
        readonly IFakeAccessor fakes;
        readonly HttpRequestBase request;

        public MockHttpRequest(IFakeAccessor fakes)
        {
            this.fakes = fakes;
            request = fakes.An<HttpRequestBase>();
            MockIt();
        }

        public HttpRequestBase Request
        {
            get { return request; }
        }

        void MockIt()
        {
            MockCollections();
            MockBrowser();
            MockPaths();
            request.WhenToldTo(r => r.UserAgent).Return("Mozilla");
        }
        
        void MockPaths()
        {
            request.WhenToldTo(r=>r.ApplicationPath).Return("~/");
            request.WhenToldTo(r=>r.AppRelativeCurrentExecutionFilePath).Return("~/");
            request.WhenToldTo(r=>r.PathInfo).Return("");
        }

        void MockBrowser()
        {
            request.WhenToldTo(r => r.Browser).Return(fakes.An<HttpBrowserCapabilitiesBase>());
        }

        void MockCollections()
        {
            request.WhenToldTo(r => r.Cookies).Return(new HttpCookieCollection());
            request.WhenToldTo(r=>r.QueryString).Return(new NameValueCollection());
            request.WhenToldTo(r=>r.Form).Return(new FormCollection());
            request.WhenToldTo(r=>r.Files).Return(fakes.An<HttpFileCollectionBase>());
        }
    }
}