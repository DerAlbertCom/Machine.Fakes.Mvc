﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;

namespace Machine.Fakes
{
    public class WithController<TController, TFakeEngine> : WithSubject<TController, TFakeEngine>
        where TFakeEngine : IFakeEngine, new() where TController : Controller
    {
        protected static BehaviorHttpContext HttpContextHelper;

        Establish context = () =>
        {
            HttpContextHelper = With<BehaviorHttpContext>();
            _specificationController.EnsureSubjectCreated();
            var routeData = new RouteData();
            routeData.Values.Add("controller",
                                 typeof (TController).Name.Substring(0,
                                                                     typeof (TController).
                                                                         Name.Length -
                                                                     "Controller".Length));
            var controllerContext = new ControllerContext(HttpContextHelper.HttpContext, routeData,
                                                          Subject);
            Subject.ControllerContext = controllerContext;
        };

        protected WithController()
        {
        }
    }
}