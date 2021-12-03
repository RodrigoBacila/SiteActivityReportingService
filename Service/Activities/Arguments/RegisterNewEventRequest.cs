using System.ComponentModel.DataAnnotations;

namespace Service.Activities.Arguments
{
    public class RegisterNewEventRequest
    {
        [Range(0, int.MaxValue)]
        public int Value { get; set; }
    }
}
