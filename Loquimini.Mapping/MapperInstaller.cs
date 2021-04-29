using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Loquimini.Mapping
{
    public static class MapperInstaller
    {
        private static Func<Type, object> factoryMethod = Activator.CreateInstance;

        public static void Initialize()
        {
            var profileTypes = MapperInstallerHelper.GetTypes();
            
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var type in profileTypes)
                {
                    var dto = factoryMethod.Invoke(type);
                    cfg.CreateProfile(type.Name, tt => (dto as IProfileBase).Configure(cfg));
                }
            });
        }

        public static void Register(IServiceCollection serviceCollection)
        {
            var profileTypes = MapperInstallerHelper.GetTypes();

            serviceCollection.AddSingleton<IMapper>(c => new Mapper(new MapperConfiguration(cfg =>
            {
                foreach (var type in profileTypes)
                {
                    var dto = factoryMethod.Invoke(type);
                    cfg.CreateProfile(type.Name, tt => (dto as IProfileBase).Configure(cfg));
                }
            })));
        }
    }
}
