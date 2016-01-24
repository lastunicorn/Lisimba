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

namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class CustomTreeView
    {
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomTreeView));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "phone");
            this.imageList1.Images.SetKeyName(1, "e-mail");
            this.imageList1.Images.SetKeyName(2, "webSite");
            this.imageList1.Images.SetKeyName(3, "postalAddress");
            this.imageList1.Images.SetKeyName(4, "date");
            this.imageList1.Images.SetKeyName(5, "socialProfileId");
            // 
            // CustomTreeView
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FullRowSelect = true;
            this.ImageIndex = 0;
            this.ImageList = this.imageList1;
            this.LineColor = System.Drawing.Color.Black;
            this.Location = new System.Drawing.Point(4, 17);
            this.Name = "treeView1";
            this.SelectedImageIndex = 0;
            this.ShowLines = false;
            this.ShowPlusMinus = false;
            this.ShowRootLines = false;
            this.Size = new System.Drawing.Size(250, 191);
            this.TabIndex = 12;
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDoubleClick);
            this.ResumeLayout(false);

        }
    }
}
