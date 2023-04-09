namespace BattleShip
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
            output = new RichTextBox();
            SuspendLayout();
            // 
            // output
            // 
            output.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            output.DetectUrls = false;
            output.Location = new Point(852, 529);
            output.Name = "output";
            output.ReadOnly = true;
            output.Size = new Size(180, 96);
            output.TabIndex = 0;
            output.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.water;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1044, 637);
            Controls.Add(output);
            Name = "Form1";
            Text = "BattleShip";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox output;
    }
}