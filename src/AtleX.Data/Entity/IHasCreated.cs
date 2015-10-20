using System;

namespace AtleX.Data.Entity
{
    /// <summary>
    /// Represents an entity with a creation date
    /// </summary>
    public interface IHasCreated
    {
        /// <summary>
        /// The date and time when the record was
        /// created, in UTC
        /// </summary>
        DateTimeOffset Created { get; set; }
    }
}
