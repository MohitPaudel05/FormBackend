    using System.ComponentModel.DataAnnotations;

    namespace FormBackend.DTOs
    {
        public class HostelTransportDetailDto
        {
            public int Id { get; set; }

            [Required, MaxLength(20)]
            public string ResidencyType { get; set; }

            [MaxLength(50)]
            public string TransportMethod { get; set; }
        }
    }
