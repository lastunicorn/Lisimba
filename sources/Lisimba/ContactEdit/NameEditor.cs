using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class NameEditor : UserControl
    {
        private PersonName personName;
        private readonly NameEditorLabel nameEditorLabel;

        public event EventHandler PersonNameChanged;

        public PersonName PersonName
        {
            get { return personName; }
            set
            {
                personName = value;

                if (personName != null)
                {
                    labelName.Text = personName.ToString();
                    textBoxName.Text = personName.ToString();
                }

                OnPersonNameChanged();
            }
        }

        public NameEditor()
        {
            InitializeComponent();

            nameEditorLabel = new NameEditorLabel();
        }

        private void buttonEdit_Resize(object sender, EventArgs e)
        {
            if (buttonEdit.Width < buttonEdit.Height)
                buttonEdit.Height = buttonEdit.Width;

            if (buttonEdit.Width > buttonEdit.Height)
                buttonEdit.Width = buttonEdit.Height;
        }

        private void labelName_Click(object sender, EventArgs e)
        {
            EnterEditMode();
        }

        private void EnterEditMode()
        {
            tableLayoutPanel1.Controls.Remove(labelName);
            tableLayoutPanel1.Controls.Add(textBoxName);

            textBoxName.Visible = true;

            nameEditorLabel.Location = textBoxName.GetBottomLeftCorner();
            nameEditorLabel.Show(this);

            textBoxName.Focus();
            textBoxName.Text = labelName.Text;
            textBoxName.SelectionStart = 0;
            textBoxName.SelectionLength = textBoxName.Text.Length;
        }

        private void LeaveEditMode()
        {
            tableLayoutPanel1.Controls.Remove(textBoxName);
            tableLayoutPanel1.Controls.Add(labelName);

            nameEditorLabel.Hide();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                NameParser nameParser = new NameParser(textBoxName.Text);

                if (!nameParser.Success)
                    return;

                PersonName.CopyFrom(nameParser.Result);

                LeaveEditMode();
            }

            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;

                LeaveEditMode();
            }
        }

        private void labelName_MouseMove(object sender, MouseEventArgs e)
        {
            if (Width - e.Location.X < buttonEdit.Width + buttonEdit.Margin.Left + buttonEdit.Margin.Right)
                buttonEdit.Visible = true;
            else
                buttonEdit.Visible = false;

            tableLayoutPanel1.PerformLayout();
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            buttonEdit.Visible = false;
        }

        private void labelName_MouseEnter(object sender, EventArgs e)
        {
            labelName.BackColor = SystemColors.Highlight;
            labelName.ForeColor = SystemColors.HighlightText;
        }

        private void labelName_MouseLeave(object sender, EventArgs e)
        {
            labelName.BackColor = SystemColors.Control;
            labelName.ForeColor = SystemColors.ControlText;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (personName == null)
                return;

            NameEditForm form = new NameEditForm
            {
                Location = labelName.GetBottomLeftCorner(),
                PersonName = personName
            };

            form.Show();
            form.Focus();
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            LeaveEditMode();
        }

        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            buttonEdit.Visible = false;
        }

        protected virtual void OnPersonNameChanged()
        {
            EventHandler handler = PersonNameChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            nameEditorLabel.LabelText = textBoxName.Text;
        }
    }
}