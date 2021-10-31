using DependencyInjectionContainer.Enums;

namespace DependencyInjectionContainer.DependenciesProvider
{
    public interface IDependencyProvider
    {
        TDependency Resolve<TDependency>(OrdinalMark mark = OrdinalMark.None)
            where TDependency : class;
    }
}