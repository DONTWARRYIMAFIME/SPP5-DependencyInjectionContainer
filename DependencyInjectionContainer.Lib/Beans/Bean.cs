using System;
using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer.Beans
{
    public abstract class Bean
    {
        public Type Type { get; }
        public OrdinalMark Mark { get; }
        
        public Bean(Type type, OrdinalMark mark)
        {
            Type = type;
            Mark = mark;
        }

        public abstract object GetInstance(IDependenciesConfig config);
    }
}