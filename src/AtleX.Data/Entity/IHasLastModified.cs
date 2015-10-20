using System;

namespace AtleX.Data.Entity
{
    /// <summary>
    /// Represents an entity with a last modified date
    /// </summary>
    public interface IHasLastModified
    {
        /// <summary>
        /// The date and time when the record was
        /// last modified, in UTC
        /// </summary>
        DateTimeOffset LastModified { get; set; }
    }
}
