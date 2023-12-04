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
            label1 = new Label();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(446, 123);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1412, 649);
            Controls.Add(label1);
            Controls.Add(pBoxMain);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pBoxMain).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timerMain;
        private PictureBox pBoxMain;
        private Label label1;
    }
}