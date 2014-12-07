using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Entity
{
    public interface IHasLastModified
    {
        DateTime? LastModified { get; set; }
    }
}
