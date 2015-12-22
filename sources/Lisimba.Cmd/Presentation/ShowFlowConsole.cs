using System;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd.Presentation
{
    class ShowFlowConsole
    {
        public void DisplayContactDetails(Contact contact)
        {
            Console.WriteLine(contact.Name.ToString());
        }

        public void DisplayContactShort(Contact contact)
        {
            Console.WriteLine(contact.ToString());
        }
    }
}