using System.Collections.Generic;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.UserControls
{
    class ContactListViewModel : ViewModelBase
    {
        private ContactsSortingType selectedSortingMethod;

        public List<SortingComboBoxItem> SortingMethods { get; private set; }

        public ContactsSortingType SelectedSortingMethod
        {
            get { return selectedSortingMethod; }
            set
            {
                selectedSortingMethod = value;
                OnPropertyChanged();
            }
        }

        public ContactListViewModel()
        {
            SortingMethods = new List<SortingComboBoxItem>
            {
                new SortingComboBoxItem{ Text="Birthday (without year)", SortingType=ContactsSortingType.Birthday },
                new SortingComboBoxItem{ Text="Birth Date (age)", SortingType=ContactsSortingType.BirthDate },
                new SortingComboBoxItem{ Text="First Name", SortingType=ContactsSortingType.FirstName },
                new SortingComboBoxItem{ Text="Last Name", SortingType=ContactsSortingType.LastName },
                new SortingComboBoxItem{ Text="Nickname", SortingType=ContactsSortingType.Nickname },
                new SortingComboBoxItem{ Text="Nickname or Name", SortingType=ContactsSortingType.NicknameOrName }
            };
        }
    }
}