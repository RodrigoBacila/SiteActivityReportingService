using Ardalis.GuardClauses;

namespace Domain.Entities
{
    public abstract class Entity
    {
        private string key;
        public string Key
        {
            get => key;
            set
            {
                Guard.Against.Null(value, nameof(value));
                key = value;
            }
        }
    }
}