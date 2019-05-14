using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProjectToyStore.Data.Models;

namespace ProjectToyStore.Data.Repository
{
    public interface IGenericRepository<Tentity> where Tentity : BaseModel, new()
    {
        void Delete(Tentity entity);
        void DeleteById(int id);
        IList<Tentity> GetAll();
        IList<Tentity> GetAll(Expression<Func<Tentity, bool>> filter);
        IList<Tentity> GetAll(Expression<Func<Tentity, bool>> filter, int page = 1, int pageSize = 10);
        Tentity GetByID(int? id);
        void Save(Tentity entity);
    }
}