namespace FormBackend.DTOs
{
    public class DisabilityDto
    {
        public string DisabilityStatus { get; set; } = "None";
        public string? DisabilityType { get; set; }
        public int? DisabilityPercentage { get; set; }

        public int StudentId { get; set; }
    }
}
