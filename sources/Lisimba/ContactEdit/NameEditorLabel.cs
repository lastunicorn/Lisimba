using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class NameEditorLabel : Form
    {
        public NameEditorLabel()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get { return label1.Text; }
            set
            {
                label1.Text = ParseText(value);
            }
        }

        private static string ParseText(string value)
        {
            NameParser nameParser = new NameParser(value);

            return !nameParser.Success
                ? "< Error >"
                : CreateText(nameParser.Result);
        }

        private static string CreateText(PersonName personName)
        {
            StringBuilder sb = new StringBuilder();

            if (personName.HasFirstName)
            {
                sb.Append("first: ");
                sb.Append(personName.FirstName);
            }

            if (personName.HasMiddleName)
            {
                if (sb.Length > 0)
                    sb.Append(" - ");

                sb.Append("middle: ");
                sb.Append(personName.MiddleName);
            }

            if (personName.HasLastName)
            {
                if (sb.Length > 0)
                    sb.Append(" - ");

                sb.Append("last: ");
                sb.Append(personName.LastName);
            }

            if (personName.HasNickname)
            {
                if (sb.Length > 0)
                    sb.Append(" - ");

                sb.Append("nick: ");
                sb.Append(personName.Nickname);
            }

            return sb.ToString();
        }
    }
}
