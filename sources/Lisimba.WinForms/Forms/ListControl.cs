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

namespace DustInTheWind.Lisimba.Forms
{
    public partial class ListControl : UserControl
    {
        public int Count
        {
            get { return flowLayoutPanel1.Controls.Count; }
        }

        public ListControl()
        {
            InitializeComponent();
        }

        public void AddRange(Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Margin = new Padding(0);
                control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                control.Padding = new Padding(8);
            }

            flowLayoutPanel1.Controls.AddRange(controls);
        }

        public void Clear()
        {
            for (int i = 1; i < flowLayoutPanel1.Controls.Count; i++)
            {
                Control control = flowLayoutPanel1.Controls[i];
                flowLayoutPanel1.Controls.RemoveAt(i);
                control.Dispose();
            }
        }

        private void HandleLayout(object sender, LayoutEventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count == 0)
                return;

            //label1.Width = flowLayoutPanel1.ClientRectangle.Width;
            label1.Width = flowLayoutPanel1.Width - SystemInformation.VerticalScrollBarWidth;
        }
    }
}