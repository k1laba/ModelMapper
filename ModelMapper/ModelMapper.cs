using System;
using System.Collections;
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
            if (IsCollection(from))
            {
                HandleCollection(from, to);
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
                    object toObj = TryInitializeInstance(toInfo.PropertyType);
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

        private object TryInitializeInstance(Type fromType)
        {
            try
            {
                return Activator.CreateInstance(fromType);
            }
            catch (Exception) { return null; }
            
        }

        private bool NotPrimitive(object fromObject)
        {
            return (fromObject.GetType().IsClass && !fromObject.GetType().IsValueType
                                                    && !fromObject.GetType().IsPrimitive);
        }

        private void HandleCollection(object from, object to)
        {
            foreach (var fromItem in (IEnumerable)from)
            {
                var toType = to.GetType().GetGenericArguments()[0];
                object toItem = TryInitializeInstance(toType);
                bool isPrimitive = toItem == null || !NotPrimitive(fromItem);
                if (isPrimitive) { toItem = fromItem; }

                var addMethod = to.GetType().GetMethod("Add");
                addMethod.Invoke(to, new object[] { toItem });
                if (isPrimitive) { continue; }
                this.Map(fromItem, toItem);
            }
        }
        private bool IsCollection(object from)
        {
            return from.GetType()
                       .GetInterfaces()
                       .Any(i => i.IsGenericType && 
                                 i.GetGenericTypeDefinition() == typeof(ICollection<>));
        }
    }
}
