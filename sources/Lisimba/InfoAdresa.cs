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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Lindani
{
	/// <summary>
	/// Summary description for InfoAdresa.
	/// </summary>
	public class InfoAdresa : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBox6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBox7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBox8;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InfoAdresa()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textBox8 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.LightYellow;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(40, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(224, 13);
			this.textBox1.TabIndex = 0;
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.Color.LightYellow;
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2.Location = new System.Drawing.Point(296, 0);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(40, 13);
			this.textBox2.TabIndex = 1;
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.Color.LightYellow;
			this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3.Location = new System.Drawing.Point(224, 16);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(40, 13);
			this.textBox3.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Str:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(272, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Nr.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(200, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Bl.";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(272, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "Ap.";
			// 
			// textBox4
			// 
			this.textBox4.BackColor = System.Drawing.Color.LightYellow;
			this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox4.Location = new System.Drawing.Point(296, 16);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(40, 13);
			this.textBox4.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(0, 32);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 16);
			this.label5.TabIndex = 5;
			this.label5.Text = "Oras:";
			this.label5.Click += new System.EventHandler(this.label5_Click);
			// 
			// textBox5
			// 
			this.textBox5.BackColor = System.Drawing.Color.LightYellow;
			this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox5.Location = new System.Drawing.Point(40, 32);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(104, 13);
			this.textBox5.TabIndex = 2;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 5;
			this.label6.Text = "Judet:";
			// 
			// textBox6
			// 
			this.textBox6.BackColor = System.Drawing.Color.LightYellow;
			this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox6.Location = new System.Drawing.Point(40, 48);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(104, 13);
			this.textBox6.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(160, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 16);
			this.label7.TabIndex = 5;
			this.label7.Text = "Tara:";
			// 
			// textBox7
			// 
			this.textBox7.BackColor = System.Drawing.Color.LightYellow;
			this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox7.Location = new System.Drawing.Point(224, 48);
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new System.Drawing.Size(112, 13);
			this.textBox7.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(160, 32);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 16);
			this.label8.TabIndex = 5;
			this.label8.Text = "Cod postal:";
			// 
			// textBox8
			// 
			this.textBox8.BackColor = System.Drawing.Color.LightYellow;
			this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox8.Location = new System.Drawing.Point(224, 32);
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.Size = new System.Drawing.Size(112, 13);
			this.textBox8.TabIndex = 2;
			// 
			// InfoAdresa
			// 
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBox8);
			this.Name = "InfoAdresa";
			this.Size = new System.Drawing.Size(336, 64);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void label5_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
