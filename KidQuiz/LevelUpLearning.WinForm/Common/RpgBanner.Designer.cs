
namespace LevelUpLearning.WinForms.Common
{
    partial class RpgBanner
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnCharacter = new System.Windows.Forms.Button();
            this.statsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.barMana = new System.Windows.Forms.ProgressBar();
            this.lblMana = new System.Windows.Forms.Label();
            this.lblHealth = new System.Windows.Forms.Label();
            this.barHealth = new System.Windows.Forms.ProgressBar();
            this.buttonLayout = new System.Windows.Forms.TableLayoutPanel();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            this.statsLayout.SuspendLayout();
            this.buttonLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 3;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayout.Controls.Add(this.btnCharacter, 0, 0);
            this.mainLayout.Controls.Add(this.statsLayout, 1, 0);
            this.mainLayout.Controls.Add(this.buttonLayout, 2, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(867, 180);
            this.mainLayout.TabIndex = 0;
            // 
            // btnCharacter
            // 
            this.btnCharacter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCharacter.Location = new System.Drawing.Point(3, 3);
            this.btnCharacter.Name = "btnCharacter";
            this.btnCharacter.Size = new System.Drawing.Size(144, 174);
            this.btnCharacter.TabIndex = 0;
            this.btnCharacter.Text = "Character Pic / Level";
            this.btnCharacter.UseVisualStyleBackColor = true;
            this.btnCharacter.Click += new System.EventHandler(this.btnCharacter_Click);
            // 
            // statsLayout
            // 
            this.statsLayout.ColumnCount = 1;
            this.statsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.statsLayout.Controls.Add(this.barMana, 0, 3);
            this.statsLayout.Controls.Add(this.lblMana, 0, 2);
            this.statsLayout.Controls.Add(this.lblHealth, 0, 0);
            this.statsLayout.Controls.Add(this.barHealth, 0, 1);
            this.statsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statsLayout.Location = new System.Drawing.Point(153, 3);
            this.statsLayout.Name = "statsLayout";
            this.statsLayout.RowCount = 4;
            this.statsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.statsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.statsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.statsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.statsLayout.Size = new System.Drawing.Size(561, 174);
            this.statsLayout.TabIndex = 1;
            // 
            // barMana
            // 
            this.barMana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barMana.Location = new System.Drawing.Point(3, 123);
            this.barMana.Name = "barMana";
            this.barMana.Size = new System.Drawing.Size(555, 48);
            this.barMana.TabIndex = 3;
            this.barMana.Value = 50;
            // 
            // lblMana
            // 
            this.lblMana.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMana.Location = new System.Drawing.Point(3, 86);
            this.lblMana.Name = "lblMana";
            this.lblMana.Size = new System.Drawing.Size(555, 34);
            this.lblMana.TabIndex = 2;
            this.lblMana.Text = "MP:";
            this.lblMana.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // lblHealth
            // 
            this.lblHealth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHealth.Location = new System.Drawing.Point(3, 0);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(555, 34);
            this.lblHealth.TabIndex = 0;
            this.lblHealth.Text = "HP:";
            this.lblHealth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // barHealth
            // 
            this.barHealth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barHealth.Location = new System.Drawing.Point(3, 37);
            this.barHealth.Name = "barHealth";
            this.barHealth.Size = new System.Drawing.Size(555, 46);
            this.barHealth.TabIndex = 1;
            this.barHealth.Value = 75;
            // 
            // buttonLayout
            // 
            this.buttonLayout.ColumnCount = 1;
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.buttonLayout.Controls.Add(this.btnMenu, 0, 1);
            this.buttonLayout.Controls.Add(this.btnInventory, 0, 0);
            this.buttonLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLayout.Location = new System.Drawing.Point(720, 3);
            this.buttonLayout.Name = "buttonLayout";
            this.buttonLayout.RowCount = 2;
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.buttonLayout.Size = new System.Drawing.Size(144, 174);
            this.buttonLayout.TabIndex = 4;
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMenu.Location = new System.Drawing.Point(3, 90);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(138, 81);
            this.btnMenu.TabIndex = 4;
            this.btnMenu.Text = "Menu";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnInventory.Location = new System.Drawing.Point(3, 3);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(138, 81);
            this.btnInventory.TabIndex = 3;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // RpgBanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainLayout);
            this.Name = "RpgBanner";
            this.Size = new System.Drawing.Size(867, 180);
            this.mainLayout.ResumeLayout(false);
            this.statsLayout.ResumeLayout(false);
            this.buttonLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Button btnCharacter;
        private System.Windows.Forms.TableLayoutPanel statsLayout;
        private System.Windows.Forms.ProgressBar barMana;
        private System.Windows.Forms.Label lblMana;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.ProgressBar barHealth;
        private System.Windows.Forms.TableLayoutPanel buttonLayout;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Button btnInventory;
    }
}
