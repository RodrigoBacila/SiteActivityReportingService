using Crosscutting.Shared;

namespace Service.Activities
{
    public interface IActivityService
    {
        void RegisterNewEventForActivity(string key, int eventDurationInSeconds);
        OperationResult<int> GetTotalActivityDuration(string key);
    }
}
