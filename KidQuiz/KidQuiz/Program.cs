using System;
using System.Windows.Forms;

namespace KidQuiz
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //if (System.IO.File.Exists("words.txt"))
            //{
            //    Application.Run(new frmQuiz(System.IO.File.ReadAllText("words.txt")));
            //}
            //else
            //{
            //    Application.Run(new frmQuiz());
            //}
            Application.Run(new frmDifficulty());
        }
    }
}
