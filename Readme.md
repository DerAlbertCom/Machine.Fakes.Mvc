# Machine.Fakes.MVC #

**Machine.Fakes.MVC** is an extension for [Machine.Fakes](https://github.com/machine/machine.fakes) for easier setup  testing Setup of ASP.MVC 

## Usage ##

Create an WithController<TSubject> Class with the FakeEngine you use.

    public class WithController<TSubject> : WithController<TSubject,MoqFakeEngine> where TSubject : Controller
    {
          
    }


Derive your specs from **WithController< T>** instead of **Subject< T>**.

Use the protected field **HttpContextHelper** to access the faked instances of **HttpContext**, **HttpRequest** etc. Most is mocked, you can change the behavior with *WhenToldTo()*.

For easier use the things like Cookies, Session Values, Items etc. are *premocked* with the right collections. Simply add or remove values.

If you want *only* need a mocked HttpContext etc. then you can use the **BehaviorHttpContext**

    contextBehavior = With<BehaviorHttpContext>();

and now you can use the contextBehavior to access the faked instances.

## Status of the Project ##
This is a fresh project, feel free to use it. Please contribute so that it can be shortly a valuable addition to the *Machine.Fakes* environment.



