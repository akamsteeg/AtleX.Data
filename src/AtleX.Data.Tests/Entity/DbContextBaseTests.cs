using AtleX.Data.Tests.Entity.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Tests.Entity
{
    [TestFixture]
    public class DbContextBaseTests
    {
        [Test]
        public void SetIHasCreated_Successful()
        {
            TestEntity te = new TestEntity();

            TestDbContext context = new TestDbContext();
            context.TestEntities.Add(te);

            context.SaveChanges();

            Assert.IsNotNull(te.Created);
        }
    }
}
