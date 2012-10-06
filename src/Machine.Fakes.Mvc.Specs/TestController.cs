using System;
using System.Web.Mvc;

namespace Machine.Fakes.Mvc.Specs
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
// ReSharper disable Mvc.ViewNotResolved
            return View();
// ReSharper restore Mvc.ViewNotResolved
        }
    }
}