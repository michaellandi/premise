namespace Premise.Data
{
    /// <summary>
    /// Represents a generic data store entity
    /// </summary>
    public interface IEntity<K>
    {
        /// <summary>
        /// The uniqye identifier of the record
        /// </summary>
        /// <returns></returns>
        K Id { get; set; }
    }
}