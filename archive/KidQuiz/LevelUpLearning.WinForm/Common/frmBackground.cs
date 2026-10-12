using System.Drawing;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Common
{
    public partial class frmBackground : Form
    {
        //private readonly double FlashLength = 0.5;

        private readonly double OpacityChangePerTick;
        
        public frmBackground()
        {
            InitializeComponent();

            //TODO: The below isn't right.  Figure out that math so I can set a length of flash
            //  instead of doing things by some arbitrary logic
            //OpacityChangePerTick = timerAnimation.Interval / FlashLength;
            OpacityChangePerTick = 0.1;
        }

        private void timerAnimation_Tick(object sender, System.EventArgs e)
        {
            double newOpacity = this.Opacity - OpacityChangePerTick;
            if (newOpacity <= 0) this.Opacity = 0;
            else this.Opacity = newOpacity;
        }

        public void Flash(Color color, double initOpacity = 1.0)
        {
            this.BackColor = color;
            this.Opacity = initOpacity;
        }
    }
}
