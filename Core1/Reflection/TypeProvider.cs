using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Reflection
{
    public static class TypeProvider
    {
        public static Type GetTypeFromAnyReferencingAssembly(string typeName)
        {
            var referencedAssemblies = Assembly.GetEntryAssembly()
                .GetReferencedAssemblies()
                .Select(a => a.FullName);

            var assembeleis = AppDomain.CurrentDomain.GetAssemblies();
            var res =  assembeleis.Where(a => referencedAssemblies.Contains(a.FullName))
                .SelectMany(a => a.GetTypes().Where(x => x.Name == typeName))
                .FirstOrDefault();
            return res;
        }

    }    
}
