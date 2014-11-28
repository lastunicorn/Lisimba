using DustInTheWind.Lisimba.Presenters;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Forms
{
    public interface IAddressBookPropertiesView
    {
        void CreateBindings(AddressBookPropertiesViewModel viewModel);
        AddressBookPropertiesPresenter Presenter { set; }
        void ShowModalView();
    }
}