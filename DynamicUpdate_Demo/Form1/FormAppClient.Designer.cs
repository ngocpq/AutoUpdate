﻿using System.Windows.Forms;

namespace Form1
{
    partial class FormAppClient
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
            this.txtUpdateServerUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetServerCommands = new System.Windows.Forms.Button();
            this.timerCheckUpdate = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.trackbar = new System.Windows.Forms.TrackBar();
            this.lableTimeInterval = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLatestVersion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblLastCheckedTime = new System.Windows.Forms.Label();
            this.btnCheckUpdateAuto = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUpdateServerUrl
            // 
            this.txtUpdateServerUrl.Location = new System.Drawing.Point(238, 78);
            this.txtUpdateServerUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUpdateServerUrl.Name = "txtUpdateServerUrl";
            this.txtUpdateServerUrl.Size = new System.Drawing.Size(330, 26);
            this.txtUpdateServerUrl.TabIndex = 1;
            this.txtUpdateServerUrl.Text = "http://192.168.1.8:8080";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Update server URL:";
            // 
            // btnGetServerCommands
            // 
            this.btnGetServerCommands.Location = new System.Drawing.Point(50, 146);
            this.btnGetServerCommands.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetServerCommands.Name = "btnGetServerCommands";
            this.btnGetServerCommands.Size = new System.Drawing.Size(135, 66);
            this.btnGetServerCommands.TabIndex = 0;
            this.btnGetServerCommands.Text = "Start Get Commands";
            this.btnGetServerCommands.UseVisualStyleBackColor = true;
            this.btnGetServerCommands.Click += new System.EventHandler(this.btnGetServerCommands_Click);
            // 
            // timerCheckUpdate
            // 
            this.timerCheckUpdate.Interval = 5000;
            this.timerCheckUpdate.Tick += new System.EventHandler(this.timerCheckUpdate_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Interval Time (s): ";
            // 
            // trackbar
            // 
            this.trackbar.LargeChange = 5000;
            this.trackbar.Location = new System.Drawing.Point(238, 166);
            this.trackbar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackbar.Maximum = 60000;
            this.trackbar.Minimum = 5000;
            this.trackbar.Name = "trackbar";
            this.trackbar.Size = new System.Drawing.Size(330, 69);
            this.trackbar.TabIndex = 0;
            this.trackbar.TickFrequency = 5;
            this.trackbar.Value = 5000;
            this.trackbar.ValueChanged += new System.EventHandler(this.trackbar_ValueChanged);
            // 
            // lableTimeInterval
            // 
            this.lableTimeInterval.AutoSize = true;
            this.lableTimeInterval.Location = new System.Drawing.Point(398, 146);
            this.lableTimeInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lableTimeInterval.Name = "lableTimeInterval";
            this.lableTimeInterval.Size = new System.Drawing.Size(18, 20);
            this.lableTimeInterval.TabIndex = 2;
            this.lableTimeInterval.Text = "5";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(18, 245);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1402, 427);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(609, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Current Version:";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(742, 63);
            this.lblCurrentVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(63, 20);
            this.lblCurrentVersion.TabIndex = 2;
            this.lblCurrentVersion.Text = "Version";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(609, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Latest version:";
            // 
            // lblLatestVersion
            // 
            this.lblLatestVersion.AutoSize = true;
            this.lblLatestVersion.Location = new System.Drawing.Point(742, 126);
            this.lblLatestVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLatestVersion.Name = "lblLatestVersion";
            this.lblLatestVersion.Size = new System.Drawing.Size(108, 20);
            this.lblLatestVersion.TabIndex = 2;
            this.lblLatestVersion.Text = "Latest version";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(609, 192);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Last checked:";
            // 
            // lblLastCheckedTime
            // 
            this.lblLastCheckedTime.AutoSize = true;
            this.lblLastCheckedTime.Location = new System.Drawing.Point(742, 192);
            this.lblLastCheckedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastCheckedTime.Name = "lblLastCheckedTime";
            this.lblLastCheckedTime.Size = new System.Drawing.Size(106, 20);
            this.lblLastCheckedTime.TabIndex = 2;
            this.lblLastCheckedTime.Text = "Checked time";
            // 
            // btnCheckUpdateAuto
            // 
            this.btnCheckUpdateAuto.Location = new System.Drawing.Point(50, 43);
            this.btnCheckUpdateAuto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCheckUpdateAuto.Name = "btnCheckUpdateAuto";
            this.btnCheckUpdateAuto.Size = new System.Drawing.Size(135, 66);
            this.btnCheckUpdateAuto.TabIndex = 0;
            this.btnCheckUpdateAuto.Text = "Check update";
            this.btnCheckUpdateAuto.UseVisualStyleBackColor = true;
            this.btnCheckUpdateAuto.Click += new System.EventHandler(this.btnCheckUpdateAuto_Click);
            // 
            // FormAppClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 692);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.trackbar);
            this.Controls.Add(this.lableTimeInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLastCheckedTime);
            this.Controls.Add(this.lblLatestVersion);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUpdateServerUrl);
            this.Controls.Add(this.btnGetServerCommands);
            this.Controls.Add(this.btnCheckUpdateAuto);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormAppClient";
            this.Text = "Form 1: Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        TrackBar trackbar;
        private System.Windows.Forms.TextBox txtUpdateServerUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetServerCommands;
        private System.Windows.Forms.Timer timerCheckUpdate;
        private System.Windows.Forms.Label label2;
        private Label lableTimeInterval;
        private RichTextBox richTextBox1;
        private Label label3;
        private Label lblCurrentVersion;
        private Label label4;
        private Label lblLatestVersion;
        private Label label6;
        private Label lblLastCheckedTime;
        private Button btnCheckUpdateAuto;
    }
}

