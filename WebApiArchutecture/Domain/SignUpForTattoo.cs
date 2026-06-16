using Newtonsoft.Json;
using WebApiArchutecture.Infrastructure;
using SignUpEntity = WebApiArchutecture.Domain.SignUpForTattoo;
namespace WebApiArchutecture.Domain
{
    public class SignUpForTattoo
    {
        

        public int Id { get; set; }
        public string NumberOfClient { get; set; }
        public DateTime TimeOfSign { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int Sessions { get; set; }
        public byte[]? RowVersion { get; set; }
        
    }
}
