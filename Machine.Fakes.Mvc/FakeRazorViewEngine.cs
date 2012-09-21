using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Machine.Fakes
{
    public class FakeRazorViewEngine : RazorViewEngine
    {
        private static readonly Lazy<string> _basePath = new Lazy<string>(BasePathFactory);

        private static string BasePathFactory()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;

            while (Directory.Exists(path))
            {
                if (Directory.Exists(Path.Combine(path, "Views")))
                {
                    return path;
                }
                path = Path.GetDirectoryName(path);
            }
            throw new InvalidOperationException();
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext,
                                                         string partialViewName,
                                                         bool useCache)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");
            string requiredString = controllerContext.RouteData.GetRequiredString("controller");
            string[] searchedLocations;
            string path = GetPath(controllerContext, this.PartialViewLocationFormats,
                                  this.AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName,
                                  requiredString, "Partial", useCache, out searchedLocations);
            if (string.IsNullOrEmpty(path))
                return new ViewEngineResult((IEnumerable<string>) searchedLocations);
            else
                return new ViewEngineResult(this.CreatePartialView(controllerContext, path), (IViewEngine) this);
        }


        private string GetPath(ControllerContext controllerContext,
                               string[] locations,
                               string[] areaLocations,
                               string locationsPropertyName,
                               string name,
                               string controllerName,
                               string cacheKeyPrefix,
                               bool useCache,
                               out string[] searchedLocations)
        {
            searchedLocations = new string[0];
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            string areaName = (string) controllerContext.RouteData.Values["area"];
            bool isArea = !string.IsNullOrEmpty(areaName);
            var viewsLocation = isArea ? areaLocations : locations;
            string action = name;
            var a = AppDomain.CurrentDomain;
            foreach (var locationFormat in viewsLocation)
            {
                string path = string.Format(locationFormat, action, controllerName, areaName);
                if (File.Exists(path.Replace("~",  _basePath.Value)))
                {
                    return path;
                }
            }
            return null;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = (string)null;
            bool runViewStartPages = false;
            IEnumerable<string> viewStartFileExtensions = (IEnumerable<string>)this.FileExtensions;
            IViewPageActivator viewPageActivator = this.ViewPageActivator;
            return (IView)new FakeRazorView(controllerContext, partialPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
            {
                DisplayModeProvider = this.DisplayModeProvider
            };
        }
    }
}