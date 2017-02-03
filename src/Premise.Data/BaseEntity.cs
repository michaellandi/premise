using System.ComponentModel.DataAnnotations;

namespace Premise.Data
{
    /// <summary>
    /// Represents a base entity model
    /// </summary>
    public abstract class BaseEntity<K> : IEntity<K>
    {
        /// <summary>
        /// The unique identifier of the record
        /// </summary>
        /// <returns></returns>
        [Display(Name="Unique Identifier")]
        public K Id { get; set; }
    }
}