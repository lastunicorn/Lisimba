using System;
using DustInTheWind.Lisimba.Egg.Book;

namespace Lisimba.Cmd.Presentation
{
    class NextBirthdaysFlowConsole
    {
        public void DisplayContactWithBirthday(Contact contact)
        {
            string text = string.Format("{0} : {1}", contact.Name, contact.Birthday);
            Console.WriteLine(text);
        }
    }
}