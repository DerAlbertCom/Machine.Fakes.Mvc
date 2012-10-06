using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Web.Caching;
using System.Web.Hosting;

namespace Machine.Fakes
{
    public class FakeVirtualPathProvider : VirtualPathProvider
    {
        public override CacheDependency GetCacheDependency(string virtualPath,
                                                           IEnumerable virtualPathDependencies,
                                                           DateTime utcStart)
        {
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override ObjRef CreateObjRef(Type requestedType)
        {
            return base.CreateObjRef(requestedType);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            return base.GetDirectory(virtualDir);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return base.GetFile(virtualPath);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return base.DirectoryExists(virtualDir);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        public override object InitializeLifetimeService()
        {
            return base.InitializeLifetimeService();
        }

        public override string CombineVirtualPaths(string basePath, string relativePath)
        {
            return base.CombineVirtualPaths(basePath, relativePath);
        }

        public override bool FileExists(string virtualPath)
        {
            return base.FileExists(virtualPath);
        }

        public override string GetCacheKey(string virtualPath)
        {
            return base.GetCacheKey(virtualPath);
        }
    }
}