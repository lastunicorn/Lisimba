using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
        }
    }
}