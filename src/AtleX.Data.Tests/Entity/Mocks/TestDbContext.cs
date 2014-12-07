using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Tests.Entity.Mocks
{
    public class TestDbContext : DbContext
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
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
