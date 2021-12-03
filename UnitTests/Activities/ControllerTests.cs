using Castle.Windsor;
using Crosscutting.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Activities;
using Service.Activities.Arguments;
using SiteActivityReportingService.Controllers;
using System.Net;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Activities
{
    public class ControllerTests
    {
        private readonly IWindsorContainer windsorContainer;
        private readonly ActivityController activityController;

        public ControllerTests()
        {
            windsorContainer = new WindsorContainer();
            windsorContainer.Install(new BaseInstaller<ActivityController>());
            activityController = windsorContainer.Resolve<ActivityController>();
        }

        [Fact]
        public void GetTotalActivityDuration_Should_Return_Success_When_Applicable()
        {
            windsorContainer
                .Resolve<Mock<IActivityService>>()
                .Setup(service => service.GetTotalActivityDuration(It.IsAny<string>()))
                .Returns(new OperationResult<int>(MockData.GetTotalActivityDurationResponse().Value).Result);

            var actionResult = activityController.GetTotalActivityDuration(It.IsAny<string>());

            var requestResult = actionResult
                .Should()
                .BeOfType<OkObjectResult>().Subject;

            requestResult
                .Should()
                .NotBeNull();

            requestResult
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);

            requestResult
                .Value
                .Should()
                .NotBeNull();
        }

        [Fact]
        public void GetTotalActivityDuration_Should_Return_Failure_When_Applicable()
        {
            windsorContainer
                .Resolve<Mock<IActivityService>>()
                .Setup(service => service.GetTotalActivityDuration(It.IsAny<string>()))
                .Returns(OperationResult<int>.Fail(It.IsAny<string>(), It.IsAny<string>()));

            var actionResult = activityController.GetTotalActivityDuration(It.IsAny<string>());

            var requestResult = actionResult
                .Should()
                .BeOfType<NotFoundResult>().Subject;

            requestResult
                .Should()
                .NotBeNull();

            requestResult
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public void RegisterNewEventForActivity_Should_Return_Success_When_Applicable()
        {
            windsorContainer
                .Resolve<Mock<IActivityService>>()
                .Setup(service => service.RegisterNewEventForActivity(It.IsAny<string>(), It.IsAny<int>()));

            var actionResult = activityController.RegisterNewEventForActivity(It.IsAny<string>(), new RegisterNewEventRequest()
            {
                Value = MockData.AnyPositiveValue()
            });

            var requestResult = actionResult
                .Should()
                .BeOfType<OkResult>().Subject;

            requestResult
                .Should()
                .NotBeNull();

            requestResult
                .StatusCode
                .Should()
                .Be((int)HttpStatusCode.OK);
        }
    }
}