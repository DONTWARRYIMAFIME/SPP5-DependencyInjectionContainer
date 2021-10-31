using System;
using System.Collections.Generic;
using DependencyInjectionContainer.Beans;
using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer
{
    public class DependenciesConfig:IDependenciesConfig
    {
        public Dictionary<Type, List<Bean>> Implementations { get; } = new();
        
        public void Register<TDependency, TImplementation>(Scope scope = Scope.Singleton, OrdinalMark mark = OrdinalMark.None) 
            where TDependency : class 
            where TImplementation : TDependency
        {
            Register(typeof(TDependency), typeof(TImplementation), scope, mark);
        }

        public void Register(Type dependencyType, Type implementationType, Scope scope = Scope.Singleton, OrdinalMark mark = OrdinalMark.None)
        {
            switch (scope)
            {
                case Scope.Singleton:
                    AddBeanToImplementations(dependencyType, new SingletonBean(implementationType, mark));
                    return;
                case Scope.Prototype:
                    AddBeanToImplementations(dependencyType, new PrototypeBean(implementationType, mark));
                    return;
                default:
                    throw new ArgumentException("[ERROR] Unknown bean scope");
            }
        }

        private void AddBeanToImplementations(Type dependencyType, Bean bean)
        {
            if (Implementations.ContainsKey(dependencyType))
            {
                Implementations[dependencyType].Add(bean);
            }
            else
            {
                Implementations.Add(dependencyType, new List<Bean> { bean });
            }
        }
        
    }
}