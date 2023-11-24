namespace ITEC225FinalProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerMain = new System.Windows.Forms.Timer(components);
            pBoxMain = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pBoxMain).BeginInit();
            SuspendLayout();
            // 
            // timerMain
            // 
            timerMain.Enabled = true;
            timerMain.Interval = 30;
            timerMain.Tick += timerMain_Tick;
            // 
            // pBoxMain
            // 
            pBoxMain.BackgroundImageLayout = ImageLayout.None;
            pBoxMain.Location = new Point(0, -2);
            pBoxMain.Name = "pBoxMain";
            pBoxMain.Size = new Size(1412, 652);
            pBoxMain.SizeMode = PictureBoxSizeMode.StretchImage;
            pBoxMain.TabIndex = 0;
            pBoxMain.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1412, 649);
            Controls.Add(pBoxMain);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pBoxMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerMain;
        private PictureBox pBoxMain;
    }
}