using System.ComponentModel.DataAnnotations;

namespace FormBackend.Enums
{
    public enum QualificationType
    {
        [Display(Name = "SLC/SEE")]
        SLC,
        [Display(Name = "+2")]
        Plus,
        Bachelors,
        Masters
    }
}