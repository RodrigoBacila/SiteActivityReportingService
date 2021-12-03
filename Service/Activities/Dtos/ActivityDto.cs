namespace Service.Activities.Dtos
{
    public class ActivityDto
    {
        public string Key { get; set; }
        public IList<EventDto> Events { get; set; } = new List<EventDto>();
    }
}
