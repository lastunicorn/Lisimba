using System.Configuration;

namespace Lisimba.Cmd
{
    class ApplicationConfiguration
    {
        public string DefaultGateName
        {
            get { return ConfigurationManager.AppSettings["DefaultGate"]; }
        }

        public string DefaultAddressBookFileName
        {
            get { return ConfigurationManager.AppSettings["DefaultAddressBook"]; }
        }
    }
}
