using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Data.Repository;
using ProjectToyStore.Data.UnitOfWork;

namespace ProjectToyStore.Servise
{
    public interface IGenericServise<TEntity> where TEntity : BaseModel, new()
    {
        GenericRepository<TEntity> _repo { get; set; }
        IUnitOfWorks _unit { get; set; }

        void Delete(TEntity entity);
        void DeleteById(int id);
        List<TEntity> GetAll();
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, int page = 1, int pageSize = 1);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        TEntity GetByID(int? id);
        void Save(TEntity entity);
    }
}