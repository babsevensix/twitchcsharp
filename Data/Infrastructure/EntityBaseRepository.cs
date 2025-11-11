using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public interface IEntityBaseRepository<T> where T : IEntity
{
    //IQueryable<T> GetAll();

    IQueryable<T> All { get; }

    IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

    void Add(T entity);
    void Delete(T entity);

    T GetSingle(int id);

    void Edit(T entity);

    int SaveChanges();

}

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T: class, IEntity, new()
{
    WebApiDbContext _dbContext;

    public EntityBaseRepository(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> All => _dbContext.Set<T>();

    public void Add(T entity)
    {
        _dbContext.Add(entity);
    }

    public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = All;
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }
        return query;
     }

    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public void Edit(T entity)
    {
        _dbContext.Update(entity);
    }

    public T GetSingle(int id)
    {
        var singleValue = this.All.FirstOrDefault(x=> x.Id == id);
        if (singleValue == null)
        {
            throw new Exception("Not Found");
        }
        return singleValue;
    }

    public int SaveChanges()
    {
       return _dbContext.SaveChanges();
    }

    // public IQueryable<T> GetAll()
    // {
    //     return _dbContext.Set<T>();
    // }
}