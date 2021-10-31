using System;
using System.Collections.Generic;
using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer.DependenciesProvider
{
    public class DependencyProvider:IDependencyProvider
    {
        private readonly IDependenciesConfig _config;

        public DependencyProvider(IDependenciesConfig config)
        {
            _config = config;
        }
        
        public TDependency Resolve<TDependency>(OrdinalMark mark = OrdinalMark.None) 
            where TDependency : class
        {
            return (TDependency)Resolve(typeof(TDependency), mark);
        }

        private object Resolve(Type dependencyType, OrdinalMark mark = OrdinalMark.None)
        {
            if (dependencyType.IsGenericType && dependencyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var genericArgument = dependencyType.GetGenericArguments()[0];
                return InstanceGenerator.GenerateCollectionOfInstances(genericArgument, _config.Implementations[genericArgument], _config);
            }

            if (!_config.Implementations.ContainsKey(dependencyType))
            {
                throw new ArgumentException("[ERROR] Dependency of type: " + dependencyType + " wasn't registered!");
            }
            
            return _config.Implementations[dependencyType].Find(bean => bean.Mark == mark)?.GetInstance(_config);
        }
        
    }
}