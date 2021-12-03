using Domain.Entities;

namespace Service.Activities
{
    public interface IActivityRepository
    {
        void CreateOrUpdateActivity(Activity activity);
        Activity? GetActivity(string key);
    }
}