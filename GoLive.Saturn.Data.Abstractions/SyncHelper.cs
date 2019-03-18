using System;
using System.Collections.Generic;
using System.Linq;
using GoLive.Saturn.Data.Entities;

namespace GoLive.Saturn.Data.Abstractions
{
    public static class SyncHelper
    {
        public static IList<TLocal> SyncFromLocal<TLocal, TRemote>(
            this IList<TLocal> Local,
            IList<TRemote> Remote,
            Func<TLocal, TRemote, bool> Identifier,
            Func<TLocal, TRemote, TLocal> PerformAssignments,
            Action<List<TLocal>> ItemsToDelete,
            Action<List<TLocal>> ItemsToUpsert
        )
            where TLocal : Entity, new() where TRemote : Entity, new()
        {
            var actualList = new List<TLocal>();

            foreach (TRemote remoteItem in Remote)
            {
                var item = Local.FirstOrDefault(f => Identifier.Invoke(f, remoteItem)) ?? new TLocal();

                if (PerformAssignments != null)
                {
                    item = PerformAssignments.Invoke(item, remoteItem);
                }
                else
                {
                    remoteItem.CopyPropertiesTo(item);
                }

                actualList.Add(item);
            }

            var toDelete = Local.Except(actualList).ToList();
            ItemsToDelete.Invoke(toDelete);
            ItemsToUpsert.Invoke(actualList);
            return Local;
        }

        public static IList<TLocal> SyncFromRemote<TLocal, TRemote>(
            this IList<TLocal> Local, 
            IList<TRemote> Remote, 
            Func<TLocal, TRemote, bool> Identifier, 
            Func<TLocal, TRemote, TLocal> PerformAssignments, 
            Action<List<TLocal>> ItemsToDelete, 
            Action<List<TLocal>> ItemsToUpsert
        ) 
            where TLocal : Entity, new()
        {
            var actualList = new List<TLocal>();

            foreach (TRemote remoteItem in Remote)
            {
                var item = Local.FirstOrDefault(f => Identifier.Invoke(f, remoteItem)) ?? new TLocal();
                item = PerformAssignments.Invoke(item, remoteItem);
                actualList.Add(item);
            }

            var toDelete = Local.Except(actualList).ToList();
            ItemsToDelete.Invoke(toDelete);
            ItemsToUpsert.Invoke(actualList);

            return Local;
        }


        public static IList<TLocal> SyncFrom<TLocal, TRemote>(this IList<TLocal> Local,
            IList<TRemote> Remote,
            Func<TLocal, TRemote, bool> Identifier,
            Func<TLocal, TRemote, TLocal> PerformAssignments,
            bool IsExternal,
            Action<List<TLocal>> ItemsToDelete,
            Action<List<TLocal>> ItemsToUpsert) where TLocal : Entity, new()
        {
            var actualList = new List<TLocal>();

            foreach (TRemote remoteItem in Remote)
            {
                var item = Local.FirstOrDefault(f => Identifier.Invoke(f, remoteItem)) ?? new TLocal();

                item = PerformAssignments.Invoke(item, remoteItem);

                actualList.Add(item);
            }

            var toDelete = Local.Except(actualList).ToList();
            ItemsToDelete.Invoke(toDelete);
            ItemsToUpsert.Invoke(actualList);
            return Local;
        }

        private static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {

            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).Where(f => f.Name != "Id").ToList();
            var destProps = typeof(TU).GetProperties().Where(x => x.CanWrite).Where(f => f.Name != "Id" ).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }

            }

        }
    }
}