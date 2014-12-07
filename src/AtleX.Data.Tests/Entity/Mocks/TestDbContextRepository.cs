using AtleX.Data.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Tests.Entity.Mocks
{
    public class TestDbContextRepository : DbContextRepository<TestDbContext>
    {
        public TestDbContextRepository()
            : base(new TestDbContext())
        {

        }
    }
}
