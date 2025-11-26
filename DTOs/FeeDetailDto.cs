using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace FormBackend.DTOs
{
    public class FeeDetailDto
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FeeCategory { get; set; }
         [Precision(18, 2)] 
        public decimal? Amount { get; set; }
    }
}
