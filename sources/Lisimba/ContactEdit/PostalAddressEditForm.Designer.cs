namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class PostalAddressEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxCountry = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxZip = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Address:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.BackColor = System.Drawing.Color.Ivory;
            this.textBoxAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAddress.Location = new System.Drawing.Point(74, 14);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(280, 13);
            this.textBoxAddress.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "City:";
            // 
            // textBoxCity
            // 
            this.textBoxCity.BackColor = System.Drawing.Color.Ivory;
            this.textBoxCity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCity.Location = new System.Drawing.Point(74, 46);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(88, 13);
            this.textBoxCity.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "State:";
            // 
            // textBoxState
            // 
            this.textBoxState.BackColor = System.Drawing.Color.Ivory;
            this.textBoxState.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxState.Location = new System.Drawing.Point(74, 62);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(88, 13);
            this.textBoxState.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(172, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Country:";
            // 
            // textBoxCountry
            // 
            this.textBoxCountry.BackColor = System.Drawing.Color.Ivory;
            this.textBoxCountry.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCountry.Location = new System.Drawing.Point(266, 62);
            this.textBoxCountry.Name = "textBoxCountry";
            this.textBoxCountry.Size = new System.Drawing.Size(88, 13);
            this.textBoxCountry.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(172, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Postal Code:";
            // 
            // textBoxZip
            // 
            this.textBoxZip.BackColor = System.Drawing.Color.Ivory;
            this.textBoxZip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxZip.Location = new System.Drawing.Point(266, 46);
            this.textBoxZip.Name = "textBoxZip";
            this.textBoxZip.Size = new System.Drawing.Size(88, 13);
            this.textBoxZip.TabIndex = 3;
            // 
            // FormAddressEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 92);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxCity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxCountry);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxZip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddressEdit";
            this.Text = "";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCountry;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxZip;
    }
}