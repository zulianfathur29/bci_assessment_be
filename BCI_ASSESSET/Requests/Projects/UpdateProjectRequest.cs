namespace BCI_ASSESSET.Requests.Projects
{
    public class UpdateProjectRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Stage { get; set; }
        public string Category { get; set; }
        public string Others { get; set; }
        public DateOnly? StartDate { get; set; }
        public string Description { get; set; }
    }
}
