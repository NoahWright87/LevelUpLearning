using LevelUpLearning.Core.Data;
using LevelUpLearning.WinForms.Common;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    
    static class Program
    {
        static frmBackground background;
        public static frmBackground Background
        {
            get
            {
                if (background == null)
                {
                    background = new frmBackground();
                    background.Show();
                }
                return background;
            }
        }
        static frmOverlay overlay;
        public static frmOverlay Overlay
        {
            get
            {
                if (overlay == null)
                {
                    overlay = new frmOverlay();
                    overlay.Show();
                }
                return overlay;
            }
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //TODO: This is awful exception handling - catch more specific things in specific places
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmLogin());

                DataController.SaveRoot();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Unhandled Exception occurred: {ex.Message}");
            }
        }
    }
}
