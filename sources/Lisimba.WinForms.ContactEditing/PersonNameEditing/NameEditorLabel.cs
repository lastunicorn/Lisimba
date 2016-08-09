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

using System.Windows.Forms;
using DustInTheWind.Lisimba.Business.AddressBookModel;
using DustInTheWind.WinFormsCommon;
using DustInTheWind.WinFormsCommon.Utils;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.PersonNameEditing
{
    public partial class NameEditorLabel : Form
    {
        private readonly NameEditorLabelViewModel viewModel;

        public NameEditorLabel()
        {
            InitializeComponent();

            viewModel = new NameEditorLabelViewModel();

            labelError.Bind(x => x.Visible, viewModel, x => x.ErrorVisible, false, DataSourceUpdateMode.Never);

            labelFirstLabel.Bind(x => x.Visible, viewModel, x => x.FirstNameVisible, false, DataSourceUpdateMode.Never);
            labelFirst.Bind(x => x.Visible, viewModel, x => x.FirstNameVisible, false, DataSourceUpdateMode.Never);
            labelFirst.Bind(x => x.Text, viewModel, x => x.FirstName, false, DataSourceUpdateMode.Never);

            labelMiddleLabel.Bind(x => x.Visible, viewModel, x => x.MiddleNameVisible, false, DataSourceUpdateMode.Never);
            labelMiddle.Bind(x => x.Visible, viewModel, x => x.MiddleNameVisible, false, DataSourceUpdateMode.Never);
            labelMiddle.Bind(x => x.Text, viewModel, x => x.MiddleName, false, DataSourceUpdateMode.Never);

            labelLastLabel.Bind(x => x.Visible, viewModel, x => x.LastNameVisible, false, DataSourceUpdateMode.Never);
            labelLast.Bind(x => x.Visible, viewModel, x => x.LastNameVisible, false, DataSourceUpdateMode.Never);
            labelLast.Bind(x => x.Text, viewModel, x => x.LastName, false, DataSourceUpdateMode.Never);

            labelNickLabel.Bind(x => x.Visible, viewModel, x => x.NicknameVisible, false, DataSourceUpdateMode.Never);
            labelNick.Bind(x => x.Visible, viewModel, x => x.NicknameVisible, false, DataSourceUpdateMode.Never);
            labelNick.Bind(x => x.Text, viewModel, x => x.Nickname, false, DataSourceUpdateMode.Never);
        }

        public string LabelText
        {
            set
            {
                NameParser nameParser = new NameParser(value);

                if (nameParser.Success)
                {
                    PersonName personName = nameParser.Result;

                    viewModel.FirstName = personName.FirstName;
                    viewModel.MiddleName = personName.MiddleName;
                    viewModel.LastName = personName.LastName;
                    viewModel.Nickname = personName.Nickname;
                }
                else
                {
                    viewModel.FirstName = null;
                    viewModel.MiddleName = null;
                    viewModel.LastName = null;
                    viewModel.Nickname = null;
                }
            }
        }
    }
}