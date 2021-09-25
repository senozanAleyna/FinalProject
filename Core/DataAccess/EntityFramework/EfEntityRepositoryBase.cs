using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    //çalışacağım tablo tipi ve bir de veri tabanı ver demek.
    //burada her şey tamamen generic hale geldi
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity> 
        where TEntity:class,IEntity,new()
        where TContext:DbContext,new() //kafana göre context alamazsın dbcontext ile çalışması gerekiyor
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //eklenecek olanı veri tabanına eşler (referansı yakala)
                addedEntity.State = EntityState.Added;//ekledi
                context.SaveChanges();//kaydetti
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //product tablosuna yerleş ve ve onları listeye çevir bana gönder
                //filtre yoksa ilk kısım çalışır filtre varsa ikinci kısım çalışır
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();

            }
        }

        //public List<TEntity> GetAllByCategory(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
