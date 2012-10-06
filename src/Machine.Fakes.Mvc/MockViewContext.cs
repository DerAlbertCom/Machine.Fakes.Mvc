using System;
using System.IO;
using System.Web.Mvc;

namespace Machine.Fakes
{
    internal class MockViewContext
    {
        readonly IFakeAccessor fakes;
        readonly ViewContext viewContext;

        public MockViewContext(IFakeAccessor fakes, ControllerContext controllerContext)
        {
            this.fakes = fakes;
            viewContext = fakes.An<ViewContext>();
            viewContext.WhenToldTo(v => v.HttpContext).Return(controllerContext.HttpContext);
            viewContext.WhenToldTo(c => c.RouteData).Return(controllerContext.RouteData);
            viewContext.WhenToldTo(c => c.View).Return(this.fakes.An<IView>());
            MockIt();
        }

        public ViewContext ViewContext
        {
            get { return viewContext; }
        }

        void MockIt()
        {
            viewContext.HttpContext.Response.WhenToldTo(r => r.Output)
                .Throw(new Exception("Response.Output should never be called."));
            viewContext.WhenToldTo(v => v.ViewData).Return(new ViewDataDictionary());
            viewContext.WhenToldTo(v => v.TempData).Return(new TempDataDictionary());
            viewContext.WhenToldTo(v => v.Writer).Return(new StringWriter());
        }
    }
}