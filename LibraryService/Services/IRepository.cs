using System.Collections.Generic;

namespace LibraryService
{
    public interface IRepository<TModel, TId>
        where TModel : class
    {
        TId Add(TModel model);
        TId Update(TModel model);
        TId Delete(TId id);
        IList<TModel> GetAll();
        TModel Get(TId id);
    }
}
