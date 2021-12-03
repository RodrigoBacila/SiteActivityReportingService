using Microsoft.AspNetCore.Mvc;
using Service.Activities;
using Service.Activities.Arguments;
using System.Net;

namespace SiteActivityReportingService.Controllers
{
    /// <summary>
    /// Site Activity Reporting API
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;

        public ActivityController(IActivityService activityService)
        {
            this.activityService = activityService;
        }

        /// <summary>
        /// Gets the aggregate sum of all event durations reported for the activity with the provided key, within the last 12 hours.
        /// </summary>
        /// <param name="key">The activity key</param>
        /// <returns>The sum of the durations of all events for the activity</returns>
        [ProducesResponseType(typeof(GetTotalActivityDurationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{key}/total")]
        public IActionResult GetTotalActivityDuration(string key)
        {
            var activityTotalDuration = activityService.GetTotalActivityDuration(key);

            return activityTotalDuration.IsFailure
                ? NotFound()
                : Ok(new GetTotalActivityDurationResponse() { Value = activityTotalDuration.Result });
        }

        /// <summary>
        /// Registers a new event and its duration for the activity with the provided key.
        /// </summary>
        /// <param name="key">The activity key</param>
        /// <param name="request">The registration request, containing the event duration in seconds.</param>
        /// <returns>Ok.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [HttpPost("{key}")]
        public IActionResult RegisterNewEventForActivity(string key, [FromBody]RegisterNewEventRequest request)
        {
            activityService.RegisterNewEventForActivity(key, request.Value);

            return Ok();
        }
    }
}
