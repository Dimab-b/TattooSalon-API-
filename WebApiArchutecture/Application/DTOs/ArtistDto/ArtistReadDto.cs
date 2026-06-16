namespace WebApiArchutecture.Application.DTOs.ArtistDto
{
    public class ArtistReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public decimal PriceForSession { get; set; }
        public decimal Experience { get; set; }
    }
}
