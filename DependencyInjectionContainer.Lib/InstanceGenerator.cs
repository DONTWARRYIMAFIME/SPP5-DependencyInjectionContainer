using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DependencyInjectionContainer.Beans;

namespace DependencyInjectionContainer
{
    public static class InstanceGenerator
    {
        public static object GenerateInstance(Bean bean, IDependenciesConfig config)
        {
            var type = bean.Type;
            
            var constructor = GetConstructorsSortedByDependenciesAmount(type, config).FirstOrDefault();
            if (constructor != null)
            {
                var arguments = GetArguments(constructor, config);

                var result = constructor.Invoke(arguments.ToArray());
                return result;
            }
            
            throw new ArgumentException("[ERROR] Instance of type: " + type + " doesn't have public constructors");
        }
        
        public static IEnumerable<object> GenerateCollectionOfInstances(Type dependencyType, List<Bean> beans, IDependenciesConfig config)
        {
            var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(dependencyType));
            beans.ForEach(bean => list?.Add(GenerateInstance(bean, config)));

            return list as IEnumerable<object>;
        }

        private static IEnumerable<object> GetArguments(ConstructorInfo constructor, IDependenciesConfig config)
        {
            return constructor.GetParameters()
                .Select(parameter => GetArgument(parameter.ParameterType, config)).ToList();
        }
        
        private static object GetArgument(Type parameterType, IDependenciesConfig config)
        {
            if (parameterType.IsGenericType)
            {
                var genericArgument = parameterType.GetGenericArguments()[0];
                return GenerateCollectionOfInstances(genericArgument, config.Implementations[genericArgument], config);
            }

            config.Implementations.TryGetValue(parameterType, out var beans);
            return beans?.FirstOrDefault()?.GetInstance(config);
        }
 
        private static IEnumerable<ConstructorInfo> GetConstructorsSortedByDependenciesAmount(Type type, IDependenciesConfig config)
        {
            var dependencyTypes = config.Implementations.Keys.ToList();
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            
            return constructors.OrderByDescending(constructor =>
                constructor.GetParameters().Count(param => dependencyTypes.Contains(param.ParameterType)));
        }
        
    }
}