namespace Domain.Entities
{
    public class Duration
    {
        public Duration(int totalSeconds)
        {
            TotalSeconds = totalSeconds;
        }

        public int TotalSeconds { get; }
    }
}