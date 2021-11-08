using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class StoreRepo : EntityRepo<Store>, IStoreRepo
    {
        private readonly ApplicationDbContext context;

        public StoreRepo(ApplicationDbContext context) :base(context)
        {
            this.context = context;
        }
        //public Store Add(Store store)
        //{
        //    context.Add(store);
        //    context.SaveChanges();
        //    return store;
        //}

        //public Store GetById(int id)
        //{
        //    return context.Stores.Find(id);
        //}

        //public IEnumerable<Store> GetAll()
        //{
        //    return context.Stores;
        //}

        //public Store Update(Store ChangedStore)
        //{
        //    var store = context.Stores.Attach(ChangedStore);
        //    store.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    context.SaveChanges();
        //    return ChangedStore;
        //}

        //public Store Delete(int Id)
        //{
        //    Store store = context.Stores.Find(Id);
        //    if (store != null)
        //    {
        //        context.Stores.Remove(store);
        //        context.SaveChanges();
        //    }
        //    return store;
        //}
    }
}
