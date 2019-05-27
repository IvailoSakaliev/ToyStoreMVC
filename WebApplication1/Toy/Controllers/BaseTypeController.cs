using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectToyStore.Data.Models;
using ProjectToyStore.Servise.EntityServise;
using Toy.Filters.EntityFilter;
using Toy.Models.ViewModels.BaseTypes;

namespace Toy.Controllers
{
    public class BaseTypeController
        : GenericController<BaseType, BaseTypeEditVm, BaseTypeList, BaseTypeFilter, BaseTypeServise>
    {
        public override BaseType PopulateEditItemToModel(BaseTypeEditVm model, BaseType entity, int id)
        {
            entity.ID = id;
            entity.Name = model.Name;
            return entity;
        }

        public override BaseType PopulateItemToModel(BaseTypeEditVm model, BaseType entity)
        {
            entity.Name = model.Name;
            return entity;
        }

        public override BaseTypeEditVm PopulateModelToItem(BaseType entity, BaseTypeEditVm model)
        {
            model.Name = entity.Name;
            return model;
        }

        internal override string PopulateINdexType(BaseTypeList itemVM, int id)
        {
            throw new NotImplementedException();
        }
    }
}