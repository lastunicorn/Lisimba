using System.ComponentModel;
using System.Runtime.CompilerServices;
using DustInTheWind.Lisimba.Annotations;

namespace DustInTheWind.Lisimba.ViewModels
{
    public class AddressBookPropertiesViewModel : INotifyPropertyChanged
    {
        private string bookName;
        private bool bookNameEnabled;
        private string fileLocation;
        private int contactsCount;

        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged();
            }
        }

        public bool BookNameEnabled
        {
            get { return bookNameEnabled; }
            set
            {
                bookNameEnabled = value;
                OnPropertyChanged();
            }
        }

        public string FileLocation
        {
            get { return fileLocation; }
            set
            {
                fileLocation = value;
                OnPropertyChanged();
            }
        }

        public int ContactsCount
        {
            get { return contactsCount; }
            set
            {
                contactsCount = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}