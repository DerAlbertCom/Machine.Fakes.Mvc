using System.Web.Mvc;
using System.Web.Mvc.Html;
using Machine.Specifications;

namespace Machine.Fakes.Mvc.Specs
{
    // ReSharper disable Mvc.PartialViewNotResolved
    [Subject(typeof(HtmlHelper))]
    public class When_Context : WithController<TestController>
    {
        Establish context = () =>
        {
            With<BehaviorHttpContext>();
            behaviorHtmlHelper = new BehaviorHtmlHelper<object>(_specificationController, Subject);
            With(behaviorHtmlHelper);
            
        };

        Because of = () =>
                     {
                         behaviorHtmlHelper.HtmlHelper.Partial("_Foobar");
                     };
        
        It should_start_with_less_then_characater = () => markup.ShouldStartWith("<");

        static BehaviorHtmlHelper<object> behaviorHtmlHelper;
        static string markup;
    }
    // ReSharper restore Mvc.PartialViewNotResolved
}