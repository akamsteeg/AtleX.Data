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
                    if (dbObject is IHasCreated)
                        SetCreated((IHasCreated)dbObject);

                    if (dbObject.Entity is IHasLastModified)
                        SetLastModified((IHasLastModified)dbObject);
                }
            }
        }

        /// <summary>
        /// Set the last modified date for the specified <see
        /// cref="IHasLastModified"/> instance
        /// </summary>
        /// <param name="dbObject">
        /// The <see cref="IHasLastModified"/> instance to set the last modified
        /// date on
        /// </param>
        /// <returns>
        /// The modified <see cref="IHasLastModified"/> instance
        /// </returns>
        private static IHasLastModified SetLastModified(IHasLastModified dbObject)
        {
            dbObject.LastModified = DateTimeOffset.UtcNow;

            return dbObject;
        }

        /// <summary>
        /// Set creation date for the specified <see
        /// cref="IHasCreated"/> instance
        /// </summary>
        /// <param name="dbObject">
        /// The <see cref="IHasCreated"/> instance to set the creation
        /// date on
        /// </param>
        /// <returns>
        /// The modified <see cref="IHasCreated"/> instance
        /// </returns>
        private static IHasCreated SetCreated(IHasCreated dbObject)
        {
            if (dbObject.Created == null)
            {
                dbObject.Created = DateTimeOffset.UtcNow;
            }

            return dbObject;
        }
    }
}
