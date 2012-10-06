using System;
using System.Web.Mvc;

namespace Machine.Fakes
{
    internal class FakeHtmlHelper<T> : HtmlHelper<T>
    {
        FakeHtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer)
            : base(viewContext, viewDataContainer)
        {
        }

        public static FakeHtmlHelper<T> Create(IFakeAccessor accessor, ControllerContext controllerContext)
        {
            ViewContext viewContext = new MockViewContext(accessor, controllerContext).ViewContext;
            var container = accessor.An<IViewDataContainer>();
            container.WhenToldTo(c => c.ViewData).Return(viewContext.ViewData);
            return new FakeHtmlHelper<T>(viewContext, container);
        }
    }
}