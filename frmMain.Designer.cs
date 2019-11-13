namespace it
{
    /// <summary>
    /// Defines the <see cref="frmMain" />
    /// </summary>
    public partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.InteractiveTimer = new System.Windows.Forms.Timer(this.components);
            this.ClipboardTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            //
            // notifyIcon1
            //
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            //
            // ClipboardTimer
            //
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Defines the notifyIcon1
        /// </summary>
        public System.Windows.Forms.NotifyIcon notifyIcon1;

        /// <summary>
        /// Defines the InteractiveTimer
        /// </summary>
        private System.Windows.Forms.Timer InteractiveTimer;

        /// <summary>
        /// Defines the ClipboardTimer
        /// </summary>
        private System.Windows.Forms.Timer ClipboardTimer;
    }
}
