using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data.UnitOfWork
{
    public class UnitOfWorks :  IUnitOfWorks
    {
        IDbContextTransaction transaction;
        public ToyContext context { get; private set; }

        public UnitOfWorks()
            : this(new ToyContext())
        {
            this.context = new ToyContext();
        }

        public UnitOfWorks(ToyContext data)
        {
            this.context = data;
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null)
            {
                transaction.Commit();
            }
            else
            {
                Dispose();
            }
        }

        public void Rowback()
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }
            else
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            transaction.Dispose();
            this.context.Dispose();
        }
    }
}
