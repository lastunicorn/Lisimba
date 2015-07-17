using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace DustInTheWind.Lisimba.Forms
{
    class Model : INotifyPropertyChanged
    {
        private Image image = new Bitmap(1, 1);

        public Image Image
        {
            get { return image; }
            set
            {
                image = value ?? new Bitmap(1, 1);
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
