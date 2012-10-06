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
        readonly RazorView razorView;
        PropertyInfo internalDisplayModeProviderPropertyInfo;
        MethodInfo protectedRenderView;

        public FakeRazorView(ControllerContext controllerContext,
                             string viewPath,
                             string layoutPath,
                             bool runViewStartPages,
                             IEnumerable<string> viewStartFileExtensions)
            : this(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, null)
        {
        }

        public FakeRazorView(ControllerContext controllerContext,
                             string viewPath,
                             string layoutPath,
                             bool runViewStartPages,
                             IEnumerable<string> viewStartFileExtensions,
                             IViewPageActivator viewPageActivator)
        {
            razorView = new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages,
                                       viewStartFileExtensions, viewPageActivator);
            Init();
        }

        internal DisplayModeProvider DisplayModeProvider
        {
            get { return (DisplayModeProvider) internalDisplayModeProviderPropertyInfo.GetValue(razorView, null); }
            set { internalDisplayModeProviderPropertyInfo.SetValue(razorView, value, null); }
        }


        public void Render(ViewContext viewContext, TextWriter writer)
        {
            return;
        }

        void Init()
        {
            internalDisplayModeProviderPropertyInfo = typeof (RazorView).GetProperty("DisplayModeProvider",
                                                                                      BindingFlags.NonPublic |
                                                                                      BindingFlags.Instance);
        }
    }
}