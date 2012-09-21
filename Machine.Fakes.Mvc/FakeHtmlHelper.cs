using System;
using System.IO;
using System.Web.Mvc;

namespace Machine.Fakes
{
    internal class MockViewContext
    {
        readonly IFakeAccessor _accessor;
        readonly ViewContext _viewContext;

        public MockViewContext(IFakeAccessor accessor, ControllerContext controllerContext)
        {
            _accessor = accessor;
            _viewContext = accessor.An<ViewContext>();
            _viewContext.WhenToldTo(v => v.HttpContext).Return(controllerContext.HttpContext);
            _viewContext.WhenToldTo(c => c.RouteData).Return(controllerContext.RouteData);
            _viewContext.WhenToldTo(c => c.View).Return(_accessor.An<IView>());
            MockIt();
        }

        void MockIt()
        {
            _viewContext.HttpContext.Response.WhenToldTo(r => r.Output).Throw(new Exception("Response.Output should never be called."));
            _viewContext.WhenToldTo(v=>v.ViewData).Return(new ViewDataDictionary());
            _viewContext.WhenToldTo(v => v.TempData).Return(new TempDataDictionary());
            _viewContext.WhenToldTo(v => v.Writer).Return(new StringWriter());


        }

        public ViewContext ViewContext
        {
            get { return _viewContext; }
        }
    }


    internal class FakeHtmlHelper<T> : HtmlHelper<T>
    {
        public string RenderPartialInternal_PartialViewName;
        public ViewDataDictionary RenderPartialInternal_ViewData;
        public object RenderPartialInternal_Model;
        public TextWriter RenderPartialInternal_Writer;
        public ViewEngineCollection RenderPartialInternal_ViewEngineCollection;

        FakeHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : base(viewContext, viewDataContainer)
        {
        }

        public static FakeHtmlHelper<T> Create(IFakeAccessor accessor, ControllerContext controllerContext)
        {
            ViewContext viewContext = new MockViewContext(accessor,controllerContext).ViewContext;
            var container = accessor.An<IViewDataContainer>();
            container.WhenToldTo(c=>c.ViewData).Return(viewContext.ViewData);
            return new FakeHtmlHelper<T>(viewContext, container);
        }

    }
}