using AtleX.Data.Entity;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Tests.Entity.Mocks
{
    public class TestDbContext : DbContextBase
    {
        public TestDbContext()
        {
            List<TestEntity> entities = new List<TestEntity>(10);
            for (int i = 0; i < 10; i++)
            {
                entities.Add(new TestEntity()
                {
                    Name = i.ToString(),
                });
            }

            IQueryable<TestEntity> queryableList = entities.AsQueryable();

            Mock<DbSet<TestEntity>> mockDbSet = new Mock<DbSet<TestEntity>>();
            mockDbSet.As<IQueryable<TestEntity>>().Setup(s => s.Provider).Returns(queryableList.Provider);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(s => s.Expression).Returns(queryableList.Expression);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(s => s.ElementType).Returns(queryableList.ElementType);
            mockDbSet.As<IQueryable<TestEntity>>().Setup(s => s.GetEnumerator()).Returns(queryableList.GetEnumerator());

            this.TestEntities = mockDbSet.Object;

            Mock<DbChangeTracker> mockChangeTracker = new Mock<DbChangeTracker>();
            //mockChangeTracker.Setup<bool>(ct => ct.HasChanges()).Returns(true);
            mockChangeTracker.Setup<IEnumerable<DbEntityEntry>>(ct => ct.Entries()).Returns(() => { 
                List<DbEntityEntry> entityEntries = new List<DbEntityEntry>();
                foreach (TestEntity te in this.TestEntities)
                {
                    var dee = new Mock<DbEntityEntry<TestEntity>>();
                    dee.SetupGet(p => p.Entity).Returns(te);
                    dee.SetupGet(p => p.State).Returns(EntityState.Added);

                    entityEntries.Add(dee.Object);
                }
                return entityEntries;
            });

        }

        public void OpenConnection()
        {
            return;
        }

        public override int SaveChanges()
        {
            this.SetCreatedAndLastModified();

            return 0;
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
