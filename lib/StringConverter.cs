using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AOC
{
    public static class StringConverter
    {
        public static Func<IEnumerable<string>, T> ToType<T>()
        {
            var objFunc = ToType(typeof(T), true);
            return (values) => (T)objFunc(values);
        }

        public static Func<IEnumerable<string>, IEnumerable<T>> ToTypeEnumerable<T>()
        {
            var objFunc = ToType(typeof(T), false);
            return (values) => values.Select(value => (T)objFunc(value));
        }

        private static Func<object, object> ToType(Type type, bool inputShouldBeEnumerable)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(type);
                return (value) => converter.ConvertFrom(value);
            }
            else if (type.IsValueType)
            {
                return (value) => Convert.ChangeType(value, type);
            }
            else
            {
                var converterMethod = type.GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(method => method.Name == "Parse")
                    .SingleOrDefault(method =>
                    {
                        var parameters = method.GetParameters();
                        if (inputShouldBeEnumerable)
                            return parameters.Length == 1 &&
                                   typeof(IEnumerable).IsAssignableFrom(parameters.First().ParameterType) &&
                                   parameters.First().ParameterType.GetGenericArguments().Length == 1 &&
                                   parameters.First().ParameterType.GetGenericArguments()[0] == typeof(string);
                        else
                            return parameters.Length == 1 &&
                                   parameters.First().ParameterType == typeof(string);
                    });

                if (converterMethod == null)
                    throw new NoConverterFoundException($"Could not find a public static method on type '{type}' with name 'Parse' that accepts exactly 1 argument of type string");

                return (value) => converterMethod.Invoke(null, new[] { value });
            }
        }
    }
}