using System.ComponentModel.DataAnnotations;

namespace TalabatApi.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; }

    }
}
