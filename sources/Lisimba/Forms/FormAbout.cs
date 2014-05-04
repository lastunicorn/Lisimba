using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Forms
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();

			Text = Application.ProductName;
			labelTitle.Text = Application.ProductName;
			labelVersion.Text = "Ver. " + Application.ProductVersion;
            labelAuthorAndDate.Text = "Iuga Alexandru - Dec 2007";
		}
	}
}