namespace BCI_ASSESSET.Requests.Projects
{
    public class ProjectListRequest
    {
        public string Name { get; set; }
        public int Page { get; set; }
        public int RecordPerPage { get; set; }
    }
}
