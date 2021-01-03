namespace UpdateServer
{
    partial class FormUpdateManager
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnShutdownServer = new System.Windows.Forms.Button();
            this.btnUpdateBinding = new System.Windows.Forms.Button();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sessionIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastActiveTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updaterServerViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updaterServerViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button5);
            this.splitContainer2.Panel1.Controls.Add(this.button4);
            this.splitContainer2.Panel1.Controls.Add(this.button6);
            this.splitContainer2.Panel1.Controls.Add(this.btnShutdownServer);
            this.splitContainer2.Panel1.Controls.Add(this.btnUpdateBinding);
            this.splitContainer2.Panel1.Controls.Add(this.btnStartServer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtPrefix);
            this.splitContainer2.Panel2.Controls.Add(this.txtMsg);
            this.splitContainer2.Size = new System.Drawing.Size(800, 206);
            this.splitContainer2.SplitterDistance = 240;
            this.splitContainer2.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(127, 49);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 32);
            this.button5.TabIndex = 2;
            this.button5.Text = "Shutdown";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(26, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 32);
            this.button4.TabIndex = 2;
            this.button4.Text = "Stop 3";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(127, 11);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(84, 32);
            this.button6.TabIndex = 2;
            this.button6.Text = "Start 3";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnShutdownServer
            // 
            this.btnShutdownServer.Location = new System.Drawing.Point(127, 97);
            this.btnShutdownServer.Name = "btnShutdownServer";
            this.btnShutdownServer.Size = new System.Drawing.Size(84, 42);
            this.btnShutdownServer.TabIndex = 1;
            this.btnShutdownServer.Text = "Shutdown Server";
            this.btnShutdownServer.UseVisualStyleBackColor = true;
            this.btnShutdownServer.Click += new System.EventHandler(this.btnShutdownServer_Click);
            // 
            // btnUpdateBinding
            // 
            this.btnUpdateBinding.Location = new System.Drawing.Point(69, 161);
            this.btnUpdateBinding.Name = "btnUpdateBinding";
            this.btnUpdateBinding.Size = new System.Drawing.Size(84, 42);
            this.btnUpdateBinding.TabIndex = 1;
            this.btnUpdateBinding.Text = "Update binding";
            this.btnUpdateBinding.UseVisualStyleBackColor = true;
            this.btnUpdateBinding.Click += new System.EventHandler(this.btnUpdateBinding_Click);
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(26, 97);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(84, 42);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(14, 23);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(411, 20);
            this.txtPrefix.TabIndex = 2;
            this.txtPrefix.Text = "http://localhost:8080/";
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.Location = new System.Drawing.Point(14, 71);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(530, 132);
            this.txtMsg.TabIndex = 1;
            this.txtMsg.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sessionIdDataGridViewTextBoxColumn,
            this.lastActiveTimeDataGridViewTextBoxColumn,
            this.versionDataGridViewTextBoxColumn});
            this.dataGridView1.DataMember = "ActiveClients";
            this.dataGridView1.DataSource = this.updaterServerViewBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 240);
            this.dataGridView1.TabIndex = 0;
            // 
            // sessionIdDataGridViewTextBoxColumn
            // 
            this.sessionIdDataGridViewTextBoxColumn.DataPropertyName = "SessionId";
            this.sessionIdDataGridViewTextBoxColumn.HeaderText = "SessionId";
            this.sessionIdDataGridViewTextBoxColumn.Name = "sessionIdDataGridViewTextBoxColumn";
            // 
            // lastActiveTimeDataGridViewTextBoxColumn
            // 
            this.lastActiveTimeDataGridViewTextBoxColumn.DataPropertyName = "LastActiveTime";
            this.lastActiveTimeDataGridViewTextBoxColumn.HeaderText = "LastActiveTime";
            this.lastActiveTimeDataGridViewTextBoxColumn.Name = "lastActiveTimeDataGridViewTextBoxColumn";
            // 
            // versionDataGridViewTextBoxColumn
            // 
            this.versionDataGridViewTextBoxColumn.DataPropertyName = "Version";
            this.versionDataGridViewTextBoxColumn.HeaderText = "Version";
            this.versionDataGridViewTextBoxColumn.Name = "versionDataGridViewTextBoxColumn";
            // 
            // updaterServerViewBindingSource
            // 
            this.updaterServerViewBindingSource.DataSource = typeof(UpdateServer.UpdaterServerView);
            // 
            // FormUpdateManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormUpdateManager";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormUpdateManager_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updaterServerViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnShutdownServer;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn sessionIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastActiveTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource updaterServerViewBindingSource;
        private System.Windows.Forms.Button btnUpdateBinding;
    }
}

