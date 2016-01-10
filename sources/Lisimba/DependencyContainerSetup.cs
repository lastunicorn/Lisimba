using DustInTheWind.Lisimba.BookShell;
using DustInTheWind.Lisimba.Operations;
using DustInTheWind.Lisimba.Services;
using Microsoft.Practices.Unity;

namespace DustInTheWind.Lisimba
{
    static class DependencyContainerSetup
    {
        public static UnityContainer CreateContainer()
        {
            UnityContainer unityContainer = new UnityContainer();

            unityContainer.RegisterType<ProgramArguments>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ConfigurationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ApplicationStatus>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<RecentFiles>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<AddressBooks>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<UserInterface>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<LisimbaApplication>(new ContainerControlledLifetimeManager());

            unityContainer.RegisterType<OpenAddressBookOperation>(new ContainerControlledLifetimeManager());
            //unityContainer.RegisterType<ImportYahooCsvOperation>(new ContainerControlledLifetimeManager());
            //unityContainer.RegisterType<ExportYahooCsvOperation>(new ContainerControlledLifetimeManager());


            return unityContainer;
        }
    }
}