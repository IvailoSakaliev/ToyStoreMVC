using Microsoft.EntityFrameworkCore;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProjectToyStore.Data.Repository
{
    public class GenericRepository<Tentity> 
        : IGenericRepository<Tentity> where Tentity
        : BaseModel, new()
    {
        private ToyContext _context { get; set; }
        protected DbSet<Tentity> _set { get; set; }
        private IUnitOfWorks _unit { get; set; }

        public GenericRepository()
        {
            _unit = new UnitOfWorks();
            _context = _unit.context;
            _set = _context.Set<Tentity>();
        }

        public GenericRepository(UnitOfWorks unit)
        {
            _context = unit.context;
            _set = _context.Set<Tentity>();
        }


        public IList<Tentity> GetAll(Expression<Func<Tentity, bool>> filter, int page = 1, int pageSize = 10)
        {
            IQueryable<Tentity> query = _set;
            if (filter != null)
            {
                return _set.Where(filter)
                    .OrderBy(x => x.ID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                return _set.OrderBy(x => x.ID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

        }
        public List<Tentity> GetAll()
        {
            return _set.ToList();
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter)
        {
            if (filter != null)
            {
                return _set.Where(filter).ToList();
            }
            else
            {
                return _set.ToList();
            }
        }

        public Tentity GetByID(int? id)
        {
            return _set.Find(id);
        }

        public void Delete(Tentity entity)
        {
            _set.Remove(entity);
            Updatestation(entity, EntityState.Deleted);
        }

        private void Add(Tentity entity)
        {
            _set.Add(entity);
            Updatestation(entity, EntityState.Added);
        }

        private void Update(Tentity entity)
        {
            Updatestation(entity, EntityState.Modified);
        }

        public void Save(Tentity entity)
        {
            if (entity.ID == 0)
            {
                Add(entity);
            }
            else
            {
                Update(entity);
            }

        }

        private void Updatestation(Tentity entity, EntityState state)
        {
            var dbentry = _context.Entry(entity);
            dbentry.State = state;
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            Tentity entity = new Tentity();
            entity = GetByID(id);
            Delete(entity);
            _context.SaveChanges();
        }
    }
}
