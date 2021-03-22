using System;
using System.Drawing;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    public partial class CustomMessageBox : Form
    {
        private CustomMessageBox()
        {
            InitializeComponent();
        }

        public static void Show(string text, string title, Color backColor)
        {
            CustomMessageBox box = new CustomMessageBox();
            box.Text = title; 
            
            box.lblText.Text = text;
            box.lblText.BackColor = backColor;

            box.ShowDialog();
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
