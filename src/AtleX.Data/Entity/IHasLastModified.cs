using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Entity
{
    public interface IHasLastModified
    {
        /// <summary>
        /// The date and time when the record was
        /// last modified, in UTC
        /// </summary>
        DateTimeOffset LastModified { get; set; }
    }
}
