using System.ComponentModel.DataAnnotations;

namespace WebIPLDapperServices.Dtos
{
    public class TeamForUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Slogan { get; set; }
        [Required]
        public string City { get; set; }
    }
}
