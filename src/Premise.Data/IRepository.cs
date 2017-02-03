using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Premise.Data
{
    /// <summary>
    /// Represents a data repository for a given model T with a given key type of K
    /// </summary>
    public interface IRepository<K, T> where T : class, IEntity<K>
    {
        #region Select

        /// <summary>
        /// Retrieves a given record by its key from the data store
        /// </summary>
        /// <param name="id">The identifier of the record</param>
        /// <returns></returns>
        T Get(K id);

        /// <summary>
        /// Retrieves a given record by its key from the data store (async)
        /// </summary>
        /// <param name="id">The identifier of the record</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task<T> GetAsync(K id, CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Retrieves all records from the data store
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Retrieves all records from the data store (async)
        /// </summary>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task<IQueryable<T>> GetAllAsync(CancellationToken cancelToken = default(CancellationToken));

        #endregion

        #region Insert

        /// <summary>
        /// Inserts a record into the data store
        /// </summary>
        /// <param name="record">The record to insert</param>
        void Insert(T record);

        /// <summary>
        /// Inserts a record into the data store (async)
        /// </summary>
        /// <param name="record">The record to insert</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task InsertAsync(T record, CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Inserts many records into the data store
        /// </summary>
        /// <param name="records">The records to insert into the data store</param>
        void InsertMany(IEnumerable<T> records);

        /// <summary>
        /// Inserts many records into the data store (async)
        /// </summary>
        /// <param name="records">The records to insert into the data store</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task InsertManyAsync(IEnumerable<T> records, CancellationToken cancelToken = default(CancellationToken));

        #endregion

        #region Update

        /// <summary>
        /// Updates a record in the data store
        /// </summary>
        /// <param name="record">The record to update</param>
        void Update(T record);

        /// <summary>
        /// Updates a record in the data store (async)
        /// </summary>
        /// <param name="record">The record to update</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task UpdateAsync(T record, CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Updates many records in the data store
        /// </summary>
        /// <param name="record">The records to update</param>
        void UpdateMany(IEnumerable<T> record);

        /// <summary>
        /// Updates many records in the data store
        /// </summary>
        /// <param name="record">The records to update</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task UpdateManyAsync(IEnumerable<T> record, CancellationToken cancelToken = default(CancellationToken));

        #endregion

        #region Delete

        /// <summary>
        /// Deletes a record in the data store
        /// </summary>
        /// <param name="record">The record to delete</param>
        void Delete(T record);

        /// <summary>
        /// Deletes a record in the data store (async)
        /// </summary>
        /// <param name="record">The record to delete</param>
        /// <param name="cancelToken">The cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task DeleteAsync(T record, CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Deletes many records in the data store
        /// </summary>
        /// <param name="records">The records to delete</param>
        void DeleteMany(IEnumerable<T> records);

        /// <summary>
        /// Deletes many records in the data store (async)
        /// </summary>
        /// <param name="records">The records to delete</param>
        /// <param name="cancelToken">The cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task DeleteManyAsync(IEnumerable<T> records, CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Deletes a record by id in the data store
        /// </summary>
        /// <param name="id">The id of the record</param>
        void Delete(K id);

        /// <summary>
        /// Deletes a record by id in the data store (async)
        /// </summary>
        /// <param name="id">The id of the record</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task DeleteAsync(K id, CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Deletes many records by id in the data store
        /// </summary>
        /// <param name="ids">The ids of the records</param>
        void DeleteMany(IEnumerable<K> ids);

        /// <summary>
        /// Deletes many records by id in the data store (async)
        /// </summary>
        /// <param name="ids">The ids of the records</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task DeleteManyAsync(IEnumerable<K> ids, CancellationToken cancelToken = default(CancellationToken));

        #endregion

        #region Transactions
        
        /// <summary>
        /// Commits pending transactions to the data store
        /// </summary>
        void Commit();

        /// <summary>
        /// Commits pending transactions to the data store (async)
        /// </summary>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancelToken = default(CancellationToken));

        #endregion
        
        #region Reload

        /// <summary>
        /// Reloads a given record from the data store
        /// </summary>
        /// <param name="record"></param>
        void Reload(T record);

        /// <summary>
        /// Reloads a given record from the data store (async)
        /// </summary>
        /// <param name="record">The record to reload</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        Task ReloadAsync(T record, CancellationToken cancelToken = default(CancellationToken));

        #endregion
    }
}