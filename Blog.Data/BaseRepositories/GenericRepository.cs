using Blog.Data.Contexts;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Blog.Data.BaseRepositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public GenericRepository(MongoContext context, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(expression).ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
    {
        return expression == null
            ? await _collection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken)
            : await _collection.CountDocumentsAsync(expression, cancellationToken: cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(expression).AnyAsync(cancellationToken);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(expression).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
    {
        var filter = expression ?? (_ => true);
        return await _collection.Find(filter)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }
}