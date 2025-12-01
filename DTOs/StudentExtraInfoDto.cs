namespace FormBackend.DTOs
{
    public class StudentExtraInfoDto
    {
        public List<string> ExtracurricularInterests { get; set; } = new(); // Sports, Music, etc.
        public string? OtherInterest { get; set; }

        public string HostellerStatus { get; set; } = string.Empty;
        public string Transportation { get; set; } = string.Empty;
    }
}
