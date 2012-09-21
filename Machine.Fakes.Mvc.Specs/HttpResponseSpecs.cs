using System.Web;
using Machine.Specifications;

namespace Machine.Fakes.Mvc.Specs
{
    [Subject(typeof(HttpResponseBase))]
    public class When_setting_the_status_code_with_200 : WithController<TestController>
    {
        Because of = () => Subject.Response.StatusCode = 200;

       It should_has_the_status_code_200 = () => Subject.Response.StatusCode.ShouldEqual(200);
        
    }

    [Subject(typeof(HttpResponseBase))]
    public class When_setting_the_status_code_with_304 : WithController<TestController>
    {
        Because of = () => Subject.Response.StatusCode = 304;

        It should_has_the_status_code_304 = () => Subject.Response.StatusCode.ShouldEqual(304);
    }

}