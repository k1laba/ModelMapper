using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapping
{
    public interface IModelMapper<TEntity, TViewModel>
        where TEntity : new()
        where TViewModel : new()
    {
        TViewModel MapToViewModel(TEntity entity);
        TEntity MapToEntity(TViewModel viewModel);
    }
}
