using Domain.Entities;
using Service.Activities.Dtos;

namespace Service.Activities.Extensions
{
    public static class EventExtensions
    {
        public static EventDto AsDto(this Event activity)
        {
            return new EventDto()
            {
                Duration = new DurationDto()
                {
                    TotalSeconds = activity.Duration.TotalSeconds
                }
            };
        }
    }
}
