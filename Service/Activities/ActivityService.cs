using Crosscutting.Shared;
using Domain.Entities;

namespace Service.Activities
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository repository;

        public ActivityService(IActivityRepository repository)
        {
            this.repository = repository;
        }

        public OperationResult<int> GetTotalActivityDuration(string key)
        {
            var queryResult = repository.GetActivity(key);

            if (queryResult == null)
                return OperationResult<int>.Fail(key, $"No activities were found for the given key [{key}] within the last 12 hours.");

            return queryResult
                .Events?
                .Sum(ev => ev.Duration.TotalSeconds) ?? 0;
        }

        public void RegisterNewEventForActivity(string key, int eventDurationInSeconds)
        {
            var activity = repository.GetActivity(key) ?? new Activity(key, new List<Event>());

            activity.Events.Add(new Event(new Duration(eventDurationInSeconds), DateTime.Now));
            activity.PruneOldEvents();

            repository.CreateOrUpdateActivity(activity);
        }
    }
}