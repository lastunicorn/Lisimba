using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.ContactEdit;

namespace DustInTheWind.Lisimba.WinForms.Forms
{
    internal partial class Form2 : Form
    {
        public Form2(ContactEditorViewModel contactEditorViewModel)
        {
            InitializeComponent();

            Contact contact = new Contact
            {
                Name = new PersonName("Alexandru", "Nicolae", "Iuga", "Alez"),
                Picture = Image.FromFile("c:\\Temp\\avatar_200.jpg"),
                //Birthday = new Date(13, 06, 1980),
                Notes = "some notes here"
            };

            contact.Items.AddRange(new ContactItem[]
            {
                new Date(15, 09, 1999),
                new PersonName("Elisabeta", "Maria", "Iuga", "Eliza"),
                new Email("me@alez.ro", "desc sad asd asd asf adg sdg sdfg srgser tser gsrg sfdg lksdfg ldsfhg lksjdfhgkljhdsfg lkdfg ldsf glhsdflg hldsfg h"),
                new Email("me2@alez.ro", "desc"),
                new Email("me32@alez.ro", "desc sad asd rg rfht xf f"),
                new Phone("0723 123456", "description")
            });

            contactEditorViewModel.Contact = contact;

            contactEditor21.ViewModel = contactEditorViewModel;
        }
    }
}