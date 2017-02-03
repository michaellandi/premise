using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Premise.Data.EntityFramework
{
    /// <summary>
    /// Represents an auditable repository that supports a standard 
    /// set of auditing fields
    /// </summary>
    public class EntityTrackerRepository<K, T> : EntityRepository<K, T> where T : TrackerEntity<K>
    {
        protected string Username { get; set; }

        /// <summary>
        /// Constructs a new generic tracking repository
        /// </summary>
        /// <param name="base(context"></param>
        public EntityTrackerRepository(DbContext context, string username = "") : base(context)
        {
            Username = username;
        }

        #region Update

        /// <summary>
        /// /// /// Updates a record in the data store
        /// </summary>
        /// <param name="record">The record to update</param>
        public override void Update(T record)
        {
            SetModified(record);

            base.Update(record);
        }
        
        /// <summary>
        /// /// Updates a record in the data store (async)
        /// </summary>
        /// <param name="record">The record to update</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public override async Task UpdateAsync(T record, CancellationToken cancelToken = default(CancellationToken))
        {
            SetModified(record);

            await base.UpdateAsync(record, cancelToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates many records in the data store
        /// </summary>
        /// <param name="record">The records to update</param>
        public override void UpdateMany(IEnumerable<T> records)
        {
            SetModified(records);
        }

        /// <summary>
        /// Updates many records in the data store
        /// </summary>
        /// <param name="record">The records to update</param>
        /// <param name="cancelToken">A cancellation token used to cancel the task</param>
        /// <returns></returns>
        public override async Task UpdateManyAsync(IEnumerable<T> records, CancellationToken cancelToken = default(CancellationToken))
        {
            SetModified(records);

            await base.UpdateManyAsync(records, cancelToken).ConfigureAwait(false);
        }

        #endregion

        /// <summary>
        /// Sets the created audit fields
        /// </summary>
        /// <param name="record">The record to update</param>
        protected virtual void SetCreated(T record)
        {
            SetCreated(new [] { record });
        }

        /// <summary>
        /// Sets the created audit fields
        /// </summary>
        /// <param name="records">The records to update</param>
        protected virtual void SetCreated(IEnumerable<T> records)
        {
            foreach (var record in records)
            {
                record.CreatedOn = DateTime.UtcNow;
                record.CreatedBy = string.IsNullOrWhiteSpace(Username) ? 
                                        null : Username;
            }
        }

        /// <summary>
        /// Sets the modified audit fields
        /// </summary>
        /// <param name="record">The record to update</param>
        protected virtual void SetModified(T record)
        {
            SetModified(new[] { record });
        }

        /// <summary>
        /// Sets the modified audit fields
        /// </summary>
        /// <param name="record">The record to update</param>
        protected virtual void SetModified(IEnumerable<T> records)
        {
            foreach (var record in records)
            {
                record.ModifiedOn = DateTime.UtcNow; 
                record.ModifiedBy = string.IsNullOrWhiteSpace(Username) ? 
                                        null : Username;   
            }
        }
    }
}