using ModelMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapping
{
    public class ModelMapper<TEntity, TViewModel> : IModelMapper<TEntity, TViewModel>
        where TEntity : IMappable, new()
        where TViewModel : IMappable, new()
    {
        public virtual TViewModel MapToViewModel(TEntity entity)
        {
            if (entity == null) { return new TViewModel(); }
            var result = new TViewModel();
            this.Map(entity, result);
            return result;
        }
        public virtual TEntity MapToEntity(TViewModel viewModel)
        {
            if (viewModel == null) { return new TEntity(); }
            var result = new TEntity();
            this.Map(viewModel, result);
            return result;
        }
        private void Map(object from, object to)
        {
            if (from == null) { return; }
            foreach (var toPropertyInfo in to.GetType().GetProperties())
            {
                if (from.GetType().GetProperty(toPropertyInfo.Name) == null) { continue; }
                var fromSubObject = from.GetType().GetProperty(toPropertyInfo.Name).GetValue(from);

                if (fromSubObject is IMappable || (fromSubObject.GetType().IsClass 
                                                    && !fromSubObject.GetType().IsValueType
                                                    && !fromSubObject.GetType().IsPrimitive))
                {
                    object toSubObject = null;
                    try
                    {
                        toSubObject = Activator.CreateInstance(toPropertyInfo.PropertyType);
                    }
                    catch (Exception) { }
                    if (toSubObject != null)
                    {
                        to.GetType().GetProperty(toPropertyInfo.Name).SetValue(to, toSubObject);
                        this.Map(fromSubObject, toSubObject);
                        continue;
                    }
                }

                Type t = Nullable.GetUnderlyingType(toPropertyInfo.PropertyType) ?? toPropertyInfo.PropertyType;
                object safeValue = (fromSubObject == null) ? null : Convert.ChangeType(fromSubObject, t);

                toPropertyInfo.SetValue(to, safeValue, null);
            }
        }
    }
}
