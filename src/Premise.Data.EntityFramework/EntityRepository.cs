using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Premise.Data.EntityFramework
{
    /// <summary>
    /// A generic entity framework data repository
    /// </summary>
    public class EntityRepository<K, T> : IRepository<K, T> where T : class, IEntity<K>
    {
        protected DbContext Context { get; set; }

        /// <summary>
        /// Constructs a new repository instance
        /// </summary>
        /// <param name="context">The database context</param>
        public EntityRepository(DbContext context)
        {
            Context = context;
        }

        #region Select

        /// <summary>
        /// Retrieves a given record by its key from the data store
        /// </summary>
        /// <param name="id">The identifier of the record</param>
        /// <returns></returns>
        public virtual T Get(K id)
        {
            return Context.Set<T>().FirstOrDefault(record => record.Id.Equals(id));
        }

        /// <summary>
        /// Retrieves a given record by its key from the data store (async)
        /// </summary>
        /// <param name="id">The identifier of the record</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(K id, CancellationToken cancelToken = default(CancellationToken))
        {
            return await Context.Set<T>().FirstOrDefaultAsync(record => record.Id.Equals(id), cancelToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves all records from the data store
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// Retrieves all records from the data store (async)
        /// </summary>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task<IQueryable<T>> GetAllAsync(CancellationToken cancelToken = default(CancellationToken))
        {
            return (await Context.Set<T>().ToListAsync(cancelToken).ConfigureAwait(false)).AsQueryable();
        }

        #endregion

        #region Insert

        /// <summary>
        /// /// Inserts a record into the data store
        /// </summary>
        /// <param name="record">The record to insert</param>
        public virtual void Insert(T record)
        {
            TryGenerateIds(record);

            Context.Set<T>().Add(record);
        }

        /// <summary>
        /// Inserts a record into the data store (async)
        /// </summary>
        /// <param name="record">The record to insert</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task InsertAsync(T record, CancellationToken cancelToken = default(CancellationToken))
        {
            TryGenerateIds(record);

            await Task.Run(() => Context.Set<T>().Add(record), cancelToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Inserts many records into the data store
        /// </summary>
        /// <param name="records">The records to insert into the data store</param>
        public virtual void InsertMany(IEnumerable<T> records)
        {
            TryGenerateIds(records);

            Context.Set<T>().AddRange(records);
        }

        /// <summary>
        /// /// Inserts many records into the data store (async)
        /// </summary>
        /// <param name="records">The records to insert into the data store</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task InsertManyAsync(IEnumerable<T> records, CancellationToken cancelToken = default(CancellationToken))
        {
            TryGenerateIds(records);

            await Task.Run(() => Context.Set<T>().AddRange(records), cancelToken).ConfigureAwait(false);
        }

        #endregion

        #region Update

        /// <summary>
        /// /// Updates a record in the data store
        /// </summary>
        /// <param name="record">The record to update</param>
        public virtual void Update(T record)
        {

        }

        /// <summary>
        /// /// Updates a record in the data store (async)
        /// </summary>
        /// <param name="record">The record to update</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual Task UpdateAsync(T record, CancellationToken cancelToken = default(CancellationToken))
        {
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Updates many records in the data store
        /// </summary>
        /// <param name="record">The records to update</param>
        public virtual void UpdateMany(IEnumerable<T> records)
        {

        }

        /// <summary>
        /// Updates many records in the data store
        /// </summary>
        /// <param name="record">The records to update</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual Task UpdateManyAsync(IEnumerable<T> records, CancellationToken cancelToken = default(CancellationToken))
        {
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Delete

        /// <summary>
        /// /// Deletes a record in the data store
        /// </summary>
        /// <param name="record">The record to delete</param>
        public virtual void Delete(T record)
        {
            Context.Set<T>().Remove(record);
        }

        /// <summary>
        /// Deletes a record in the data store (async)
        /// </summary>
        /// <param name="record">The record to delete</param>
        /// <param name="cancelToken">The cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(T record, CancellationToken cancelToken = default(CancellationToken))
        {
            await Task.Run(() => Context.Set<T>().Remove(record), cancelToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Deletes many records in the data store
        /// </summary>
        /// <param name="records">The records to delete</param>
        public virtual void DeleteMany(IEnumerable<T> records)
        {
            Context.Set<T>().RemoveRange(records);
        }

        /// <summary>
        /// Deletes many records in the data store (async)
        /// </summary>
        /// <param name="records">The records to delete</param>
        /// <param name="cancelToken">The cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task DeleteManyAsync(IEnumerable<T> records, CancellationToken cancelToken = default(CancellationToken))
        {
            await Task.Run(() => Context.Set<T>().RemoveRange(records), cancelToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a record by id in the data store
        /// </summary>
        /// <param name="id">The id of the record</param>
        public virtual void Delete(K id)
        {
            var record = Get(id);
            if (record != null)
            {
                Context.Set<T>().Remove(record);
            }
        }

        /// <summary>
        /// Deletes a record by id in the data store (async)
        /// </summary>
        /// <param name="id">The id of the record</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(K id, CancellationToken cancelToken = default(CancellationToken))
        {
            var record = await GetAsync(id, cancelToken).ConfigureAwait(false);
            if (record != null)
            {
                await Task.Run(() => Context.Set<T>().Remove(record), cancelToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// /// Deletes many records by id in the data store
        /// </summary>
        /// <param name="ids">The ids of the records</param>
        public virtual void DeleteMany(IEnumerable<K> ids)
        {
            var records = Context.Set<T>().Where(x => ids.Contains(x.Id));
            if (records != null && records.Any())
            {
                Context.Set<T>().RemoveRange(records);
            }
        }

        /// <summary>
        /// Deletes many records by id in the data store (async)
        /// </summary>
        /// <param name="ids">The ids of the records</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task DeleteManyAsync(IEnumerable<K> ids, CancellationToken cancelToken = default(CancellationToken))
        {
            var records = await Context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync(cancelToken).ConfigureAwait(false);
            if (records != null && records.Any())
            {
                await Task.Run(() => Context.Set<T>().RemoveRange(records), cancelToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region Transactions
        
        /// <summary>
        /// Commits pending transactions to the data store
        /// </summary>
        public virtual void Commit()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// /// Commits pending transactions to the data store (async)
        /// </summary>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual async Task CommitAsync(CancellationToken cancelToken = default(CancellationToken))
        {
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion

        #region Reload

        /// <summary>
        /// /// Reloads a given record from the data store
        /// </summary>
        /// <param name="record"></param>
        public virtual void Reload(T record)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reloads a given record from the data store (async)
        /// </summary>
        /// <param name="record">The record to reload</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public virtual Task ReloadAsync(T record, CancellationToken cancelToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        #endregion

        protected virtual void TryGenerateIds(T record)
        {
            TryGenerateIds(new [] { record });
        }

        protected virtual void TryGenerateIds(IEnumerable<T> records)
        {
            if (typeof(K) == typeof(Guid)) 
            {
                foreach (var record in records)
                {
                    record.Id = (K)(object)Guid.NewGuid();
                }
            }
        }
    }
}