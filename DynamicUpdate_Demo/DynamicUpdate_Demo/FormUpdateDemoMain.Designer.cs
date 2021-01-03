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
            this.tabCtr_Main = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnUpdateSample1 = new System.Windows.Forms.Button();
            this.btnUpdate2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabCtr_Main.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnUpdateSample1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabCtr_Main);
            this.splitContainer1.Size = new System.Drawing.Size(901, 573);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabCtr_Main
            // 
            this.tabCtr_Main.Controls.Add(this.tabPage1);
            this.tabCtr_Main.Controls.Add(this.tabPage2);
            this.tabCtr_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtr_Main.Location = new System.Drawing.Point(0, 0);
            this.tabCtr_Main.Name = "tabCtr_Main";
            this.tabCtr_Main.SelectedIndex = 0;
            this.tabCtr_Main.Size = new System.Drawing.Size(597, 573);
            this.tabCtr_Main.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(589, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(276, 139);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnUpdateSample1
            // 
            this.btnUpdateSample1.Location = new System.Drawing.Point(76, 91);
            this.btnUpdateSample1.Name = "btnUpdateSample1";
            this.btnUpdateSample1.Size = new System.Drawing.Size(142, 47);
            this.btnUpdateSample1.TabIndex = 0;
            this.btnUpdateSample1.Text = "Update Tab 1";
            this.btnUpdateSample1.UseVisualStyleBackColor = true;
            this.btnUpdateSample1.Click += new System.EventHandler(this.btnUpdateSample1_Click);
            // 
            // btnUpdate2
            // 
            this.btnUpdate2.Location = new System.Drawing.Point(76, 201);
            this.btnUpdate2.Name = "btnUpdate2";
            this.btnUpdate2.Size = new System.Drawing.Size(142, 47);
            this.btnUpdate2.TabIndex = 0;
            this.btnUpdate2.Text = "Update tab 2";
            this.btnUpdate2.UseVisualStyleBackColor = true;
            // 
            // FormUpdateDemoMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormUpdateDemoMain";
            this.Text = "FormUpdateDemoMain";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabCtr_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnUpdate2;
        private System.Windows.Forms.Button btnUpdateSample1;
        private System.Windows.Forms.TabControl tabCtr_Main;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}