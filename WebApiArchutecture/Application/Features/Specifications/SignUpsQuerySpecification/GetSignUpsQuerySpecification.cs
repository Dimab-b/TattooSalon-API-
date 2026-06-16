using WebApiArchutecture.Application.Features.Specifications;
using WebApiArchutecture.Domain;

namespace WebApiArchitecture.Application.Features.Specifications.SignUpsQuerySpecification
{
    public class GetSignUpsQuerySpecification : BaseSpecification<SignUpForTattoo>
    {
        public GetSignUpsQuerySpecification(int skip , int take) 
        {
            AddInclude(s => s.Artist);
            ApplyPaging(skip, take);
            ApplyOrderBy(s => s.Id);
        }
    }
}
