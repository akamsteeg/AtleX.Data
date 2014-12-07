using AtleX.Data.Tests.Entity.Mocks;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Tests.Entity
{
    [TestFixture]
    public class DbContextRepositoryTests
    {
        protected TestDbContextRepository Repository;

        [SetUp]
        public void SetUp()
        {
            this.Repository = new TestDbContextRepository();
        }

        [Test]
        public void QueryDataSuccessfully()
        {
            var allItems = this.Repository.Query<TestEntity>().ToList();
            Assert.IsNotNull(allItems);
        }

        [Test]
        public void SetIHasCreated()
        {
            TestEntity t = new TestEntity()
            {
                Name = "test",
            };

            this.Repository.Add<TestEntity>(t);

            this.Repository.SaveChanges();

            Assert.IsNotNull(t.Created);
        }
    }
}
