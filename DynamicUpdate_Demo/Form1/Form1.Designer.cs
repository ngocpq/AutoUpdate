using System.Windows.Forms;

namespace Form1
{
    partial class Form1
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
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.txtUpdateServerUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheckUpdateAuto = new System.Windows.Forms.Button();
            this.timerCheckUpdate = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.trackbar = new System.Windows.Forms.TrackBar();
            this.lableTimeInterval = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(84, 28);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(152, 43);
            this.btnCheckUpdate.TabIndex = 0;
            this.btnCheckUpdate.Text = "Check update manual";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // txtUpdateServerUrl
            // 
            this.txtUpdateServerUrl.Location = new System.Drawing.Point(328, 51);
            this.txtUpdateServerUrl.Name = "txtUpdateServerUrl";
            this.txtUpdateServerUrl.Size = new System.Drawing.Size(221, 20);
            this.txtUpdateServerUrl.TabIndex = 1;
            this.txtUpdateServerUrl.Text = "http://localhost:8080/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Update server URL:";
            // 
            // btnCheckUpdateAuto
            // 
            this.btnCheckUpdateAuto.Location = new System.Drawing.Point(84, 95);
            this.btnCheckUpdateAuto.Name = "btnCheckUpdateAuto";
            this.btnCheckUpdateAuto.Size = new System.Drawing.Size(152, 30);
            this.btnCheckUpdateAuto.TabIndex = 0;
            this.btnCheckUpdateAuto.Text = "Start Auto Check update";
            this.btnCheckUpdateAuto.UseVisualStyleBackColor = true;
            this.btnCheckUpdateAuto.Click += new System.EventHandler(this.btnCheckUpdateAuto_Click);
            // 
            // timerCheckUpdate
            // 
            this.timerCheckUpdate.Interval = 5000;
            this.timerCheckUpdate.Tick += new System.EventHandler(this.timerCheckUpdate_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Interval Time (s): ";
            // 
            // trackbar
            // 
            this.trackbar.LargeChange = 5000;
            this.trackbar.Location = new System.Drawing.Point(328, 108);
            this.trackbar.Maximum = 60000;
            this.trackbar.Minimum = 5000;
            this.trackbar.Name = "trackbar";
            this.trackbar.Size = new System.Drawing.Size(220, 45);
            this.trackbar.TabIndex = 0;
            this.trackbar.TickFrequency = 5;
            this.trackbar.Value = 5000;
            this.trackbar.ValueChanged += new System.EventHandler(this.trackbar_ValueChanged);
            // 
            // lableTimeInterval
            // 
            this.lableTimeInterval.AutoSize = true;
            this.lableTimeInterval.Location = new System.Drawing.Point(420, 79);
            this.lableTimeInterval.Name = "lableTimeInterval";
            this.lableTimeInterval.Size = new System.Drawing.Size(13, 13);
            this.lableTimeInterval.TabIndex = 2;
            this.lableTimeInterval.Text = "5";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 159);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(776, 279);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.trackbar);
            this.Controls.Add(this.lableTimeInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUpdateServerUrl);
            this.Controls.Add(this.btnCheckUpdateAuto);
            this.Controls.Add(this.btnCheckUpdate);
            this.Name = "Form1";
            this.Text = "Form 1: Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        TrackBar trackbar;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.TextBox txtUpdateServerUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheckUpdateAuto;
        private System.Windows.Forms.Timer timerCheckUpdate;
        private System.Windows.Forms.Label label2;
        private Label lableTimeInterval;
        private RichTextBox richTextBox1;
    }
}

