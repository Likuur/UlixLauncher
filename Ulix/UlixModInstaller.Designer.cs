namespace Ulix
{
    partial class UlixModInstaller
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UlixModInstaller));
            this.label4 = new System.Windows.Forms.Label();
            this.guna2AnimateWindow2 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.CloseAppControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.DownButtonApp = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.installedMods = new System.Windows.Forms.ListBox();
            this.InstallModButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectMod = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(181, 24);
            this.label4.TabIndex = 20;
            this.label4.Text = "Установщик модов";
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 30000;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.AnimationInterval = 320;
            this.guna2BorderlessForm1.BorderRadius = 15;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockForm = false;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2AnimateWindow1
            // 
            this.guna2AnimateWindow1.Interval = 230;
            // 
            // CloseAppControlBox
            // 
            this.CloseAppControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseAppControlBox.Animated = true;
            this.CloseAppControlBox.BorderRadius = 3;
            this.CloseAppControlBox.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.CloseAppControlBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.CloseAppControlBox.CustomClick = true;
            this.CloseAppControlBox.CustomIconSize = 11F;
            this.CloseAppControlBox.FillColor = System.Drawing.Color.DarkGreen;
            this.CloseAppControlBox.HoverState.FillColor = System.Drawing.Color.Green;
            this.CloseAppControlBox.HoverState.IconColor = System.Drawing.Color.Red;
            this.CloseAppControlBox.IconColor = System.Drawing.Color.White;
            this.CloseAppControlBox.Location = new System.Drawing.Point(735, 5);
            this.CloseAppControlBox.Name = "CloseAppControlBox";
            this.CloseAppControlBox.Padding = new System.Windows.Forms.Padding(2, 0, 2, 1);
            this.CloseAppControlBox.Size = new System.Drawing.Size(33, 31);
            this.CloseAppControlBox.TabIndex = 18;
            this.CloseAppControlBox.Click += new System.EventHandler(this.CloseAppControlBox_Click);
            // 
            // DownButtonApp
            // 
            this.DownButtonApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownButtonApp.Animated = true;
            this.DownButtonApp.BorderRadius = 3;
            this.DownButtonApp.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.DownButtonApp.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.DownButtonApp.Cursor = System.Windows.Forms.Cursors.Default;
            this.DownButtonApp.FillColor = System.Drawing.Color.DarkGreen;
            this.DownButtonApp.HoverState.FillColor = System.Drawing.Color.Green;
            this.DownButtonApp.HoverState.IconColor = System.Drawing.Color.Lime;
            this.DownButtonApp.IconColor = System.Drawing.Color.White;
            this.DownButtonApp.Location = new System.Drawing.Point(699, 5);
            this.DownButtonApp.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.DownButtonApp.Name = "DownButtonApp";
            this.DownButtonApp.Padding = new System.Windows.Forms.Padding(2, 7, 2, 1);
            this.DownButtonApp.Size = new System.Drawing.Size(33, 31);
            this.DownButtonApp.TabIndex = 19;
            // 
            // guna2GroupBox2
            // 
            this.guna2GroupBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2GroupBox2.BorderRadius = 6;
            this.guna2GroupBox2.Controls.Add(this.installedMods);
            this.guna2GroupBox2.CustomBorderColor = System.Drawing.Color.DarkGreen;
            this.guna2GroupBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox2.ForeColor = System.Drawing.Color.White;
            this.guna2GroupBox2.Location = new System.Drawing.Point(8, 39);
            this.guna2GroupBox2.Name = "guna2GroupBox2";
            this.guna2GroupBox2.Size = new System.Drawing.Size(227, 378);
            this.guna2GroupBox2.TabIndex = 33;
            this.guna2GroupBox2.Text = "Установлено";
            // 
            // installedMods
            // 
            this.installedMods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.installedMods.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.installedMods.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.installedMods.ForeColor = System.Drawing.Color.White;
            this.installedMods.FormattingEnabled = true;
            this.installedMods.ItemHeight = 25;
            this.installedMods.Location = new System.Drawing.Point(3, 42);
            this.installedMods.Name = "installedMods";
            this.installedMods.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.installedMods.Size = new System.Drawing.Size(221, 325);
            this.installedMods.TabIndex = 0;
            // 
            // InstallModButton
            // 
            this.InstallModButton.Animated = true;
            this.InstallModButton.AnimatedGIF = true;
            this.InstallModButton.BorderRadius = 7;
            this.InstallModButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InstallModButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.InstallModButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.InstallModButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.InstallModButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.InstallModButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.InstallModButton.FillColor = System.Drawing.Color.DarkGreen;
            this.InstallModButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.InstallModButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.InstallModButton.ForeColor = System.Drawing.Color.White;
            this.InstallModButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.InstallModButton.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.InstallModButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.InstallModButton.HoverState.FillColor2 = System.Drawing.Color.DarkGreen;
            this.InstallModButton.Location = new System.Drawing.Point(415, 278);
            this.InstallModButton.Name = "InstallModButton";
            this.InstallModButton.Size = new System.Drawing.Size(209, 75);
            this.InstallModButton.TabIndex = 34;
            this.InstallModButton.Text = "Установить мод";
            this.InstallModButton.Click += new System.EventHandler(this.InstallModButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(468, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Мод не выбран";
            // 
            // SelectMod
            // 
            this.SelectMod.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.SelectMod.HoverState.ImageSize = new System.Drawing.Size(200, 200);
            this.SelectMod.Image = ((System.Drawing.Image)(resources.GetObject("SelectMod.Image")));
            this.SelectMod.ImageOffset = new System.Drawing.Point(0, 0);
            this.SelectMod.ImageRotate = 0F;
            this.SelectMod.ImageSize = new System.Drawing.Size(200, 200);
            this.SelectMod.Location = new System.Drawing.Point(396, 57);
            this.SelectMod.Name = "SelectMod";
            this.SelectMod.PressedState.ImageSize = new System.Drawing.Size(210, 210);
            this.SelectMod.Size = new System.Drawing.Size(228, 191);
            this.SelectMod.TabIndex = 36;
            this.SelectMod.Click += new System.EventHandler(this.SelectMod_Click);
            // 
            // UlixModInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(770, 423);
            this.Controls.Add(this.SelectMod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InstallModButton);
            this.Controls.Add(this.guna2GroupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CloseAppControlBox);
            this.Controls.Add(this.DownButtonApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UlixModInstaller";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UlixModInstaller";
            this.guna2GroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow2;
        private System.Windows.Forms.Timer UpdateTimer;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox CloseAppControlBox;
        private Guna.UI2.WinForms.Guna2ControlBox DownButtonApp;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private System.Windows.Forms.ListBox installedMods;
        private Guna.UI2.WinForms.Guna2GradientButton InstallModButton;
        private Guna.UI2.WinForms.Guna2ImageButton SelectMod;
        private System.Windows.Forms.Label label1;
    }
}