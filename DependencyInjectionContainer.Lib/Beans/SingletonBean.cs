using System;
using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer.Beans
{
    public class SingletonBean:Bean
    {
        private static readonly object SyncRoot = new();
        
        private object _instance;
            
        public SingletonBean(Type type, OrdinalMark mark) : base(type, mark)
        {
        }
        
        public override object GetInstance(IDependenciesConfig config)
        {
            if (_instance != null) return _instance;
            
            lock (SyncRoot)
            {
                return _instance ??= InstanceGenerator.GenerateInstance(this, config);
            }
        }
    }
}
