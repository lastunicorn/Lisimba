using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.WinForms.Forms
{
    public partial class WarningsForm : Form
    {
        public List<Exception> Warnings { get; set; }

        public WarningsForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            IEnumerable<Control> labels = Warnings
                .Select(CreateLabelFor);

            listControl1.AddRange(labels.ToArray());

            base.OnShown(e);
        }

        private static Control CreateLabelFor(Exception exception)
        {
            Label control = new Label
            {
                Text = exception.Message,
                AutoSize = true
            };

            control.MouseEnter += HandleLabelMouseEnter;
            control.MouseLeave += HandleLabelMouseLeave;

            return control;
        }

        private static void HandleLabelMouseLeave(object sender, EventArgs e)
        {
            Control control = sender as Control;

            if (control == null)
                return;

            control.ForeColor = SystemColors.ControlText;
            control.BackColor = SystemColors.Control;
        }

        private static void HandleLabelMouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;

            if (control == null)
                return;

            control.ForeColor = SystemColors.HighlightText;
            control.BackColor = SystemColors.MenuHighlight;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}