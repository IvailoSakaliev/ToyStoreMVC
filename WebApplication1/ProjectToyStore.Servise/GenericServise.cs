using ProjectToyStore.Data.Models;
using ProjectToyStore.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectToyStore.Servise
{
    public class GenericServise<TEntity> 
        : IGenericServise<TEntity> where TEntity
        : BaseModel, new()
    {
        public GenericRepository<TEntity> _repo { get; set; }
        
        public GenericServise()
        {
            _repo = new GenericRepository<TEntity>();
        }
        public List<TEntity> GetAll()
        {
            return _repo.GetAll();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return (List<TEntity>)_repo.GetAll(filter);
        }
        

        public TEntity GetByID(int? id)
        {
            return _repo.GetByID(id);
        }
        public TEntity GetLastElement()
        {
            return _repo.GetLastElement();
        }

        public void Save(TEntity entity)
        {
            try
            {
                _repo.Save(entity);
                
            }
            catch (Exception)
            {
               
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _repo.Delete(entity);
                
            }
            catch (Exception)
            {
                
            }
        }
        public void DeleteById(int id)
        {
            try
            {
                _repo.DeleteById(id);
            }
            catch (Exception)
            {
            }
        }
        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                _repo.Delete(filter);
            }
            catch (Exception)
            {
            }
        }

    }
}
