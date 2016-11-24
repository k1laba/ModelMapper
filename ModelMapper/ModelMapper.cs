using ModelMapping.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapping
{
    public class ModelMapper : IModelMapper
    {
        private Dictionary<Type, Type> _bindingOptions;

        public ModelMapper() : this(new Dictionary<Type, Type>()) { }
        public ModelMapper(Dictionary<Type, Type> bindingOptions)
        {
            this._bindingOptions = bindingOptions ?? new Dictionary<Type, Type>();
        }
        public virtual TResult Map<TSource, TResult>(TSource source) where TResult : new() where TSource : new()
        {
            var result = new TResult();
            if (source == null) { return result; }
            this.Map(source, result);
            return result;
        }
        public virtual TResult Map<TResult>(object source) where TResult : new()
        {
            var mapMethod = this.GetType().GetMethods().Single(m => m.Name == "Map" && m.GetGenericArguments().Count() == 2);
            var generic = mapMethod.MakeGenericMethod(source.GetType(), typeof(TResult));
            return (TResult)generic.Invoke(this, new object[] { source });
        }
        public virtual void ClearBindings()
        {
            this._bindingOptions = new Dictionary<Type, Type>();
        }
        public virtual void Bind<TFrom, TTo>()
        {
            var key = typeof(TFrom);
            if (this._bindingOptions.ContainsKey(key))
            {
                throw new Exception($"type: {key} already added in bindingOptions");
            }
            this._bindingOptions.Add(key, typeof(TTo));
        }
        private void Map(object source, object destination)
        {
            if (source == null) { return; }
            if (IsCollection(source))
            {
                HandleCollection(source, destination);
                return;
            }
            foreach (var sourceInfo in source.GetType().GetProperties())
            {
                if (ShouldIgnore(sourceInfo)) { continue; }
                string destName = this.GetMappedPropertyName(sourceInfo);
                var destInfo = destination.GetType().GetProperty(destName);
                if (destInfo == null) { continue; }
                if (ShouldIgnore(destInfo)) { continue; }

                var sourceObj = sourceInfo.GetValue(source);
                if (sourceObj == null)
                {
                    destInfo.SetValue(destination, null);
                    continue;
                }

                if (NotPrimitive(sourceObj))
                {
                    object toObj = TryInitializeInstance(destInfo.PropertyType);
                    if (toObj != null)
                    {
                        destInfo.SetValue(destination, toObj);
                        this.Map(sourceObj, toObj);
                        continue;
                    }
                }

                Type t = Nullable.GetUnderlyingType(destInfo.PropertyType) ?? destInfo.PropertyType;
                object safeValue = (sourceObj == null) ? null : Convert.ChangeType(sourceObj, t);

                destInfo.SetValue(destination, safeValue);
            }
        }

        private string GetMappedPropertyName(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute<MapToAttribute>()?.GetPropertyName() ?? propInfo.Name;
        }
        private bool ShouldIgnore(PropertyInfo propInfo)
        {
            return propInfo.GetCustomAttribute<MapIgnoreAttribute>() != null;
        }

        private object TryInitializeInstance(Type sourceType)
        {
            try
            {
                Type toType = this._bindingOptions.ContainsKey(sourceType) ? this._bindingOptions[sourceType]
                                                                         : sourceType;
                return Activator.CreateInstance(toType);
            }
            catch (Exception) { return null; }

        }

        private bool NotPrimitive(object sourceObject)
        {
            return (sourceObject.GetType().IsClass && !sourceObject.GetType().IsValueType
                                                    && !sourceObject.GetType().IsPrimitive);
        }

        private void HandleCollection(object source, object destination)
        {
            foreach (var sourceItem in (IEnumerable)source)
            {
                var destType = destination.GetType().GetGenericArguments()[0];
                object destItem = TryInitializeInstance(destType);
                bool isPrimitive = destItem == null || !NotPrimitive(sourceItem);
                if (isPrimitive) { destItem = sourceItem; }

                var addMethod = destination.GetType().GetMethod("Add");
                addMethod.Invoke(destination, new object[] { destItem });
                if (isPrimitive) { continue; }
                this.Map(sourceItem, destItem);
            }
        }
        private bool IsCollection(object source)
        {
            return source.GetType()
                       .GetInterfaces()
                       .Any(i => i.IsGenericType &&
                                 i.GetGenericTypeDefinition() == typeof(ICollection<>));
        }
    }
}
