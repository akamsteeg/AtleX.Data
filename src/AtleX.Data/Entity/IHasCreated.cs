using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Entity
{
    public interface IHasCreated
    {
        /// <summary>
        /// The date and time when the record was
        /// created, in UTC
        /// </summary>
        DateTimeOffset? Created { get; set; }
    }
}
