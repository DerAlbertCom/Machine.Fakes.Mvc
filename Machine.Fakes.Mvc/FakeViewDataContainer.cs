using System.Web.Mvc;

namespace Machine.Fakes
{
    public class FakeViewDataContainer : IViewDataContainer
     {
        public FakeViewDataContainer(ViewDataDictionary viewData)
        {
            ViewData = viewData;
        }

        public ViewDataDictionary ViewData { get; set; }
     }
}