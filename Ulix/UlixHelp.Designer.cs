namespace Ulix
{
    partial class UlixHelp
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
            this.guna2AnimateWindow2 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.CloseAppControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            this.helpPage = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.helpPage)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 20;
            this.label4.Text = "Помощь";
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
            this.CloseAppControlBox.Location = new System.Drawing.Point(754, 5);
            this.CloseAppControlBox.Name = "CloseAppControlBox";
            this.CloseAppControlBox.Padding = new System.Windows.Forms.Padding(2, 0, 2, 1);
            this.CloseAppControlBox.Size = new System.Drawing.Size(33, 31);
            this.CloseAppControlBox.TabIndex = 18;
            this.CloseAppControlBox.Click += new System.EventHandler(this.CloseAppControlBox_Click);
            // 
            // helpPage
            // 
            this.helpPage.AllowExternalDrop = true;
            this.helpPage.CreationProperties = null;
            this.helpPage.DefaultBackgroundColor = System.Drawing.Color.White;
            this.helpPage.Location = new System.Drawing.Point(12, 42);
            this.helpPage.Name = "helpPage";
            this.helpPage.Size = new System.Drawing.Size(768, 435);
            this.helpPage.Source = new System.Uri("https://likuur.github.io/UlixLauncher/OtherShit/UHelp.html", System.UriKind.Absolute);
            this.helpPage.TabIndex = 21;
            this.helpPage.ZoomFactor = 1D;
            // 
            // UlixHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(792, 484);
            this.Controls.Add(this.helpPage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CloseAppControlBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UlixHelp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Помощь";
            this.Load += new System.EventHandler(this.UlixHelp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.helpPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow2;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox CloseAppControlBox;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Microsoft.Web.WebView2.WinForms.WebView2 helpPage;
    }
}