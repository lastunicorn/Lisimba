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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace XmlTransform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(this.openFileDialog1.OpenFile());

                XmlNode book = xml.ChildNodes[1];
                XmlNode contacts = book.ChildNodes[1];
                XmlNode contact = null; ;
                XmlNode name = null;
                XmlAttribute xmlAttribute = null;

                for (int i = 0; i < contacts.ChildNodes.Count; i++)
                {
                    contact = contacts.ChildNodes[i];
                    name = xml.CreateNode(XmlNodeType.Element, "Name", null);
                    
                    xmlAttribute = xml.CreateAttribute("First");
                    xmlAttribute.Value = contact.ChildNodes[0].InnerText;
                    name.Attributes.Append(xmlAttribute);

                    xmlAttribute = xml.CreateAttribute("Middle");
                    xmlAttribute.Value = contact.ChildNodes[1].InnerText;
                    name.Attributes.Append(xmlAttribute);

                    xmlAttribute = xml.CreateAttribute("Last");
                    xmlAttribute.Value = contact.ChildNodes[2].InnerText;
                    name.Attributes.Append(xmlAttribute);
                    
                    xmlAttribute = xml.CreateAttribute("Nickname");
                    xmlAttribute.Value = contact.ChildNodes[3].InnerText;
                    name.Attributes.Append(xmlAttribute);

                    contact.RemoveChild(contact.ChildNodes[0]);
                    contact.RemoveChild(contact.ChildNodes[0]);
                    contact.RemoveChild(contact.ChildNodes[0]);
                    contact.RemoveChild(contact.ChildNodes[0]);

                    contact.PrependChild(name);
                }

                xml.Save("del.xml");
                
                
                
                //this.treeView1.Nodes.Clear();

                //for (int i = 0; i < xml.ChildNodes.Count; i++)
                //{
                //    this.AddTreeNode(this.treeView1.Nodes, xml.ChildNodes[i]);
                //}
            }
        }

        private void AddTreeNode(TreeNodeCollection treeNodeCollection, XmlNode xmlNode)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Text = xmlNode.Name;
            treeNode.Tag = xmlNode;
            treeNodeCollection.Add(treeNode);

            for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
            {
                this.AddTreeNode(treeNode.Nodes, xmlNode.ChildNodes[i]);
            }
        }
    }
}