using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class CartDto
    {
        [Required]
        public string Id { get; set; }
        public List<CartItemDto> CartITems { get; set; }
    }
}