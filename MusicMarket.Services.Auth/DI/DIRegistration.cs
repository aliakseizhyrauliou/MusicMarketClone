using MusicMarket.Services.Auth.Attributes;
using MusicMarket.Services.Auth.DbStuff.Repositories.Attributes;
using System;
using System.Reflection;

namespace MusicMarket.Services.Auth.DI
{
    public static class DIRegistration
    {
        private static Type repositoryInterfaceAttribute = typeof(RepositoryAttribute);
        private static Type serviceInterfaceAttribute = typeof(ServiceAttribute);


        public static void RegisterAll(this WebApplicationBuilder builder)
        {
            RegisterRepositories(builder.Services);
            RegisterServices(builder.Services);
        }

        public static void RegisterRepositories(IServiceCollection services)
        {

            Assembly
                .GetAssembly(repositoryInterfaceAttribute)
                .GetTypes()
                .Where(type =>
                    type
                        .CustomAttributes
                        .Any(x => x.AttributeType == repositoryInterfaceAttribute)
                    || type
                            .GetConstructors()
                            .Any(constructor => constructor
                                .CustomAttributes
                                .Any(attribute => attribute.AttributeType == repositoryInterfaceAttribute)))
                .ToList()
                .ForEach(repoInterface =>
                {
                    var repoImplementation = Assembly.GetAssembly(repoInterface)
                    .GetTypes()
                    .SingleOrDefault(repoClass => repoClass.IsClass && repoInterface.IsAssignableFrom(repoClass));

                    Register(repoInterface, repoImplementation, services);

                });
        }
        public static void RegisterServices(IServiceCollection services) 
        {

            Assembly
                .GetAssembly(serviceInterfaceAttribute)
                .GetTypes()
                .Where(type =>
                    type
                        .CustomAttributes
                        .Any(x => x.AttributeType == serviceInterfaceAttribute)
                    || type
                            .GetConstructors()
                            .Any(constructor => constructor
                                .CustomAttributes
                                .Any(attribute => attribute.AttributeType == serviceInterfaceAttribute)))
                .ToList()
                .ForEach(serviceInterface =>
                {
                    var serviceImplementation = Assembly.GetAssembly(serviceInterface)
                    .GetTypes()
                    .SingleOrDefault(serviceClass => serviceClass.IsClass && serviceInterface.IsAssignableFrom(serviceClass));

                    Register(serviceInterface, serviceImplementation, services);

                });
        }

        private static void Register(Type interfaceOfType, Type implementation, IServiceCollection services) 
        {
            var constructors = implementation.GetConstructors();

            var constructor = constructors
                .OrderBy(methodInfo => methodInfo.GetParameters().Length)
                .Last();

            services.AddScoped(
                interfaceOfType,
                serviceProvider =>
                {
                    var parametersData = constructor
                        .GetParameters()
                        .Select(parameter => serviceProvider.GetService(parameter.ParameterType))
                        .ToArray();

                    return constructor.Invoke(parametersData);
                });
        }
    }
}
