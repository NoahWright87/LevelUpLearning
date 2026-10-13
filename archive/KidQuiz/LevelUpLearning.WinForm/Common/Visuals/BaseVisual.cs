using LevelUpLearning.WinForm;
using System;
using System.Drawing;

namespace LevelUpLearning.WinForms.Common.Visuals
{
    public abstract class BaseVisual
    {
        protected Size BOUNDS => Program.Overlay.Size;

        public PointF position;
        public PointF velocity;
        public PointF acceleration;

        public bool IsDone = false;
        
        protected BaseVisual()
        {

        }

        public abstract void Draw(Graphics g);

        public virtual void Update(float elapsed)
        {
            UpdatePosition(elapsed);

            if (this.IsOutOfBounds) HandleOutOfBounds();
        }

        protected virtual void UpdatePosition(float elapsed)
        {
            velocity.X += acceleration.X * elapsed;
            velocity.Y += acceleration.Y * elapsed;

            position.X += velocity.X * elapsed;
            position.Y += velocity.Y * elapsed;
        }

        public virtual void HandleOutOfBounds()
        {
            Remove();
        }

        public bool IsOutOfBounds => this.position.X < 0
            || this.position.X > BOUNDS.Width
            || this.position.Y < 0
            || this.position.Y > BOUNDS.Width;

        public void Remove()
        {
            this.IsDone = true;
        }
    }
}
