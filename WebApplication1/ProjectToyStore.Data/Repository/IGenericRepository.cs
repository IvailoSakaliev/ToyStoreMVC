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
        List<Tentity> GetAll();
        List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter);
        Tentity GetLastElement();
        Tentity GetByID(int? id);
        void Delete(Expression<Func<Tentity, bool>> filter);
        void Save(Tentity entity);
    }
}