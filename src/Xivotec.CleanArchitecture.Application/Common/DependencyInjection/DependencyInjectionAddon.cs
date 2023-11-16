using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Xivotec.CleanArchitecture.Application.Common.DependencyInjection;

public static class DependencyInjectionAddOn
{
    /// <summary>
    /// Registers all Implementations of Interface TInterface as Singleton in the DI Container.
    /// </summary>
    /// <typeparam name="TInterface">Interface Type to register.</typeparam>
    /// <param name="services">Implicit DI Container access point.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterInterfaceImplementationAsSingletons<TInterface>(
        this IServiceCollection services) where TInterface : class
    {
        var assembly = Assembly.GetCallingAssembly();
        return RegisterInterfaceBase<TInterface>(assembly, services, RegistrationType.Singleton);
    }

    /// <summary>
    /// Registers all Implementations of Interface TInterface as Singleton in the DI Container.
    /// </summary>
    /// <param name="services">Implicit DI Container access point.</param>
    /// <param name="interfaceType">Interface Type to register.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterInterfaceImplementationAsSingletons(
        this IServiceCollection services, Type interfaceType)
    {
        var assembly = Assembly.GetCallingAssembly();
        return RegisterInterfaceBase(assembly, services, interfaceType, RegistrationType.Singleton);
    }


    /// <summary>
    /// Registers all Implementations of Interface TInterface as Transient in the DI Container.
    /// </summary>
    /// <param name="services">Implicit DI Container access point.</param>
    /// <param name="interfaceType">Interface Type to register.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterInterfaceImplementationAsTransient(
        this IServiceCollection services, Type interfaceType)
    {
        var assembly = Assembly.GetCallingAssembly();
        return RegisterInterfaceBase(assembly, services, interfaceType, RegistrationType.Transient);
    }

    /// <summary>
    /// Registers all Implementations of Interface TInterface as Transient in the DI Container.
    /// </summary>
    /// <typeparam name="TInterface">Interface Type to register.</typeparam>
    /// <param name="services">Implicit DI Container access point.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterInterfaceImplementationAsTransient<TInterface>(
        this IServiceCollection services) where TInterface : class
    {
        var assembly = Assembly.GetCallingAssembly();
        return RegisterInterfaceBase<TInterface>(assembly, services, RegistrationType.Transient);
    }

    /// <summary>
    /// Registers all Implementations of Interface TInterface depending on the registrationType.
    /// </summary>
    /// <typeparam name="TInterface">Interface Type to register.</typeparam>
    /// <param name="assembly">The assembly where the types are located.</param>
    /// <param name="services">Implicit DI Container.</param>
    /// <param name="registrationType">Type to register as.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if TInterface is not an interface.</exception>
    /// <exception cref="NotSupportedException">Thrown if registrationType is not recognized.</exception>
    private static IServiceCollection RegisterInterfaceBase<TInterface>(
        Assembly assembly,
        IServiceCollection services,
        RegistrationType registrationType) where TInterface : class
    {
        if (!typeof(TInterface).IsInterface)
        {
            throw new ArgumentException($"{nameof(TInterface)} must be an interface type.");
        }

        var types = assembly.GetTypes();

        foreach (var type in types)
        {

            if (!typeof(TInterface).IsAssignableFrom(type) || type.IsAbstract || type.IsInterface)
            {
                continue;
            }

            RegisterInterfaceType(services, registrationType, type, typeof(TInterface));
        }

        return services;
    }

    /// <summary>
    /// Registers all Implementations of Interface TInterface depending on the registrationType.
    /// </summary>
    /// <param name="assembly">The assembly where the types are located.</param>
    /// <param name="services">Implicit DI Container.</param>
    /// <param name="openGenericType">The implementation type.</param>
    /// <param name="registrationType">Type to register as.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if TInterface is not an interface.</exception>
    /// <exception cref="NotSupportedException">Thrown if registrationType is not recognized.</exception>
    private static IServiceCollection RegisterInterfaceBase(
        Assembly assembly,
        IServiceCollection services,
        Type openGenericType,
        RegistrationType registrationType)
    {
        if (!openGenericType.IsInterface)
        {
            throw new ArgumentException($"{openGenericType.Name} must be an interface type.");
        }

        var exportedTypes = assembly.GetExportedTypes();

        foreach (var type in exportedTypes)
        {
            IEnumerable<Type> interfaces = type.GetInterfaces();
            var matchingInterface = interfaces.FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericType);

            if (matchingInterface is null || type.IsAbstract || type.IsInterface)
            {
                continue;
            }

            var genericTypeArgument = matchingInterface.GenericTypeArguments;

            var implementationType = openGenericType.MakeGenericType(genericTypeArgument);

            RegisterInterfaceType(services, registrationType, type, implementationType);
        }

        return services;
    }

    /// <summary>
    /// Registers the type as implementationType
    /// </summary>
    /// <param name="services">DI ServiceCollection</param>
    /// <param name="registrationType"></param>
    /// <param name="type"></param>
    /// <param name="implementationType"></param>
    /// <exception cref="NotSupportedException"></exception>
    private static void RegisterInterfaceType(
        IServiceCollection services,
        RegistrationType registrationType,
        Type type, Type implementationType)
    {
        _ = registrationType switch
        {
            RegistrationType.Transient => services.AddTransient(type),
            RegistrationType.Scoped => services.AddScoped(implementationType, type),
            RegistrationType.Singleton => services.AddSingleton(implementationType, type),
            _ => throw new NotSupportedException()
        };
    }

    /// <summary>
    /// Supported Registration type Enums
    /// </summary>
    private enum RegistrationType
    {
        Transient,
        Scoped,
        Singleton
    }
}
