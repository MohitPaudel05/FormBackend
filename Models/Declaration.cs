namespace FormBackend.Models
{
    public class Declaration
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public bool IsAgreed { get; set; }  // Checkbox: true if checked
        public DateOnly DateOfApplication { get; set; }  // Auto-filled
        public string Place { get; set; }
    }
}
