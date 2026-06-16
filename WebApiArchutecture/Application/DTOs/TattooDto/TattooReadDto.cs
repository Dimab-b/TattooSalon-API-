using Microsoft.AspNetCore.Mvc;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Application.DTOs.TattooDto.TattooReadDto
{
   

    public class TattooReadDto
    {
        
        
        public int Id { get; set; }
        public string Size { get; set; }
        public Color Color { get; set; }
        public string Style { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfCreating { get; set; } = DateTime.UtcNow;
    }
}
