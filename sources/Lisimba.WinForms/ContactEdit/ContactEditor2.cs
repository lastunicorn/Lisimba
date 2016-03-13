using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.WinFormsCommon;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
{
    internal partial class ContactEditor2 : UserControl, IContactEditorView
    {
        private ContactEditorViewModel viewModel;

        public ContactEditorViewModel ViewModel
        {
            get { return viewModel; }
            set
            {
                if (viewModel != null)
                    return;

                viewModel = value;

                if (viewModel != null)
                    CreateBindings();
            }
        }

        public ContactEditor2()
        {
            InitializeComponent();
        }

        private void CreateBindings()
        {
            pictureBox1.Bind(x => x.Image, ViewModel, x => x.Picture, true, DataSourceUpdateMode.Never);

            nameEditor1.Bind(x => x.PersonName, ViewModel, x => x.Name, true, DataSourceUpdateMode.OnPropertyChanged);
            nameEditor1.Bind(x => x.ActionQueue, ViewModel, x => x.ActionQueue, true, DataSourceUpdateMode.Never);

            birthdayView1.Bind(x => x.Birthday, ViewModel, x => x.Birthday, true, DataSourceUpdateMode.Never);
            birthdayView1.Bind(x => x.ActionQueue, ViewModel, x => x.ActionQueue, true, DataSourceUpdateMode.Never);
            birthdayView1.Bind(x => x.BiorhythmButtonViewModel, ViewModel, x => x.BiorhythmButtonViewModel, true, DataSourceUpdateMode.Never);

            zodiacSignView1.Bind(x => x.ZodiacSign, ViewModel, x => x.ZodiacSign, false, DataSourceUpdateMode.Never);

            contactItemsList1.Bind(x => x.ContactItems, ViewModel, x => x.ContactItems, true, DataSourceUpdateMode.Never);
            //customTreeView1.Bind(x => x.ContactItems, ViewModel, x => x.ContactItems, true, DataSourceUpdateMode.Never);
            //customTreeView1.Bind(x => x.ActionQueue, ViewModel, x => x.ActionQueue, true, DataSourceUpdateMode.Never);

            textBoxNotes.Bind(x => x.Text, ViewModel, x => x.Notes, false, DataSourceUpdateMode.OnPropertyChanged);

            this.Bind(x => x.Enabled, ViewModel, x => x.Enabled, false);
        }

        public void AddAddress(CustomObservableCollection<ContactItem> contactItems)
        {
        }

        public void AddDate(CustomObservableCollection<ContactItem> contactItems)
        {
        }

        public void AddEmail(CustomObservableCollection<ContactItem> contactItems)
        {
        }

        public void AddSocialProfileId(CustomObservableCollection<ContactItem> contactItems)
        {
        }

        public void AddPhone(CustomObservableCollection<ContactItem> contactItems)
        {
        }

        public void AddWebSite(CustomObservableCollection<ContactItem> contactItems)
        {
        }
    }
}