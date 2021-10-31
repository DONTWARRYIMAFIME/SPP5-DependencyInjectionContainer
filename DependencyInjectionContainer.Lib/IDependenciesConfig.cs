using System;
using System.Collections.Generic;
using DependencyInjectionContainer.Beans;
using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer
{
    public interface IDependenciesConfig
    {
        public Dictionary<Type, List<Bean>> Implementations { get; }
        
        void Register<TDependency, TImplementation>(Scope scope = Scope.Singleton, OrdinalMark mark = OrdinalMark.None) 
            where TDependency : class
            where TImplementation : TDependency;

        void Register(Type dependencyType, Type implementationType, Scope scope = Scope.Singleton, OrdinalMark mark = OrdinalMark.None);
    }
}