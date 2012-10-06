using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Machine.Fakes
{
    public class FakeRazorViewEngine : RazorViewEngine
    {
        static readonly Lazy<string> _basePath = new Lazy<string>(BasePathFactory);

        static string BasePathFactory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

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
            string path = GetPath(controllerContext, PartialViewLocationFormats,
                                  AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName,
                                  requiredString, "Partial", useCache, out searchedLocations);
            if (string.IsNullOrEmpty(path))
                return new ViewEngineResult(searchedLocations);
            else
                return new ViewEngineResult(CreatePartialView(controllerContext, path), this);
        }


        string GetPath(ControllerContext controllerContext,
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
            var areaName = (string) controllerContext.RouteData.Values["area"];
            bool isArea = !string.IsNullOrEmpty(areaName);
            string[] viewsLocation = isArea ? areaLocations : locations;
            string action = name;
            AppDomain a = AppDomain.CurrentDomain;
            foreach (string locationFormat in viewsLocation)
            {
                string path = string.Format(locationFormat, action, controllerName, areaName);
                if (File.Exists(path.Replace("~", _basePath.Value)))
                {
                    return path;
                }
            }
            return null;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string layoutPath = null;
            bool runViewStartPages = false;
            IEnumerable<string> viewStartFileExtensions = FileExtensions;
            IViewPageActivator viewPageActivator = ViewPageActivator;
            return new FakeRazorView(controllerContext, partialPath, layoutPath, runViewStartPages,
                                     viewStartFileExtensions, viewPageActivator)
                       {
                           DisplayModeProvider = DisplayModeProvider
                       };
        }
    }
}