using System;
using System.ComponentModel.DataAnnotations;

namespace Premise.Data.EntityFramework
{
    /// <summary>
    /// Represents an auditable entity that supports a standard 
    /// set of auditing fields
    /// </summary>
    public abstract class TrackerEntity<K> : BaseEntity<K>
    {
        /// <summary>
        /// The timestamp of record creation
        /// </summary>
        /// <returns></returns>
        [Display(Name="Created On")]
        public virtual DateTime CreatedOn { get; set; }

        /// <summary>
        /// The user that created the record
        /// </summary>
        /// <returns></returns>
        [Display(Name="Created By")]
        [StringLength(255)]
        public virtual string CreatedBy { get; set; }
          
        /// <summary>
        /// The timestamp of record modification
        /// </summary>
        /// <returns></returns>
        [Display(Name="Modified On")]
        public virtual DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// The last user that modified the record
        /// </summary>
        /// <returns></returns>
        [StringLength(255)]
        [Display(Name="Modified By")]
        public virtual string ModifiedBy { get; set; }
    }
}