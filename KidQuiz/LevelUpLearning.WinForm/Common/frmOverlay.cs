using LevelUpLearning.Core;
using LevelUpLearning.WinForms.Common.Visuals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms.Common
{
    public partial class frmOverlay : Form
    {
        List<BaseVisual> visuals = new List<BaseVisual>();

        public frmOverlay()
        {
            InitializeComponent();
        }

        public void AnswerReaction(int amount, bool isCorrect)
        {
            Color color = isCorrect ? TextVisual.POSITIVE_COLOR : TextVisual.NEGATIVE_COLOR;

            float bottom = this.Height - 10;
            int margin = 10;


            for (int i = 0; i < amount; i++)
            {
                string text = isCorrect ? TextVisual.RANDOM_POSITIVE_TEXT : TextVisual.RANDOM_NEGATIVE_TEXT;
                visuals.Add(new TextVisual(color, text)
                {
                    position = new PointF(Utils.Random.Next(margin, this.Width - margin), bottom),
                    velocity = new PointF(0, -45 + (float)Utils.Random.NextDouble() * -15),
                    acceleration = new PointF(0, 1.5f + (float)Utils.Random.NextDouble() * 3)
                });
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.LimeGreen);

            foreach (var visual in visuals)
            {
                visual.Draw(e.Graphics);
            }
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            float elapsed = 1 / (float)timerAnimation.Interval;

            foreach (var visual in visuals)
            {
                visual.Update(elapsed);
            }

            CullVisuals();

            this.Refresh();
        }

        private void CullVisuals()
        {
            visuals.RemoveAll(x => x.IsDone);
        }
    }
}
