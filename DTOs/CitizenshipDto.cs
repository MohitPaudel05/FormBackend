namespace FormBackend.DTOs
{
    public class CitizenShipDto
    {
        public string CitizenshipNumber { get; set; } = string.Empty;
        public DateOnly CitizenshipIssueDate { get; set; }
        public string CitizenshipIssueDistrict { get; set; } = string.Empty;

        public int StudentId { get; set; }
    }
}
