using WebApiArchutecture.Application.DTOs.ArtistDto;
using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Application.DTOs.SignUpForDto
{
    public class SignUpForTattooReadDto
    {
        public int Id { get; set; }
        public string NumberOfClient { get; set; }
        public DateTime TimeOfSign { get; set; }
        public Artist artist { get; set; }
        public int Sessions { get; set; }
    }
}
