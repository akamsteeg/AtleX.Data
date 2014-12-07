using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Entity
{
    public interface IHasCreated
    {
        DateTime? Created { get; set; }
    }
}
