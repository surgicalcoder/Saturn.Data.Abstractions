using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GoLive.Saturn.Data.Entities;

namespace GoLive.Saturn.Data.Abstractions
{
    public interface IReadonlyRepository : IDisposable
    {
        Task<T> ById<T>(string id, string overrideCollectionName = "") where T : Entity;
        Task<List<T>> ById<T>(List<string> IDs, string overrideCollectionName = "") where T : Entity;

        Task<List<Ref<T>>> ByRef<T>(List<Ref<T>> Item, string overrideCollectionName = "") where T : Entity;
        Task<T> ByRef<T>(Ref<T> Item, string overrideCollectionName = "") where T : Entity;
        Task<Ref<T>> PopulateRef<T>(Ref<T> Item, string overrideCollectionName = "") where T : Entity;
        
        Task<IQueryable<T>> All<T>(string overrideCollectionName = "") where T : Entity;
        
        Task<T> One<T>(Expression<Func<T, bool>> predicate, string overrideCollectionName = "") where T : Entity;
        
        Task<IQueryable<T>> Many<T>(Expression<Func<T, bool>> predicate, string overrideCollectionName = "") where T : Entity;
        Task<List<T>> Many<T>(Dictionary<string, object> WhereClause, string overrideCollectionName = "") where T : Entity;
        Task<IQueryable<T>> Many<T>(Expression<Func<T, bool>> predicate, int pageSize, int PageNumber, string overrideCollectionName = "") where T : Entity;
        Task<List<T>> Many<T>(Dictionary<string, object> WhereClause, int pageSize, int PageNumber, string overrideCollectionName = "") where T : Entity;
        
        Task<long> CountMany<T>(Expression<Func<T, bool>> predicate, string overrideCollectionName = "") where T : Entity;

        Task Watch<T>(Expression<Func<ChangedEntity<T>, bool>> predicate, ChangeOperation operationFilter, Action<T, string, ChangeOperation> callback, string overrideCollectionName = "") where T : Entity;
    }

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