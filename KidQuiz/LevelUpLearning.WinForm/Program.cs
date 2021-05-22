using LevelUpLearning.Core.Data;
using System;
using System.Windows.Forms;

namespace LevelUpLearning.WinForm
{
    static class Program
    {
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
