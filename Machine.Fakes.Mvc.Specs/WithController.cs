using System.Web.Mvc;
using Machine.Fakes.Adapters.Moq;

namespace Machine.Fakes.Mvc.Specs
{
    public class WithController<TSubject> : WithController<TSubject,MoqFakeEngine> where TSubject : Controller
    {
          
    }
}