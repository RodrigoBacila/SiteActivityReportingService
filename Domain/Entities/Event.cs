using Ardalis.GuardClauses;

namespace Domain.Entities
{
    public class Event
    {
        private Duration duration;
        private DateTime dateOfRegistration;

        protected Event() { }

        public Event(Duration duration, DateTime dateOfRegistration)
        {
            Duration = duration;
            DateOfRegistration = dateOfRegistration;
        }

        public Duration Duration
        {
            get => duration; set
            {
                Guard.Against.Null(value, nameof(value));
                duration = value;
            }
        }

        public DateTime DateOfRegistration
        {
            get => dateOfRegistration; set
            {
                Guard.Against.Null(value, nameof(value));
                dateOfRegistration = value;
            }
        }
    }
}