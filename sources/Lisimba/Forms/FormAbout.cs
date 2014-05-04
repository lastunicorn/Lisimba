using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();

			this.Text = Application.ProductName;
			this.labelTitle.Text = Application.ProductName;
			this.labelVersion.Text = "Ver. " + Application.ProductVersion;
            this.labelAuthorAndDate.Text = "Iuga Alexandru - Dec 2007";
		}
	}
}