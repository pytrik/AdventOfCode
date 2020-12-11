using System;
using System.Linq;
using System.Reflection;

namespace AOC
{
    public static class StringConverter
    {

        public static Func<object, T> ToType<T>()
        {
            var type = typeof(T);
            if (Nullable.GetUnderlyingType(type) != null)
            {
                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
                return (value) => (T)converter.ConvertFrom(value);
            }
            else if (type.IsValueType)
            {
                return (value) => (T)Convert.ChangeType(value, type);
            }
            else
            {
                var converterMethod = type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(method => method.Name == "Parse")
                    .SingleOrDefault(method =>
                    {
                        var parameters = method.GetParameters();
                        return parameters.Length == 1 && parameters.First().ParameterType == typeof(string);
                    });

                if (converterMethod == null)
                    throw new NoConverterFoundException($"Could not find a public static method on type '{type}' with name 'Parse' that accepts exactly 1 argument of type string");

                return (value) => (T)converterMethod.Invoke(null, new[] { value });
            }
        }
    }
}