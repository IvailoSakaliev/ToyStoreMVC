using ProjectToyStore.Data.Models;
using ProjectToyStore.Data.Repository;
using ProjectToyStore.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProjectToyStore.Servise
{
    public class GenericServise<TEntity> 
        : IGenericServise<TEntity> where TEntity
        : BaseModel, new()
    {
        public GenericRepository<TEntity> _repo { get; set; }
        public IUnitOfWorks _unit { get; set; }

        public GenericServise()
            : this(new UnitOfWorks())
        {


        }
        public GenericServise(UnitOfWorks unit)
        {
            _repo = new GenericRepository<TEntity>(unit);
            _unit = unit;
        }
        public List<TEntity> GetAll()
        {
            return _repo.GetAll();
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return (List<TEntity>)_repo.GetAll(filter);
        }
        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, int page = 1, int pageSize = 1)
        {
            return _repo.GetAll(filter, page, pageSize);
        }

        public TEntity GetByID(int? id)
        {
            return _repo.GetByID(id);
        }


        public void Save(TEntity entity)
        {
            try
            {
                _repo.Save(entity);
                _unit.Commit();
            }
            catch (Exception)
            {
                _unit.Rowback();
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _repo.Delete(entity);
                _unit.Commit();
            }
            catch (Exception)
            {
                _unit.Rowback();
            }
        }
        public void DeleteById(int id)
        {
            try
            {
                _repo.DeleteById(id);
                _unit.Commit();
            }
            catch (Exception)
            {
                _unit.Rowback();
            }
        }

    }
}
