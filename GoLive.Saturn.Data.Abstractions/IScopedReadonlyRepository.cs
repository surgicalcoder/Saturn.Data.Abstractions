using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GoLive.Saturn.Data.Entities;

namespace GoLive.Saturn.Data.Abstractions
{
    public interface IScopedReadonlyRepository : IDisposable
    {
        Task<T> ById<T, T2>(T2 scope, string id) where T : ScopedEntity<T2> where T2 : Entity;
        Task<List<T>> ById<T, T2>(T2 scope, List<string> IDs) where T : ScopedEntity<T2> where T2 : Entity;

        Task<IQueryable<T>> All<T, T2>(T2 scope) where T : ScopedEntity<T2> where T2 : Entity;

        Task<T> One<T, T2>(T2 scope, Expression<Func<T, bool>> predicate) where T : ScopedEntity<T2> where T2 : Entity;

        Task<IQueryable<T>> Many<T, T2>(T2 scope, Expression<Func<T, bool>> predicate) where T : ScopedEntity<T2> where T2 : Entity;
        Task<IQueryable<T>> Many<T, T2>(T2 scope, Expression<Func<T, bool>> predicate, int pageSize, int PageNumber) where T : ScopedEntity<T2> where T2 : Entity;

        Task<long> CountMany<T, T2>(T2 scope, Expression<Func<T, bool>> predicate) where T : ScopedEntity<T2> where T2 : Entity;
    }
}