using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Data.Repository;

namespace ProjectToyStore.Servise
{
    public interface IGenericServise<TEntity> where TEntity : BaseModel, new()
    {
        GenericRepository<TEntity> _repo { get; set; }

        void Delete(TEntity entity);
        void DeleteById(int id);
        List<TEntity> GetAll();
        TEntity GetLastElement();
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        void Delete(Expression<Func<TEntity, bool>> filter);
        TEntity GetByID(int? id);
        void Save(TEntity entity);
    }
}