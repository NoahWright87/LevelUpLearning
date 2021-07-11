using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelUpLearning.WinForms
{
    public partial class frmMultipleChoice : Form
    {
        private int ReturnValue = -1;

        private frmMultipleChoice(params MultiChoiceOption[] choices)
        {
            InitializeComponent();

            CreateButtons(choices);
        }

        public static int GetChoice(IWin32Window owner, params MultiChoiceOption[] choices)
        {
            var instance = new frmMultipleChoice(choices);
            var result = instance.ShowDialog(owner);
            return instance.ReturnValue;
        }

        public void CreateButtons(params MultiChoiceOption[] choices)
        {
            for(int i = 0; i < choices.Length; i++)
            {
                var newButton = new Button()
                {
                    Dock = DockStyle.None,
                    Name = $"btnChoice{i}",
                    Size = new Size(600, 300),
                    Tag = i
                };
                newButton.Click += new EventHandler(this.btnChoice_Click);

                layout.Controls.Add(newButton);
            }
        }

        private void btnChoice_Click(object sender, EventArgs e)
        {
            
        }
    }

    //TODO: Form will take a list of these
    public class MultiChoiceOption
    {
        public string ButtonText;
        //public image ButtonImage;
        public string ToolTip;
        //what happens when it's clicked - what sort of variable would that be??
    }
}
