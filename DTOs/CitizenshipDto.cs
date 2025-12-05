namespace FormBackend.DTOs
{
    public class CitizenShipDto
    {
        public string CitizenshipNumber { get; set; } = string.Empty;
        public DateOnly CitizenshipIssueDate { get; set; }
        public string CitizenshipIssueDistrict { get; set; } = string.Empty;

        public IFormFile CitizenshipFrontPhoto { get; set; }
        public IFormFile CitizenshipBackPhoto { get; set; }

        // File paths stored in database
        public string CitizenshipFrontPhotoPath { get; set; } = string.Empty;
        public string CitizenshipBackPhotoPath { get; set; } = string.Empty;

    }
}
