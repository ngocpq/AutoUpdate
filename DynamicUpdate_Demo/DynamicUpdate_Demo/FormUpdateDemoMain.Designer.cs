namespace DynamicUpdate_Demo
{
    partial class FormUpdateDemoMain
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnUpdate2 = new System.Windows.Forms.Button();
            this.btnUpdateSample1 = new System.Windows.Forms.Button();
            this.tabCtr_Main = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainerMainBody = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.txtCurrentAsmFilePath = new System.Windows.Forms.TextBox();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.txtNewAsmFile = new System.Windows.Forms.TextBox();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabCtr_Main.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainBody)).BeginInit();
            this.splitContainerMainBody.Panel1.SuspendLayout();
            this.splitContainerMainBody.Panel2.SuspendLayout();
            this.splitContainerMainBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdate2);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdateSample1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabCtr_Main);
            this.splitContainer1.Size = new System.Drawing.Size(952, 573);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnUpdate2
            // 
            this.btnUpdate2.Location = new System.Drawing.Point(41, 46);
            this.btnUpdate2.Name = "btnUpdate2";
            this.btnUpdate2.Size = new System.Drawing.Size(142, 47);
            this.btnUpdate2.TabIndex = 0;
            this.btnUpdate2.Text = "Load Tab1";
            this.btnUpdate2.UseVisualStyleBackColor = true;
            this.btnUpdate2.Click += new System.EventHandler(this.btnUpdate2_Click);
            // 
            // btnUpdateSample1
            // 
            this.btnUpdateSample1.Location = new System.Drawing.Point(41, 169);
            this.btnUpdateSample1.Name = "btnUpdateSample1";
            this.btnUpdateSample1.Size = new System.Drawing.Size(142, 47);
            this.btnUpdateSample1.TabIndex = 0;
            this.btnUpdateSample1.Text = "Update DLL";
            this.btnUpdateSample1.UseVisualStyleBackColor = true;
            this.btnUpdateSample1.Click += new System.EventHandler(this.btnUpdateSample1_Click);
            // 
            // tabCtr_Main
            // 
            this.tabCtr_Main.Controls.Add(this.tabPage1);
            this.tabCtr_Main.Controls.Add(this.tabPage2);
            this.tabCtr_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtr_Main.Location = new System.Drawing.Point(0, 0);
            this.tabCtr_Main.Name = "tabCtr_Main";
            this.tabCtr_Main.SelectedIndex = 0;
            this.tabCtr_Main.Size = new System.Drawing.Size(740, 573);
            this.tabCtr_Main.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainerMainBody);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainerMainBody
            // 
            this.splitContainerMainBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainBody.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMainBody.Name = "splitContainerMainBody";
            this.splitContainerMainBody.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMainBody.Panel1
            // 
            this.splitContainerMainBody.Panel1.Controls.Add(this.button1);
            this.splitContainerMainBody.Panel1.Controls.Add(this.txtClassName);
            this.splitContainerMainBody.Panel1.Controls.Add(this.txtCurrentAsmFilePath);
            this.splitContainerMainBody.Panel1.Controls.Add(this.lblCurrentVersion);
            this.splitContainerMainBody.Panel1.Controls.Add(this.txtNewAsmFile);
            this.splitContainerMainBody.Panel1.Controls.Add(this.lblNewVersion);
            this.splitContainerMainBody.Panel1.Controls.Add(this.label5);
            this.splitContainerMainBody.Panel1.Controls.Add(this.label3);
            this.splitContainerMainBody.Panel1.Controls.Add(this.label4);
            this.splitContainerMainBody.Panel1.Controls.Add(this.label2);
            this.splitContainerMainBody.Panel1.Controls.Add(this.label1);
            // 
            // splitContainerMainBody.Panel2
            // 
            this.splitContainerMainBody.Panel2.Controls.Add(this.panel1);
            this.splitContainerMainBody.Size = new System.Drawing.Size(726, 541);
            this.splitContainerMainBody.SplitterDistance = 168;
            this.splitContainerMainBody.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(656, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtClassName
            // 
            this.txtClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClassName.Location = new System.Drawing.Point(134, 141);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(252, 20);
            this.txtClassName.TabIndex = 1;
            // 
            // txtCurrentAsmFilePath
            // 
            this.txtCurrentAsmFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentAsmFilePath.Location = new System.Drawing.Point(134, 88);
            this.txtCurrentAsmFilePath.Name = "txtCurrentAsmFilePath";
            this.txtCurrentAsmFilePath.Size = new System.Drawing.Size(516, 20);
            this.txtCurrentAsmFilePath.TabIndex = 1;
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentVersion.Location = new System.Drawing.Point(131, 120);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(53, 13);
            this.lblCurrentVersion.TabIndex = 0;
            this.lblCurrentVersion.Text = "Version:";
            // 
            // txtNewAsmFile
            // 
            this.txtNewAsmFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewAsmFile.Location = new System.Drawing.Point(134, 23);
            this.txtNewAsmFile.Name = "txtNewAsmFile";
            this.txtNewAsmFile.Size = new System.Drawing.Size(516, 20);
            this.txtNewAsmFile.TabIndex = 1;
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewVersion.Location = new System.Drawing.Point(131, 55);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(53, 13);
            this.lblNewVersion.TabIndex = 0;
            this.lblNewVersion.Text = "Version:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Version:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Form\'s class name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Assembly File (DLL):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Assembly File (DLL):";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 369);
            this.panel1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(732, 547);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 249);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 47);
            this.button2.TabIndex = 0;
            this.button2.Text = "Unload Dll";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormUpdateDemoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormUpdateDemoMain";
            this.Text = "FormUpdateDemoMain";
            this.Load += new System.EventHandler(this.FormUpdateDemoMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabCtr_Main.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainerMainBody.Panel1.ResumeLayout(false);
            this.splitContainerMainBody.Panel1.PerformLayout();
            this.splitContainerMainBody.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainBody)).EndInit();
            this.splitContainerMainBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnUpdate2;
        private System.Windows.Forms.Button btnUpdateSample1;
        private System.Windows.Forms.TabControl tabCtr_Main;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainerMainBody;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.TextBox txtNewAsmFile;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCurrentAsmFilePath;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
    }
}