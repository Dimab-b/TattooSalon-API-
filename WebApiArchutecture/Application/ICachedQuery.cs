namespace WebApiArchutecture.Application
{
    public interface ICachedQuery
    {
        string CacheKey { get; } 
        TimeSpan? Expiration { get; } 
    }
}
