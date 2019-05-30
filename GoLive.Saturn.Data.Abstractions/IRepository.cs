using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GoLive.Saturn.Data.Entities;

namespace GoLive.Saturn.Data.Abstractions
{
    public interface IRepository : IDisposable
    {
        Task<T> ById<T>(string id, string overrideCollectionName = "") where T : Entity;
        Task<List<T>> ById<T>(List<string> IDs, string overrideCollectionName = "") where T : Entity;


        Task<List<Ref<T>>> ByRef<T>(List<Ref<T>> Item, string overrideCollectionName = "") where T : Entity;
        Task<T> ByRef<T>(Ref<T> Item, string overrideCollectionName = "") where T : Entity;

        Task<Ref<T>> PopulateRef<T>(Ref<T> Item, string overrideCollectionName = "") where T : Entity;

        Task<IQueryable<T>> All<T>(string overrideCollectionName = "") where T : Entity;
        Task<T> One<T>(Expression<Func<T, bool>> predicate, string overrideCollectionName = "") where T : Entity;
        Task<IQueryable<T>> Many<T>(Expression<Func<T, bool>> predicate, string overrideCollectionName = "") where T : Entity;

        Task Add<T>(T entity, string overrideCollectionName = "") where T : Entity;
        Task AddMany<T>(IEnumerable<T> entities, string overrideCollectionName = "") where T : Entity;

        Task<bool> Update<T>(T entity, string overrideCollectionName = "") where T : Entity;
        Task UpdateMany<T>(List<T> entity, string overrideCollectionName = "") where T : Entity;


        Task Upsert<T>(T entity, string overrideCollectionName = "") where T : Entity;
        Task UpsertMany<T>(List<T> entity, string overrideCollectionName = "") where T : Entity;

        Task Delete<T>(T entity, string overrideCollectionName = "") where T : Entity;
        Task Delete<T>(string Id, string overrideCollectionName = "") where T : Entity;
        Task DeleteMany<T>(IEnumerable<T> entity, string overrideCollectionName = "") where T : Entity;
        Task DeleteMany<T>(List<string> IDs, string overrideCollectionName = "") where T : Entity;

        void DisposeConnection();
        void InitDatabase();


        Task Delete<T>(Expression<Func<T, bool>> filter, string overrideCollectionName = "") where T : Entity;


        Task<List<T>> Many<T>(Dictionary<string, object> WhereClause, string overrideCollectionName = "") where T : Entity;
        Task<long> CountMany<T>(Expression<Func<T, bool>> predicate, string overrideCollectionName = "") where T : Entity;
    }
}