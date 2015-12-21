using System;
using Microsoft.Practices.Unity;

namespace Lisimba.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UnityContainer container = DependencyContainerSetup.CreateContainer();

                LisimbaApplication lisimbaApplication = container.Resolve<LisimbaApplication>();
                lisimbaApplication.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey(true);
            }
        }
    }
}
