﻿using System;

namespace Conference.Common.IocHelper
{
    public class ObjectContainer
    {
        /// <summary>Represents the current object container.</summary>
        public static IObjectContainer Current { get; private set; }

        /// <summary>Set the object container.</summary>
        /// <param name="container"></param>
        public static void SetContainer(IObjectContainer container)
        {
            ObjectContainer.Current = container;
        }

        /// <summary>Register a implementation type.</summary>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public static void RegisterType(Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            ObjectContainer.Current.RegisterType(implementationType, serviceName, life);
        }

        /// <summary>Register a implementer type as a service implementation.</summary>
        /// <param name="serviceType">The implementation type.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public static void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            ObjectContainer.Current.RegisterType(serviceType, implementationType, serviceName, life);
        }

        /// <summary>Register a implementer type as a service implementation.</summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public static void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            ObjectContainer.Current.Register<TService, TImplementer>(serviceName, life);
        }

        /// <summary>Register a implementer type instance as a service implementation.</summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="instance">The implementer type instance.</param>
        /// <param name="serviceName">The service name.</param>
        public static void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null) where TService : class where TImplementer : class, TService
        {
            ObjectContainer.Current.RegisterInstance<TService, TImplementer>(instance, serviceName);
        }

        /// <summary>Resolve a service.</summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <returns>The component instance that provides the service.</returns>
        public static TService Resolve<TService>() where TService : class
        {
            return ObjectContainer.Current.Resolve<TService>();
        }

        /// <summary>Resolve a service.</summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public static object Resolve(Type serviceType)
        {
            return ObjectContainer.Current.Resolve(serviceType);
        }

        /// <summary>Try to retrieve a service from the container.</summary>
        /// <typeparam name="TService">The service type to resolve.</typeparam>
        /// <param name="instance">The resulting component instance providing the service, or default(TService).</param>
        /// <returns>True if a component providing the service is available.</returns>
        public static bool TryResolve<TService>(out TService instance) where TService : class
        {
            return ObjectContainer.Current.TryResolve<TService>(out instance);
        }

        /// <summary>Try to retrieve a service from the container.</summary>
        /// <param name="serviceType">The service type to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public static bool TryResolve(Type serviceType, out object instance)
        {
            return ObjectContainer.Current.TryResolve(serviceType, out instance);
        }

        /// <summary>Resolve a service.</summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <returns>The component instance that provides the service.</returns>
        public static TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return ObjectContainer.Current.ResolveNamed<TService>(serviceName);
        }

        /// <summary>Resolve a service.</summary>
        /// <param name="serviceName">The service name.</param>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public static object ResolveNamed(string serviceName, Type serviceType)
        {
            return ObjectContainer.Current.ResolveNamed(serviceName, serviceType);
        }

        /// <summary>Try to retrieve a service from the container.</summary>
        /// <param name="serviceName">The name of the service to resolve.</param>
        /// <param name="serviceType">The type of the service to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public static bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return ObjectContainer.Current.TryResolveNamed(serviceName, serviceType, out instance);
        }
    }
}
