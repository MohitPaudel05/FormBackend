namespace FormBackend.DTOs
{
    public class AchievementDto
    {
        public string Title { get; set; } = string.Empty;
        public string IssuingOrganization { get; set; } = string.Empty;
        public int YearReceived { get; set; }
    }
}
