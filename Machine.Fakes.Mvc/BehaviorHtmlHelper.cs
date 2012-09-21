using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Policy;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Machine.Fakes
{
    public class FakeVirtualPathProvider : VirtualPathProvider
    {
        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return base.CombineVirtualPaths(basePath, relativePath);
        }

        public override bool FileExists(string virtualPath)
        {
            return true;
            return base.FileExists(virtualPath);
        }

        public override string GetCacheKey(string virtualPath)
        {
            return base.GetCacheKey(virtualPath);
        }
    }

    public class BehaviorHtmlHelper<TModel>
    {
        public BehaviorHtmlHelper(IFakeAccessor accessor, Controller controller, IView view)
        {
            context(accessor);
            Thread.GetDomain().SetData(".appDomain", ".appDomain");
            Thread.GetDomain().SetData(".appId", "FooBar");
            Thread.GetDomain().SetData(".appPath", @"C:\Code\Machine.Fakes.Mvc\Machine.Fakes.Mvc.Specs");
            Thread.GetDomain().SetData(".appVPath", @"/");
            Thread.GetDomain().SetData(".domainId", @"FooBarDomainId");

            var policyLevel = PolicyLevel.CreateAppDomainLevel();
            var _privateInitializeHostingFeatures = typeof(HttpRuntime).GetMethod("InitializeHostingFeatures",
                                                                    BindingFlags.Static | BindingFlags.NonPublic);
            _privateInitializeHostingFeatures.Invoke(null, new object[] { 0, policyLevel,null });


            var _privateInitalize = typeof(BuildManager).GetMethod("InitializeBuildManager",
                                                        BindingFlags.Static | BindingFlags.NonPublic);
            _privateInitalize.Invoke(null, null);

            if (!HostingEnvironment.IsHosted)
            {
//                var am = ApplicationManager.GetApplicationManager();
   //             am.CreateObject("FooBar", typeof(DummyRegistryObject), "/", @"C:\Code\Machine.Fakes.Mvc\Machine.Fakes.Mvc.Specs", false);
                new HostingEnvironment();
                CallContext.SetData("__TemporaryVirtualPathProvider__", new FakeVirtualPathProvider());
            }
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FakeRazorViewEngine());
            HtmlHelper = FakeHtmlHelper<TModel>.Create(accessor, controller.ControllerContext);
        }

        public BehaviorHtmlHelper(IFakeAccessor accessor, Controller controller)
            : this(accessor, controller, accessor.The<IView>())
        {
        }

        public HtmlHelper<TModel> HtmlHelper { get; private set; }

        OnEstablish context = accessor => { };
    }

    [Serializable]
    public class DummyRegistryObject : IRegisteredObject
    {
        public DummyRegistryObject()
        {
            var a = 1;
        }
        public void Stop(bool immediate)
        {
        }
    }
}