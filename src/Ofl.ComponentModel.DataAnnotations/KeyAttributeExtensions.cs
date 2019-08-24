using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Ofl.Linq.Expressions;
using Ofl.Reflection;

namespace Ofl.ComponentModel.DataAnnotations
{
    public static class KeyAttributeExtensions
    {
        public static PropertyInfo GetKeyProperty<T>() =>
            GetKeyProperty<T>(true);

        public static PropertyInfo GetKeyProperty<T>(bool inherit) =>
            typeof(T).GetKeyProperty(inherit);

        public static PropertyInfo GetKeyProperty(this Type type) =>
            type.GetKeyProperty(true);

        public static PropertyInfo GetKeyProperty(this Type type, bool inherit)
        {
            // Validate parameters.
            if (type == null) throw new ArgumentNullException(nameof(type));

            // Get the property with the key attribute.
            return type.GetPropertiesWithPublicInstanceGetters()
                .SingleOrDefault(p => p.GetCustomAttribute<KeyAttribute>(inherit) != null);
        }

        public static Func<TEntity, TKey> CreateKeyPropertyGetter<TEntity, TKey>() =>
            CreateKeyPropertyGetter<TEntity, TKey>(true);

        public static Func<TEntity, TKey> CreateKeyPropertyGetter<TEntity, TKey>(bool inherit) =>
            GetKeyProperty<TEntity>(inherit).CreateGetPropertyLambdaExpression<TEntity, TKey>().Compile();
    }
}
