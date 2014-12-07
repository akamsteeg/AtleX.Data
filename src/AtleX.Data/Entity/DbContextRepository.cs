using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Entity
{
    public abstract class DbContextRepository<T> : IDisposable where T : DbContext
    {
        private T _context;
        protected T Context
        {
            get
            {
                return _context;
            }
        }

        public DbContextRepository(T context)
        {
            _context = context;
        }

        /// <summary>
        /// Open a connection to the database
        /// </summary>
        public void OpenConnection()
        {
            if (Context.Database.Connection.State != ConnectionState.Open)
            {
                Context.Database.Connection.Open();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _context.Database.Connection.Close();
            _context.Dispose();
            _context = null;

            this.Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Query data from the Context
        /// </summary>
        /// <typeparam name="TDataObject"></typeparam>
        /// <returns></returns>
        public IQueryable<TDataObject> Query<TDataObject>() where TDataObject : class
        {
            return GetObjectSet<TDataObject>();
        }

        /// <summary>
        /// Add a new object for saving it to the database
        /// </summary>
        /// <typeparam name="TDataObject"></typeparam>
        /// <param name="objectToAdd"></param>
        public void Add<TDataObject>(TDataObject objectToAdd) where TDataObject : class
        {
            GetObjectSet<TDataObject>().Add(objectToAdd);
        }

        /// <summary>
        /// Remove an object
        /// </summary>
        /// <typeparam name="TDataObject"></typeparam>
        /// <param name="objectToDelete"></param>
        public void Delete<TDataObject>(TDataObject objectToDelete) where TDataObject : class
        {
            GetObjectSet<TDataObject>().Remove(objectToDelete);
        }

        /// <summary>
        /// Saves all the changes made to the underlying databases
        /// </summary>
        /// <remarks>
        /// Entities decorated with <see cref="IHasCreated"/> and
        /// <see cref="IHasLastModified"/> have their values updated
        /// if necessary.
        /// </remarks>
        /// <returns></returns>
        public int SaveChanges()
        {
            if (Context.ChangeTracker.HasChanges())
            {
                foreach (DbEntityEntry dbObject in Context.ChangeTracker.Entries())
                {
                    if (dbObject.Entity is IHasCreated)
                    {
                        IHasCreated createdObject = (IHasCreated)dbObject.Entity;
                        if (createdObject.Created == null)
                        {
                            createdObject.Created = DateTime.UtcNow;
                        }
                    }
                    if (dbObject.Entity is IHasLastModified)
                    {
                        ((IHasLastModified)dbObject.Entity).LastModified = DateTime.UtcNow;
                    }
                }
            }
            return Context.SaveChanges();
        }

        protected virtual DbSet<TDataObject> GetObjectSet<TDataObject>() where TDataObject : class
        {
            this.OpenConnection();

            DbSet<TDataObject> result = Context.Set<TDataObject>();

            return result;
        }
    }
}
