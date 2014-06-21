// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.UserControls
{
    partial class CustomTreeView
    {
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomTreeView));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Phones");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Emails");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Web Sites");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Addresses");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Dates");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Messenger Ids");
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "phone");
            this.imageList1.Images.SetKeyName(1, "e-mail");
            this.imageList1.Images.SetKeyName(2, "website");
            this.imageList1.Images.SetKeyName(3, "address");
            this.imageList1.Images.SetKeyName(4, "date");
            this.imageList1.Images.SetKeyName(5, "mesengerid");
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
            treeNode1.ImageKey = "phone";
            treeNode1.Name = "Phones";
            treeNode1.SelectedImageKey = "phone";
            treeNode1.Text = "Phones";
            treeNode2.ImageKey = "e-mail";
            treeNode2.Name = "Emails";
            treeNode2.SelectedImageKey = "e-mail";
            treeNode2.Text = "Emails";
            treeNode3.ImageKey = "website";
            treeNode3.Name = "Web Sites";
            treeNode3.SelectedImageKey = "website";
            treeNode3.Text = "Web Sites";
            treeNode4.ImageKey = "address";
            treeNode4.Name = "Addresses";
            treeNode4.SelectedImageKey = "address";
            treeNode4.Text = "Addresses";
            treeNode5.ImageKey = "date";
            treeNode5.Name = "Dates";
            treeNode5.SelectedImageKey = "date";
            treeNode5.Text = "Dates";
            treeNode6.ImageKey = "mesengerid";
            treeNode6.Name = "Messenger Ids";
            treeNode6.SelectedImageKey = "mesengerid";
            treeNode6.Text = "Messenger Ids";
            this.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.SelectedImageIndex = 0;
            this.ShowLines = false;
            this.ShowPlusMinus = false;
            this.ShowRootLines = false;
            this.Size = new System.Drawing.Size(250, 191);
            this.TabIndex = 12;
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDoubleClick);
            this.ResumeLayout(false);

        }
    }
}
