using System.Collections.Generic;
using ProjectToyStore.Data.Models;
using ToyStore.Filters;

namespace ToyStore.Models
{
    public interface IGenericList<Tentity, Tfilter>
        where Tentity : BaseModel
        where Tfilter : GenericFiler<Tentity>, new()
    {
        string ActionName { get; set; }
        IList<Tentity> AllItems { get; set; }
        string ControllerName { get; set; }
        Tfilter Filter { get; set; }
        IList<Tentity> Items { get; set; }
        int Pages { get; set; }
        int StartItem { get; set; }
    }
}