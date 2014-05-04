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