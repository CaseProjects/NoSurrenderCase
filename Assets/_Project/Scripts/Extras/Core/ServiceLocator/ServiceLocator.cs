using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private static readonly IDictionary<Type, ServiceDescriptor> _serviceDescriptors =
        new Dictionary<Type, ServiceDescriptor>();


    public static void Register(ServiceDescriptor serviceDescriptor) =>
        _serviceDescriptors.Add(serviceDescriptor.ServiceType, serviceDescriptor);

    private static object GetInternal(Type serviceType)
    {
        _serviceDescriptors.TryGetValue(serviceType, out var serviceDescriptor);

        if (serviceDescriptor == null)
            throw new Exception($"Service of type {serviceType.Name} is not registered");

        return serviceDescriptor.Implementation;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Clear() => _serviceDescriptors.Clear();

    public static T Get<T>() => (T)GetInternal(typeof(T));
}