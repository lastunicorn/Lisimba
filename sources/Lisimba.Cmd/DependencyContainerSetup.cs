using System.Configuration;
using System.IO;
using System.Reflection;
using Lisimba.Cmd.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Lisimba.Cmd
{
    static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer container = new UnityContainer();

            LoadFromConfigurationFile(container);
            RegisterAdditionalTypes(container);

            return container;
        }

        private static void LoadFromConfigurationFile(IUnityContainer container)
        {
            UnityConfigurationSection unitySection = GetUnityConfigurationSection();
            container.LoadConfiguration(unitySection);
        }

        private static UnityConfigurationSection GetUnityConfigurationSection()
        {
            string unityConfigFilePath = GetUnityConfigFilePath();

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = unityConfigFilePath };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return (UnityConfigurationSection)configuration.GetSection("unity");
        }

        private static string GetUnityConfigFilePath()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string applicationDirectory = Path.GetDirectoryName(entryAssembly.Location);
            return Path.Combine(applicationDirectory, "Unity.config");
        }

        private static void RegisterAdditionalTypes(UnityContainer container)
        {
            container.RegisterInstance(container);
            container.RegisterType<ApplicationLoop>(new ContainerControlledLifetimeManager());
            container.RegisterType<AddressBooks>(new ContainerControlledLifetimeManager());
            container.RegisterType<ApplicationConfiguration>(new ContainerControlledLifetimeManager());
            container.RegisterType<Gates>(new ContainerControlledLifetimeManager());
        }
    }
}
