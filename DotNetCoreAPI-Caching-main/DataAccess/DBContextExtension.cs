using Demo7.DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo7.DataAccess
{
    public static class DbContextExtension
    {
        public static void ApplyStateChanges(this DbContext dbContext)
        {
            foreach (var dbEntityEntry in dbContext.ChangeTracker.Entries())
            {
                var entityState = dbEntityEntry.Entity as IObjectState;
                if (entityState == null)
                    throw new InvalidCastException(
                        "All entites must implement " +
                        "the IDALEnums.ObjectState interface, this interface " +
                        "must be implemented so each entites state" +
                        "can explicitely determined when updating graphs.");

                dbEntityEntry.State = ConvertState(entityState.EntityState);
            }
        }

        private static EntityState ConvertState(DALEnums.ObjectState state)
        {
            switch (state)
            {
                case DALEnums.ObjectState.Added:
                    return EntityState.Added;
                case DALEnums.ObjectState.Modified:
                    return EntityState.Modified;
                case DALEnums.ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
