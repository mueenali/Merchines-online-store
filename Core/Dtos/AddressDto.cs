using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Street { get; set; }
        
        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string City { get; set; }
        
        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string State { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string ZipCode { get; set; }
    }
}