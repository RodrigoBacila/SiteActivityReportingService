namespace Domain.Entities
{
    public class Activity : Entity
    {
        protected Activity() { }

        public Activity(string key, IList<Event> events)
        {
            Key = key;
            Events = events ?? new List<Event>();
        }

        public IList<Event> Events { get; set; } = new List<Event>();

        public void PruneOldEvents()
        {
            Events = Events.Where(ev => ev.DateOfRegistration > DateTime.Now.AddHours(-12)).ToList();
        }
    }
}