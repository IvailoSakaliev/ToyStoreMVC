﻿using Microsoft.EntityFrameworkCore;
using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectToyStore.Data.Repository
{
    public class GenericRepository<Tentity> 
        : IGenericRepository<Tentity> where Tentity
        : BaseModel, new()
    {
        private ToyContext _context { get; set; }
        protected DbSet<Tentity> _set { get; set; }

        public GenericRepository()
        {
            _context = new ToyContext();
            _set = _context.Set<Tentity>();
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

        public Tentity GetLastElement()
        {
            return _set.LastOrDefault();
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

        public void Delete(Expression<Func<Tentity, bool>> filter)
        {
            List<Tentity> list = _set.Where(filter).ToList();
            foreach (var item in list)
            {
                _set.Remove(item);
                Updatestation(item, EntityState.Deleted);
            }
        }

        
    }
}
