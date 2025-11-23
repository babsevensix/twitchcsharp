public static class ValidoDalAlExtensions
{
    public static IQueryable<T> DopoLaData<T>(this IQueryable<T> query, DateTime when) 
        where T: IValidoDalAl
    {
        return query.Where(x=>x.ValidoDal > when);
    }

     public static IQueryable<T> ValidoOra<T>(this IQueryable<T> query) 
        where T: IValidoDalAl
    {
        DateTime now = DateTime.UtcNow;

        return query.Where(x=>x.ValidoDal > now && (x.ValidoAl == null || x.ValidoAl < now ));
    }
}