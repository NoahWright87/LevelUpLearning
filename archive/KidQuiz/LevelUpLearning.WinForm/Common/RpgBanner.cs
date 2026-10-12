using LevelUpLearning.Core.Data;
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
    public partial class RpgBanner : UserControl
    {
        public RpgBanner()
        {
            InitializeComponent();

            //RefreshStats();
        }

        public void RefreshStats()
        {
            var user = DataController.State.CurrentUser;
            var character = user.Character;

            btnCharacter.Text = $"{user.DisplayName}"
                + $"{Environment.NewLine}Level {character.Level}"
                + $"{Environment.NewLine}//TODO: Set picture";

            lblHealth.Text = $"HP: {character.CurrentHealth} / {character.MaxHealth}";
            barHealth.Maximum = character.MaxHealth;
            barHealth.Value = character.CurrentHealth;

            lblMana.Text = $"MP: {character.CurrentMana} / {character.MaxMana}";
            barMana.Maximum = character.MaxMana;
            barMana.Value = character.CurrentMana;
        }

        private void btnCharacter_Click(object sender, EventArgs e)
        {
            MessageBox.Show("//TODO: Open character menu");
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("//TODO: Open the inventory");
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("//TODO: Open menu");
        }
    }
}
