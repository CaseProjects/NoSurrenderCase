using System;

public class ServiceDescriptor
{
    public Type ServiceType { get; private set; }
    public object Implementation { get;  set; }

    public ServiceDescriptor(object implementation)
    {
        ServiceType = implementation.GetType();
        Implementation = implementation;
    }
    
}