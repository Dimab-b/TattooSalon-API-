using WebApiArchutecture.Domain;

namespace WebApiArchutecture.Application.Features.Specifications.TattooQuerySpecification
{
    public class GetArtistsWithPriceSpecification : BaseSpecification<Artist>
    {
        public GetArtistsWithPriceSpecification(int price , int skip , int take) : base(p => p.PriceForSession > price)
        {
            ApplyPaging(skip, take);
            ApplyOrderBy(x=> x.Id);
        }
    }
}
