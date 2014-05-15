using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Presenters;

namespace DustInTheWind.Lisimba.Forms
{
    internal interface IAddContactView
    {
        AddContactPresenter Presenter { set; }
        
        Contact Contact { get; set; }

        void Show();

        void Close();
    }
}