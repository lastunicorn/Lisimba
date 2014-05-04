using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Egg;

namespace Lindani
{
	public partial class Form1 : Form
	{
		private Agenda oAgenda;
		private string m_sStatusText;
		
		// Lindani - in Zulu language: Wait/Be Patient

		public Form1()
		{
			InitializeComponent();

			this.oAgenda = new Agenda();

			this.m_sStatusText = "";
			this.toolStripStatusLabel1.Text = this.m_sStatusText;

			this.Text = Application.ProductName + " " + Application.ProductVersion;

			
			Person oAgendaItem = new Person();
			oAgendaItem.sFirstName = "Alexandru";
			oAgendaItem.sMidleName = "Nicolae";
			oAgendaItem.sLastName = "Iuga";
			oAgendaItem.alEmails.Add(new Email("coldblackstar@yahoo.com"));
			oAgendaItem.alEmails.Add(new Email("alex.iuga@gmail.com"));
			oAgendaItem.alPhones.Add(new Phone("0723002252"));
			//oAgendaItem.alDates.Add(new Date(13, 06, 2007, "Birthday"));
			oAgendaItem.oBirthday = new Date(13, 6, 1980);

			this.oAgenda.Add(oAgendaItem);

			oAgendaItem = new Person();
			oAgendaItem.sFirstName = "Adrian";
			oAgendaItem.sMidleName = "";
			oAgendaItem.sLastName = "Rauta";
			oAgendaItem.alEmails.Add(new Email("coldblackstar@yahoo.com"));
			oAgendaItem.alEmails.Add(new Email("alex.iuga@gmail.com"));
			oAgendaItem.alPhones.Add(new Phone("0723002252"));
			oAgendaItem.oBirthday = new Date(1, 8, 1980);

			this.oAgenda.Add(oAgendaItem);
			//*/

			this.Refresh();
		}

		public override void Refresh()
		{
			base.Refresh();
			
			this.listBox1.Items.Clear();

			for (int i = 0; i < this.oAgenda.Count; i++)
			{
				//this.listBox1.Items.Add(i + ") " + this.oAgenda[i]);
				this.listBox1.Items.Add(this.oAgenda[i]);
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.infoPerson1.Item = (Person)this.listBox1.SelectedItem;
		}

		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			this.infoPerson1.ReadOnly = false;
		}

		private void AgendaOpen()
		{
			Agenda oAgenda = null;
			this.openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((oAgenda = Agenda.LoadFromFile(this.openFileDialog1.FileName, FileFormat.Egg)) != null)
					{
						this.oAgenda = oAgenda;
						this.Refresh();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void AgendaSave()
		{
			if (!this.oAgenda.sFileName.Equals(""))
			{
				try
				{
					this.oAgenda.SaveToFile();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
			{
				this.AgendaSaveAs();
			}
		}

		private void AgendaSaveAs()
		{
			this.saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
			if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					this.oAgenda.SaveToFile(this.saveFileDialog1.FileName, FileFormat.Egg);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		#region Menu OnClick

		private void toolStripMenuItem_File_New_Click(object sender, EventArgs e)
		{

		}

		private void toolStripMenuItem_File_Open_Click(object sender, EventArgs e)
		{
			this.AgendaOpen();
		}

		private void toolStripMenuItem_File_Save_Click(object sender, EventArgs e)
		{
			this.AgendaSave();
		}

		private void toolStripMenuItem_File_SaveAs_Click(object sender, EventArgs e)
		{
			this.AgendaSaveAs();
		}

		private void toolStripMenuItem_Help_About_Click(object sender, EventArgs e)
		{
			FormAbout oFormAbout = new FormAbout();
			oFormAbout.ShowDialog();
			oFormAbout.Dispose();
		}

		#endregion Menu OnClick

		#region Mouse Over Meniu

		private void toolStripMenuItem_File_New_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Create a new Agenda.";
		}

		private void toolStripMenuItem_File_New_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_File_Open_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Open Agenda from file.";
		}

		private void toolStripMenuItem_File_Open_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_File_Save_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Save current opened Agenda.";
		}

		private void toolStripMenuItem_File_Save_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_File_SaveAs_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Save current opened Agenda with another name.";
		}

		private void toolStripMenuItem_File_SaveAs_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_File_Export_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Export current opened Agenda in another format.";
		}

		private void toolStripMenuItem_File_Export_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_File_Import_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Import Agenda from another format.";
		}

		private void toolStripMenuItem_File_Import_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_File_Exit_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Exit the program.";
		}

		private void toolStripMenuItem_File_Exit_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		private void toolStripMenuItem_Help_About_MouseEnter(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = "Info about the program.";
		}

		private void toolStripMenuItem_Help_About_MouseLeave(object sender, EventArgs e)
		{
			this.toolStripStatusLabel1.Text = this.m_sStatusText;
		}

		#endregion Mouse Over Meniu

		private void toolStripMenuItem_ImportFromYahooCSV_Click(object sender, EventArgs e)
		{
			Agenda oAgenda = null;
			this.openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
			this.openFileDialog1.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
			this.openFileDialog1.FileName = string.Empty;
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((oAgenda = Agenda.LoadFromFile(this.openFileDialog1.FileName, FileFormat.CsvYahoo)) != null)
					{
						this.oAgenda = oAgenda;
						this.Refresh();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}