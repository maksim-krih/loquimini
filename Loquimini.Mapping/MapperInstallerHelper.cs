using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Loquimini.Mapping
{
    public static class MapperInstallerHelper
    {
        public static IEnumerable<Type> GetTypes()
        {
            var assemblies = new [] {
                Assembly.Load("Loquimini.ModelDTO"),
                Assembly.Load("Loquimini.Model"),
            };

            List<Type> profileTypes = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IProfileBase)) && !type.IsAbstract);

                profileTypes.AddRange(types);
            }

            return profileTypes;
        }
    }
}
