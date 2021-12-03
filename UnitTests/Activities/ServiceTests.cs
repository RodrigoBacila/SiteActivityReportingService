using Castle.Windsor;
using Crosscutting.Shared;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Service.Activities;
using System.Collections.Generic;
using UnitTests.Common;
using Xunit;

namespace UnitTests.Activities
{
    public class ServiceTests
    {
        private readonly IWindsorContainer windsorContainer;
        private readonly ActivityService activityService;

        public ServiceTests()
        {
            windsorContainer = new WindsorContainer();
            windsorContainer.Install(new BaseInstaller<ActivityService>());
            activityService = windsorContainer.Resolve<ActivityService>();
        }

        [Fact]
        public void GetTotalActivityDuration_Should_Return_Success_When_Applicable()
        {
            var key = MockData.RandomKey();
            var events = new List<Event>() 
            {
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
            };

            windsorContainer
                .Resolve<Mock<IActivityRepository>>()
                .Setup(service => service.GetActivity(key))
                .Returns(new Activity(key, events));

            var serviceResult = activityService.GetTotalActivityDuration(key);

            var result = serviceResult
                .Should()
                .BeOfType<OperationResult<int>>().Subject;

            result
                .IsFailure
                .Should()
                .BeFalse();

            result
                .Result
                .Should()
                .BePositive();
        }

        [Fact]
        public void GetTotalActivityDuration_Should_Return_Failure_When_Applicable()
        {
            var key = MockData.RandomKey();
            var events = new List<Event>()
            {
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
                MockData.RandomEvent(),
            };

            windsorContainer
                .Resolve<Mock<IActivityRepository>>()
                .Setup(service => service.GetActivity(key))
                .Returns(value: null);

            var serviceResult = activityService.GetTotalActivityDuration(key);

            var result = serviceResult
                .Should()
                .BeOfType<OperationResult<int>>().Subject;

            result
                .IsFailure
                .Should()
                .BeTrue();
        }

        [Fact]
        public void RegisterNewEventForActivity_Should_Return_Success_When_Applicable()
        {
            windsorContainer
                .Resolve<Mock<IActivityService>>()
                .Setup(service => service.RegisterNewEventForActivity(It.IsAny<string>(), It.IsAny<int>()));

            
        }
    }
}