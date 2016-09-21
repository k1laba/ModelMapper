using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapping
{
    public class ModelMapper<TEntity, TViewModel> : IModelMapper<TEntity, TViewModel>
        where TEntity : new()
        where TViewModel : new()
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
            if (IsList(from))
            {
                HandleList(from, to);
                return;
            }
            foreach (var fromInfo in from.GetType().GetProperties())
            {
                if (ShouldIgnore(fromInfo)) { continue; }
                string toName = this.GetMappedPropertyName(fromInfo);
                var toInfo = to.GetType().GetProperty(toName);
                if (toInfo == null) { continue; }
                if (ShouldIgnore(toInfo)) { continue; }

                var fromObj = fromInfo.GetValue(from);
                if (fromObj == null)
                {
                    toInfo.SetValue(to, null);
                    continue;
                }

                if (NotPrimitive(fromObj))
                {
                    object toObj = TryInitializeInstance(toInfo);
                    if (toObj != null)
                    {
                        toInfo.SetValue(to, toObj);
                        this.Map(fromObj, toObj);
                        continue;
                    }
                }

                Type t = Nullable.GetUnderlyingType(toInfo.PropertyType) ?? toInfo.PropertyType;
                object safeValue = (fromObj == null) ? null : Convert.ChangeType(fromObj, t);

                toInfo.SetValue(to, safeValue);
            }
        }

        private string GetMappedPropertyName(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute<MappingAttribute>()?.GetPropertyName() ?? propInfo.Name;
        }
        private bool ShouldIgnore(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute<MappingAttribute>()?.ShouldIgnore() ?? false;
        }

        private object TryInitializeInstance(PropertyInfo toPropertyInfo)
        {
            try
            {
                return Activator.CreateInstance(toPropertyInfo.PropertyType);
            }
            catch (Exception) { return null; }
        }

        private bool NotPrimitive(object fromSubObject)
        {
            return (fromSubObject.GetType().IsClass && !fromSubObject.GetType().IsValueType
                                                    && !fromSubObject.GetType().IsPrimitive);
        }

        private void HandleList(object from, object to)
        {
            foreach (var fromItem in (System.Collections.IList)from)
            {
                var toType = to.GetType().GetGenericArguments()[0];
                var toItem = Activator.CreateInstance(toType);
                ((System.Collections.IList)to).Add(toItem);
                this.Map(fromItem, toItem);
            }
        }

        private bool IsList(object from)
        {
            return from is System.Collections.IList;
        }
    }
}
