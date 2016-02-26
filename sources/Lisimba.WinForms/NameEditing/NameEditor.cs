// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.NameEditing
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

        public ActionQueue ActionQueue { get; set; }

        public NameEditor()
        {
            InitializeComponent();

            nameEditorLabel = new NameEditorLabel();
        }

        private void HandleButtonEditResize(object sender, EventArgs e)
        {
            if (buttonEdit.Width < buttonEdit.Height)
                buttonEdit.Height = buttonEdit.Width;

            if (buttonEdit.Width > buttonEdit.Height)
                buttonEdit.Width = buttonEdit.Height;
        }

        private void HandleLabelNameClick(object sender, EventArgs e)
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

        private void HandleTextBoxNameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                bool success = UpdatePersonName(textBoxName.Text);

                if (success)
                    LeaveEditMode();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                LeaveEditMode();
            }
            else if (e.KeyCode == Keys.Back)
            {
                int cursorPosition = textBoxName.SelectionStart;

                bool openParenthesisIsDeleted = cursorPosition != 0 && textBoxName.Text[cursorPosition - 1] == '(';
                bool existsClosingParenthesis = cursorPosition < textBoxName.Text.Length && textBoxName.Text[cursorPosition] == ')';

                if (openParenthesisIsDeleted && existsClosingParenthesis)
                {
                    textBoxName.Text = textBoxName.Text.Remove(cursorPosition - 1, 2);
                    textBoxName.Select(cursorPosition - 1, 0);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void HandleTextBoxNameKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '(')
            {
                int cursorPosition = textBoxName.SelectionStart;

                if (cursorPosition < textBoxName.Text.Length && textBoxName.Text[cursorPosition] != ' ')
                {
                    textBoxName.SelectedText = "(";

                    if (textBoxName.Text.IndexOf(')', cursorPosition) == -1)
                        textBoxName.AppendText(")");
                }
                else
                {
                    textBoxName.SelectedText = "()";
                }

                textBoxName.Select(cursorPosition + 1, 0);

                e.Handled = true;
            }
        }

        private void HandleTextBoxNameTextChanged(object sender, EventArgs e)
        {
            nameEditorLabel.LabelText = textBoxName.Text;
        }

        private void HandleLabelNameMouseMove(object sender, MouseEventArgs e)
        {
            buttonEdit.Visible = Width - e.Location.X < buttonEdit.Width + buttonEdit.Margin.Left + buttonEdit.Margin.Right;

            tableLayoutPanel1.PerformLayout();
        }

        private void HandleButtonEditMouseLeave(object sender, EventArgs e)
        {
            buttonEdit.Visible = false;
        }

        private void HandleLabelNameMouseEnter(object sender, EventArgs e)
        {
            labelName.BackColor = SystemColors.Highlight;
            labelName.ForeColor = SystemColors.HighlightText;
        }

        private void HandleLabelNameMouseLeave(object sender, EventArgs e)
        {
            labelName.BackColor = SystemColors.Control;
            labelName.ForeColor = SystemColors.ControlText;
        }

        private void HandleButtonEditClick(object sender, EventArgs e)
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

        private void HandleTextBoxNameLeave(object sender, EventArgs e)
        {
            bool success = UpdatePersonName(textBoxName.Text);

            if (success)
                LeaveEditMode();
        }

        private bool UpdatePersonName(string nameString)
        {
            NameParser nameParser = new NameParser(nameString);

            if (!nameParser.Success)
                return false;

            PersonName newPersonName = nameParser.Result;

            if (!PersonName.Equals(newPersonName))
            {
                IAction action = new UpdateContactItemAction(PersonName, newPersonName);

                if (ActionQueue != null)
                    ActionQueue.Do(action);
                else
                    action.Do();
            }

            return true;
        }

        private void HandleTableLayoutPanelMouseLeave(object sender, EventArgs e)
        {
            buttonEdit.Visible = false;
        }

        protected virtual void OnPersonNameChanged()
        {
            EventHandler handler = PersonNameChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}