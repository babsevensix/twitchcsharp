public static class IEntityExtensions
{
    public static IQueryable<T> ById<T>(this IQueryable<T> query, int id) where T : IEntity
    {
        return query.Where(x => x.Id == id);
    }

    public static T GetElementOrFail<T>(this IQueryable<T> query) where T : IEntity
    {
        T element = query.FirstOrDefault();

        if (element == null) throw new KeyNotFoundException();

        return element;

    }
}