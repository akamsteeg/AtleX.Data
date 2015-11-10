using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AtleX.Data.Entity
{
    /// <summary>
    /// A <see cref="DbContextBase"/> instance extends a <see cref="DbContext"/>
    /// with tracking of creation and last modified dates of the managed entities
    /// </summary>
    public class DbContextBase : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DbContextBase"/>
        /// </summary>
        public DbContextBase()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of <see cref="DbContextBase"/>
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name of the connection string or the connection string
        /// itself to connect to a database
        /// </param>
        public DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /// <summary>
        /// Saves all the changes made in this context to the underlying database
        /// </summary>
        /// <returns>
        /// The number of changed and saved entities
        /// </returns>
        public override int SaveChanges()
        {
            if (this.ChangeTracker.HasChanges())
            {
                this.SetCreatedAndLastModified();
            }

            return base.SaveChanges();
        }

        /// <summary>
        /// Set the created and last modified dates
        /// for the changed entities
        /// </summary>
        private void SetCreatedAndLastModified()
        {
            foreach (DbEntityEntry dbObject in this.ChangeTracker.Entries())
            {
                if (dbObject.State != EntityState.Unchanged)
                {
                    if (dbObject.Entity is IHasCreated)
                    {
                        SetCreated((IHasCreated)dbObject.Entity);
                    }

                    if (dbObject.Entity is IHasLastModified)
                    {
                        SetLastModified((IHasLastModified)dbObject.Entity);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private static void SetCreated(IHasCreated entity)
        {
            if (entity.Created == null
                || entity.Created == DateTimeOffset.MinValue
                || entity.Created == DateTimeOffset.MaxValue
                )
            {
                entity.Created = DateTimeOffset.UtcNow;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">
        /// </param>
        private static void SetLastModified(IHasLastModified entity)
        {
            entity.LastModified = DateTimeOffset.UtcNow;
        }
    }
}
