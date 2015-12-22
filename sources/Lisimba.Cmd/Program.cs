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

                ApplicationLoop applicationLoop = container.Resolve<ApplicationLoop>();
                applicationLoop.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey(true);
            }
        }
    }
}
