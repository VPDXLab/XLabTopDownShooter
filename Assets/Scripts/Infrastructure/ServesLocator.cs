using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public static class ServesLocator
    {
        private static Dictionary<Type, object> m_services = new();
        
        public static void Register<T>(T instance) => 
            m_services.Add(typeof(T), instance);

        public static T Resolve<T>() =>
            (T)m_services[typeof(T)];
    }
}
