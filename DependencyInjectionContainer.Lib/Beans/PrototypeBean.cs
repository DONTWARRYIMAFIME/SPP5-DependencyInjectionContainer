using System;
using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer.Beans
{
    public class PrototypeBean:Bean
    {
        public PrototypeBean(Type type, OrdinalMark mark) : base(type, mark)
        {
        }
        
        public override object GetInstance(IDependenciesConfig config)
        {
            return InstanceGenerator.GenerateInstance(this, config);
        }

    }
}