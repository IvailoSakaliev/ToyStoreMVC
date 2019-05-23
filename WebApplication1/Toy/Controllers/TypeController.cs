using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using Toy.Filters.EntityFilter;
using Toy.Models.ViewModels.Type;

namespace Toy.Controllers
{
    public class TypeController 
        :GenericController<TypeSubject,TypeVM,TypeList,TypeFilter,TypeServise>
    {

        public override TypeSubject PopulateEditItemToModel(TypeVM model, TypeSubject entity, int id)
        {
            entity.ID = id;
            entity.Name = model.Type;
            return entity;
        }

        public override TypeSubject PopulateItemToModel(TypeVM model, TypeSubject entity)
        {
            entity.Name = model.Type;
            return entity;
        }

        public override TypeVM PopulateModelToItem(TypeSubject entity, TypeVM model)
        {
            model.Type = entity.Name;
            return model;
        }
    }
}