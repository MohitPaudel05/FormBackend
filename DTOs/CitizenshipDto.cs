using System.Text.Json.Serialization;

namespace FormBackend.DTOs
{
    public class CitizenShipDto
    {
        public string CitizenshipNumber { get; set; } = string.Empty;
        public DateOnly CitizenshipIssueDate { get; set; }
        public string CitizenshipIssueDistrict { get; set; } = string.Empty;
        [JsonIgnore]
        public IFormFile ?CitizenshipFrontPhoto { get; set; }
        [JsonIgnore]
        public IFormFile? CitizenshipBackPhoto { get; set; }

        // File paths stored in database
        public string ?CitizenshipFrontPhotoPath { get; set; } = string.Empty;
        public string ?CitizenshipBackPhotoPath { get; set; } = string.Empty;

    }
}
