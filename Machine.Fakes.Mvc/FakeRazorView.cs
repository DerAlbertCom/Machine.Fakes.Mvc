using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Machine.Fakes
{
    public class FakeRazorView : IView
    {
        PropertyInfo _internalDisplayModeProviderPropertyInfo;
        MethodInfo _protectedRenderView;
        readonly RazorView _razorView;

        public FakeRazorView(ControllerContext controllerContext,
                             string viewPath,
                             string layoutPath,
                             bool runViewStartPages,
                             IEnumerable<string> viewStartFileExtensions)
            : this(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, null)
        {
        }

        void Init()
        {
            _internalDisplayModeProviderPropertyInfo = typeof (RazorView).GetProperty("DisplayModeProvider",
                                                                                      BindingFlags.NonPublic |
                                                                                      BindingFlags.Instance);
            _protectedRenderView = typeof (RazorView).GetMethod("RenderView",
                                                                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public FakeRazorView(ControllerContext controllerContext,
                             string viewPath,
                             string layoutPath,
                             bool runViewStartPages,
                             IEnumerable<string> viewStartFileExtensions,
                             IViewPageActivator viewPageActivator)
        {
            _razorView = new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages,
                                       viewStartFileExtensions, viewPageActivator);
            Init();
        }

        internal DisplayModeProvider DisplayModeProvider
        {
            get { return (DisplayModeProvider) _internalDisplayModeProviderPropertyInfo.GetValue(_razorView, null); }
            set { _internalDisplayModeProviderPropertyInfo.SetValue(_razorView, value, null); }
        }


        public void Render(ViewContext viewContext, TextWriter writer)
        {
            System.Web.Compilation.BuildManager.GetCompiledType(_razorView.ViewPath);
            _razorView.Render(viewContext,writer);
//            _protectedRenderView.Invoke(_razorView, new object[] {viewContext, writer});
        }
    }
}