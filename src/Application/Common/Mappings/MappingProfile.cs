using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace DocRouter.Application.Common.Mappings
{
    /// <summary>
    /// Class that implements <see cref="Profile"></see> to map objects
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Creates a new instance of MappingProfile
        /// </summary>
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
