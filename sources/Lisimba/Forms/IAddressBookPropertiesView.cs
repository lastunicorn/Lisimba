using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Forms
{
    interface IAddressBookPropertiesView
    {
        void CreateBindings(AddressBookPropertiesViewModel viewModel);
        AddressBookPropertiesPresenter Presenter { set; }
        void ShowModalView();
    }
}