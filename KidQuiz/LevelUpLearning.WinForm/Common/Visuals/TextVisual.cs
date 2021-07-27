using LevelUpLearning.Core;
using System.Drawing;

namespace LevelUpLearning.WinForms.Common.Visuals
{
    public class TextVisual : BaseVisual
    {
        private static readonly Font font = new Font(FontFamily.GenericMonospace, 36);

        public static readonly string[] NEGATIVE_TEXT = new string[]
        {
            "X_X",
            "X__x",
            "x___X",
            "ヽ（≧□≦）ノ",
            "(* ￣︿￣)",
            "(╯‵□′)╯︵┻━┻",
            ":(",
            "q__q",
            ":'(",
            "T__T"
        };
        public static readonly string[] POSITIVE_TEXT = new string[]
        {
            ":)",
            ":D",
            ":O",
            "^___^",
            "^_^",
            "o__o;;",
            "UwU",
            ":}"
        };

        public static string RANDOM_NEGATIVE_TEXT => NEGATIVE_TEXT[Utils.Random.Next(NEGATIVE_TEXT.Length)];
        public static string RANDOM_POSITIVE_TEXT => POSITIVE_TEXT[Utils.Random.Next(POSITIVE_TEXT.Length)];

        public static Color NEGATIVE_COLOR => Color.DarkRed;
        public static Color POSITIVE_COLOR => Color.DarkGreen;

        string text;
        Brush brush;

        public TextVisual(Color color, string text)
        {
            this.brush = new SolidBrush(color);
            this.text = text;
        }

        public override void Draw(Graphics g)
        {
            //g.DrawEllipse(Pens.BlueViolet, position.X, position.Y, 1, 1);
            //g.FillEllipse(Brushes.Blue, position.X, position.Y, 10, 10);
            g.DrawString(text, font, brush, position.X, position.Y);
        }
    }
}
