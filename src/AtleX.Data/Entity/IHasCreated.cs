using System;

namespace AtleX.Data.Entity
{
    /// <summary>
    /// Represents an entity with a creation date
    /// </summary>
    public interface IHasCreated
    {
        /// <summary>
        /// The date and time when the record was created, in UTC
        /// 
        /// When setting the value explicitly, it's converted to UTC when saving
        /// the entity to the database
        /// </summary>
        DateTimeOffset Created { get; set; }
    }
}
