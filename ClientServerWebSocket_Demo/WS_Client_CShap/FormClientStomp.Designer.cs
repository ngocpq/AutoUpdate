
namespace WS_Client_CShap
{
    partial class FormClientStomp
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
            this.label3 = new System.Windows.Forms.Label();
            this.richTxtMsg = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMsgData = new System.Windows.Forms.TextBox();
            this.txtWssAddress = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.btnSendInfo = new System.Windows.Forms.Button();
            this.txtSubscribeDest = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSendDest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHeader = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Log message:";
            // 
            // richTxtMsg
            // 
            this.richTxtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTxtMsg.Location = new System.Drawing.Point(43, 346);
            this.richTxtMsg.Name = "richTxtMsg";
            this.richTxtMsg.Size = new System.Drawing.Size(714, 207);
            this.richTxtMsg.TabIndex = 18;
            this.richTxtMsg.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Send message to server";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "WebSocket Address:";
            // 
            // txtMsgData
            // 
            this.txtMsgData.Location = new System.Drawing.Point(43, 203);
            this.txtMsgData.Multiline = true;
            this.txtMsgData.Name = "txtMsgData";
            this.txtMsgData.Size = new System.Drawing.Size(476, 89);
            this.txtMsgData.TabIndex = 14;
            this.txtMsgData.Text = "{\"type\":\"DEVICE\", \"roomId\":\"80d9ffff-4349-4523-b9a4-d88d54d16c37\", \"message\":\"{\\\"" +
    "admin_id\\\":\\\"CShapClientAdmin\\\",\\\"user_id\\\":\\\"CShapClientUser\\\",\\\"version\\\":\\\"1." +
    "0.0.1\\\"}\"}";
            // 
            // txtWssAddress
            // 
            this.txtWssAddress.Location = new System.Drawing.Point(46, 34);
            this.txtWssAddress.Name = "txtWssAddress";
            this.txtWssAddress.Size = new System.Drawing.Size(476, 20);
            this.txtWssAddress.TabIndex = 15;
            this.txtWssAddress.Text = "ws://erpiaapi.smartb2b.co.kr/socket/ws-stomp/websocket";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(660, 29);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(97, 29);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(528, 256);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(92, 36);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(528, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(92, 29);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(528, 64);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(92, 29);
            this.btnSubscribe.TabIndex = 12;
            this.btnSubscribe.Text = "Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.btnSubscribe_Click);
            // 
            // btnSendInfo
            // 
            this.btnSendInfo.Location = new System.Drawing.Point(681, 138);
            this.btnSendInfo.Name = "btnSendInfo";
            this.btnSendInfo.Size = new System.Drawing.Size(97, 36);
            this.btnSendInfo.TabIndex = 12;
            this.btnSendInfo.Text = "Send Client Info";
            this.btnSendInfo.UseVisualStyleBackColor = true;
            this.btnSendInfo.Click += new System.EventHandler(this.btnSendInfo_Click);
            // 
            // txtSubscribeDest
            // 
            this.txtSubscribeDest.Location = new System.Drawing.Point(132, 64);
            this.txtSubscribeDest.Name = "txtSubscribeDest";
            this.txtSubscribeDest.Size = new System.Drawing.Size(387, 20);
            this.txtSubscribeDest.TabIndex = 15;
            this.txtSubscribeDest.Text = "/sub/chat/room/80d9ffff-4349-4523-b9a4-d88d54d16c37-admin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Sub. destination";
            // 
            // txtSendDest
            // 
            this.txtSendDest.Location = new System.Drawing.Point(132, 94);
            this.txtSendDest.Name = "txtSendDest";
            this.txtSendDest.Size = new System.Drawing.Size(387, 20);
            this.txtSendDest.TabIndex = 15;
            this.txtSendDest.Text = "/pub/chat/message";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Send destitation";
            // 
            // txtHeader
            // 
            this.txtHeader.Location = new System.Drawing.Point(43, 138);
            this.txtHeader.Multiline = true;
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(476, 48);
            this.txtHeader.TabIndex = 14;
            this.txtHeader.Text = "token:1234\r\ncontent-type:application/json";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Headers: <Name>: <Value>";
            // 
            // FormClientStomp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 570);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTxtMsg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHeader);
            this.Controls.Add(this.txtMsgData);
            this.Controls.Add(this.txtSendDest);
            this.Controls.Add(this.txtSubscribeDest);
            this.Controls.Add(this.txtWssAddress);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnSendInfo);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Name = "FormClientStomp";
            this.Text = "FormClientStomp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTxtMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMsgData;
        private System.Windows.Forms.TextBox txtWssAddress;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button btnSendInfo;
        private System.Windows.Forms.TextBox txtSubscribeDest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSendDest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHeader;
        private System.Windows.Forms.Label label6;
    }
}