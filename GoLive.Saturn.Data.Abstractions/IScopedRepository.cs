using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GoLive.Saturn.Data.Entities;

namespace GoLive.Saturn.Data.Abstractions
{
    public interface IScopedRepository : IScopedReadonlyRepository
    {
        Task Add<T, T2>(T2 scope, T entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task AddMany<T, T2>(IEnumerable<T> entities, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task AddMany<T, T2>(T2 scope, IEnumerable<T> entities, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        
        Task Update<T, T2>(T2 scope, T entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task UpdateMany<T, T2>(List<T> entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task UpdateMany<T, T2>(T2 scope, List<T> entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;

        Task Upsert<T, T2>(T2 scope, T entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task UpsertMany<T, T2>(List<T> entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task UpsertMany<T, T2>(T2 scope, List<T> entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        
        Task Delete<T, T2>(T2 scope, T entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task Delete<T, T2>(T2 scope, Expression<Func<T, bool>> filter, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task Delete<T, T2>(T2 scope, string Id, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task DeleteMany<T, T2>(IEnumerable<T> entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task DeleteMany<T, T2>(T2 scope, IEnumerable<T> entity, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
        Task DeleteMany<T, T2>(T2 scope, List<string> IDs, string overrideCollectionName = "")  where T : ScopedEntity<T2> where T2 : Entity;
    }
}