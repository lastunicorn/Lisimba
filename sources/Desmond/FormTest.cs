using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DustInTheWind.Desmond
{
    public partial class FormTest : Form
    {
        private RedEye d = new RedEye();

        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            d.Start();
        }

        private void FormTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            d.Stop();
        }
    }
}