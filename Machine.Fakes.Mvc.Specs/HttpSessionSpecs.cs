using System.Web.SessionState;
using Machine.Specifications;

namespace Machine.Fakes.Mvc.Specs
{
    [Subject(typeof(HttpSessionState))]
    public class When_adding_a_session_value : WithController<TestController>
    {
        Because of = () => Subject.Session.Add("wert1", "value1");

        It should_has_the_right_value = () => Subject.Session["wert1"].ShouldEqual("value1");
    }

    [Subject(typeof(HttpSessionState))]
    public class When_settiong_a_session_value : WithController<TestController>
    {
        Because of = () => Subject.Session["wert1"]= "value1";


        It should_has_the_right_value = () => Subject.Session["wert1"].ShouldEqual("value1");
    }

    [Subject(typeof(HttpSessionState))]
    public class When_adding_two_session_values : WithController<TestController>
    {
        Because of = () =>
            {
                Subject.Session.Add("wert1", "value1");
                Subject.Session.Add("wert2", "value2");
            };

        It should_the_first_has_the_right_value = () => Subject.Session["wert1"].ShouldEqual("value1");

        It should_the_second_has_the_right_value = () => Subject.Session["wert2"].ShouldEqual("value2");
    }

    [Subject(typeof(HttpSessionState))]
    public class When_removing_a_session_value : WithController<TestController>
    {
        Establish context = () =>
            {
                Subject.Session.Add("wert1", "value1");
                Subject.Session.Add("wert2", "value2");
            };

        Because of = () => Subject.Session.Remove("wert1");


        It should_the_second_has_the_right_value = () => Subject.Session["wert2"].ShouldEqual("value2");
    }

    [Subject(typeof(HttpSessionState))]
    public class When_removing_a_the_second_session_value : WithController<TestController>
    {
        Establish context = () =>
        {
            Subject.Session.Add("wert1", "value1");
            Subject.Session.Add("wert2", "value2");
        };

        Because of = () => Subject.Session.RemoveAt(1);


        It should_the_first_has_the_right_value = () => Subject.Session["wert1"].ShouldEqual("value1");
    }

    [Subject(typeof(HttpSessionState))]
    public class When_clearing_all_session_values : WithController<TestController>
    {
        Establish context = () =>
        {
            Subject.Session.Add("wert1", "value1");
            Subject.Session.Add("wert2", "value2");
        };

        Because of = () => Subject.Session.Clear();

        It should_the_first_not_present_in_the_faked_session = () => Subject.Session.Count.ShouldEqual(0);
    }
}