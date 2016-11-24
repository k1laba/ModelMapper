using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapping
{
    public interface IModelMapper
    {
        TDestination Map<TSource, TDestination>(TSource source) where TDestination : new() where TSource : new();
        TResult Map<TResult>(object source) where TResult : new();
        void Bind<TFrom, TTo>();
        void ClearBinding();
    }
}
