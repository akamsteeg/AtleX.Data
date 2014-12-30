using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtleX.Data.Entity
{
    public class DbContextBase : DbContext
    {
        public override int SaveChanges()
        {
            if (this.ChangeTracker.HasChanges())
            {
                foreach (DbEntityEntry dbObject in this.ChangeTracker.Entries())
                {
                    // Ignore unchanged items
                    if (dbObject.State == EntityState.Unchanged)
                        continue;

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

            return base.SaveChanges();
        }
    }
}
