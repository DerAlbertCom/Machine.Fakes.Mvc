using System.Web;
using Machine.Specifications;

namespace Machine.Fakes.Mvc.Specs
{
    [Subject(typeof(HttpContextBase))]
    public class When_adding_an_entry_to_items : WithController<TestController>
    {
        Establish context = () =>
                            {
                                Subject.HttpContext.Items.Add("key", "value");
                            };

        It should_has_the_item_key_present = () => Subject.HttpContext.Items.Contains("key").ShouldBeTrue();
        It should_has_the_item_key_has_the_value_value = () => Subject.HttpContext.Items["key"].ShouldEqual("value");
    }
}