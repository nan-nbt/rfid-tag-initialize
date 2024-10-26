namespace RFIDTagInitialize
{
    partial class FrmTagInitial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTagInitial));
            this.cbxTagType = new System.Windows.Forms.ComboBox();
            this.cbxComPort = new System.Windows.Forms.ComboBox();
            this.btnClearProgMsg = new System.Windows.Forms.Button();
            this.rtbProgMsg = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCount = new System.Windows.Forms.Label();
            this.txtAutoEPC = new System.Windows.Forms.TextBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClearTagLog = new System.Windows.Forms.Button();
            this.txtWrite = new System.Windows.Forms.TextBox();
            this.btnUnmask = new System.Windows.Forms.Button();
            this.btnMask = new System.Windows.Forms.Button();
            this.lbxTag = new System.Windows.Forms.ListBox();
            this.txtManualEPC = new System.Windows.Forms.TextBox();
            this.btnWriteTag = new System.Windows.Forms.Button();
            this.btnReadTag = new System.Windows.Forms.Button();
            this.imgReaderStatus = new System.Windows.Forms.PictureBox();
            this.imgComPort = new System.Windows.Forms.PictureBox();
            this.cbxSetPower = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgReaderStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgComPort)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxTagType
            // 
            this.cbxTagType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTagType.FormattingEnabled = true;
            this.cbxTagType.Items.AddRange(new object[] {
            "Alien",
            "AlienUHF",
            "NPX"});
            this.cbxTagType.Location = new System.Drawing.Point(444, 14);
            this.cbxTagType.Name = "cbxTagType";
            this.cbxTagType.Size = new System.Drawing.Size(121, 21);
            this.cbxTagType.TabIndex = 1;
            // 
            // cbxComPort
            // 
            this.cbxComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxComPort.FormattingEnabled = true;
            this.cbxComPort.Location = new System.Drawing.Point(444, 43);
            this.cbxComPort.Name = "cbxComPort";
            this.cbxComPort.Size = new System.Drawing.Size(121, 21);
            this.cbxComPort.TabIndex = 2;
            // 
            // btnClearProgMsg
            // 
            this.btnClearProgMsg.Location = new System.Drawing.Point(577, 14);
            this.btnClearProgMsg.Name = "btnClearProgMsg";
            this.btnClearProgMsg.Size = new System.Drawing.Size(75, 50);
            this.btnClearProgMsg.TabIndex = 3;
            this.btnClearProgMsg.Text = "Clear Log";
            this.btnClearProgMsg.UseVisualStyleBackColor = true;
            this.btnClearProgMsg.Click += new System.EventHandler(this.btnClearProgMsg_Click);
            // 
            // rtbProgMsg
            // 
            this.rtbProgMsg.BackColor = System.Drawing.SystemColors.Info;
            this.rtbProgMsg.Location = new System.Drawing.Point(16, 512);
            this.rtbProgMsg.Name = "rtbProgMsg";
            this.rtbProgMsg.Size = new System.Drawing.Size(632, 137);
            this.rtbProgMsg.TabIndex = 4;
            this.rtbProgMsg.Text = "";
            this.rtbProgMsg.TextChanged += new System.EventHandler(this.rtbProgMsg_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(16, 78);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(636, 428);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCount);
            this.tabPage1.Controls.Add(this.txtAutoEPC);
            this.tabPage1.Controls.Add(this.btnRestart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(628, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Automatic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            this.lblCount.BackColor = System.Drawing.Color.LightGray;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F);
            this.lblCount.Location = new System.Drawing.Point(6, 40);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(616, 355);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "0";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAutoEPC
            // 
            this.txtAutoEPC.Location = new System.Drawing.Point(6, 9);
            this.txtAutoEPC.Name = "txtAutoEPC";
            this.txtAutoEPC.ReadOnly = true;
            this.txtAutoEPC.Size = new System.Drawing.Size(535, 20);
            this.txtAutoEPC.TabIndex = 1;
            // 
            // btnRestart
            // 
            this.btnRestart.Enabled = false;
            this.btnRestart.Location = new System.Drawing.Point(547, 7);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 0;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnClearTagLog);
            this.tabPage2.Controls.Add(this.txtWrite);
            this.tabPage2.Controls.Add(this.btnUnmask);
            this.tabPage2.Controls.Add(this.btnMask);
            this.tabPage2.Controls.Add(this.lbxTag);
            this.tabPage2.Controls.Add(this.txtManualEPC);
            this.tabPage2.Controls.Add(this.btnWriteTag);
            this.tabPage2.Controls.Add(this.btnReadTag);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(628, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manual";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClearTagLog
            // 
            this.btnClearTagLog.Enabled = false;
            this.btnClearTagLog.Location = new System.Drawing.Point(6, 32);
            this.btnClearTagLog.Name = "btnClearTagLog";
            this.btnClearTagLog.Size = new System.Drawing.Size(200, 23);
            this.btnClearTagLog.TabIndex = 7;
            this.btnClearTagLog.Text = "Clear Tag Log";
            this.btnClearTagLog.UseVisualStyleBackColor = true;
            this.btnClearTagLog.Click += new System.EventHandler(this.btnClearTagLog_Click);
            // 
            // txtWrite
            // 
            this.txtWrite.Enabled = false;
            this.txtWrite.Location = new System.Drawing.Point(423, 6);
            this.txtWrite.Name = "txtWrite";
            this.txtWrite.Size = new System.Drawing.Size(200, 20);
            this.txtWrite.TabIndex = 6;
            // 
            // btnUnmask
            // 
            this.btnUnmask.Enabled = false;
            this.btnUnmask.Location = new System.Drawing.Point(317, 32);
            this.btnUnmask.Name = "btnUnmask";
            this.btnUnmask.Size = new System.Drawing.Size(95, 23);
            this.btnUnmask.TabIndex = 5;
            this.btnUnmask.Text = "Unmask Tag";
            this.btnUnmask.UseVisualStyleBackColor = true;
            this.btnUnmask.Click += new System.EventHandler(this.btnUnmask_Click);
            // 
            // btnMask
            // 
            this.btnMask.Enabled = false;
            this.btnMask.Location = new System.Drawing.Point(212, 32);
            this.btnMask.Name = "btnMask";
            this.btnMask.Size = new System.Drawing.Size(95, 23);
            this.btnMask.TabIndex = 4;
            this.btnMask.Text = "Mask Tag";
            this.btnMask.UseVisualStyleBackColor = true;
            this.btnMask.Click += new System.EventHandler(this.btnMask_Click);
            // 
            // lbxTag
            // 
            this.lbxTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxTag.FormattingEnabled = true;
            this.lbxTag.ItemHeight = 33;
            this.lbxTag.Location = new System.Drawing.Point(6, 65);
            this.lbxTag.Name = "lbxTag";
            this.lbxTag.Size = new System.Drawing.Size(616, 301);
            this.lbxTag.TabIndex = 3;
            // 
            // txtManualEPC
            // 
            this.txtManualEPC.Enabled = false;
            this.txtManualEPC.Location = new System.Drawing.Point(212, 6);
            this.txtManualEPC.Name = "txtManualEPC";
            this.txtManualEPC.Size = new System.Drawing.Size(200, 20);
            this.txtManualEPC.TabIndex = 2;
            // 
            // btnWriteTag
            // 
            this.btnWriteTag.Enabled = false;
            this.btnWriteTag.Location = new System.Drawing.Point(423, 32);
            this.btnWriteTag.Name = "btnWriteTag";
            this.btnWriteTag.Size = new System.Drawing.Size(199, 23);
            this.btnWriteTag.TabIndex = 1;
            this.btnWriteTag.Text = "Write Tag";
            this.btnWriteTag.UseVisualStyleBackColor = true;
            this.btnWriteTag.Click += new System.EventHandler(this.btnWriteTag_Click);
            // 
            // btnReadTag
            // 
            this.btnReadTag.Enabled = false;
            this.btnReadTag.Location = new System.Drawing.Point(6, 6);
            this.btnReadTag.Name = "btnReadTag";
            this.btnReadTag.Size = new System.Drawing.Size(200, 23);
            this.btnReadTag.TabIndex = 0;
            this.btnReadTag.Text = "Start Read Tag";
            this.btnReadTag.UseVisualStyleBackColor = true;
            this.btnReadTag.Click += new System.EventHandler(this.btnReadTag_Click);
            // 
            // imgReaderStatus
            // 
            this.imgReaderStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgReaderStatus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgReaderStatus.Image = global::RFIDTagInitialize.Properties.Resources.disconnect;
            this.imgReaderStatus.ImageLocation = "";
            this.imgReaderStatus.Location = new System.Drawing.Point(16, 14);
            this.imgReaderStatus.Name = "imgReaderStatus";
            this.imgReaderStatus.Size = new System.Drawing.Size(50, 50);
            this.imgReaderStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgReaderStatus.TabIndex = 6;
            this.imgReaderStatus.TabStop = false;
            this.imgReaderStatus.Tag = "disconnected";
            this.imgReaderStatus.Click += new System.EventHandler(this.imgReaderStatus_Click);
            // 
            // imgComPort
            // 
            this.imgComPort.Image = global::RFIDTagInitialize.Properties.Resources.ComPort;
            this.imgComPort.Location = new System.Drawing.Point(388, 14);
            this.imgComPort.Name = "imgComPort";
            this.imgComPort.Size = new System.Drawing.Size(50, 50);
            this.imgComPort.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgComPort.TabIndex = 7;
            this.imgComPort.TabStop = false;
            // 
            // cbxSetPower
            // 
            this.cbxSetPower.DropDownHeight = 70;
            this.cbxSetPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSetPower.FormattingEnabled = true;
            this.cbxSetPower.IntegralHeight = false;
            this.cbxSetPower.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.cbxSetPower.Location = new System.Drawing.Point(332, 14);
            this.cbxSetPower.MaxDropDownItems = 5;
            this.cbxSetPower.Name = "cbxSetPower";
            this.cbxSetPower.Size = new System.Drawing.Size(50, 21);
            this.cbxSetPower.TabIndex = 8;
            this.cbxSetPower.SelectedIndexChanged += new System.EventHandler(this.cbxSetPower_SelectedIndexChanged);
            // 
            // FrmTagInitial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 661);
            this.Controls.Add(this.cbxSetPower);
            this.Controls.Add(this.imgComPort);
            this.Controls.Add(this.imgReaderStatus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.rtbProgMsg);
            this.Controls.Add(this.btnClearProgMsg);
            this.Controls.Add(this.cbxComPort);
            this.Controls.Add(this.cbxTagType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmTagInitial";
            this.Tag = "";
            this.Text = "Mass Batch Initialize Chip";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTagInitialize_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgReaderStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgComPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxTagType;
        private System.Windows.Forms.ComboBox cbxComPort;
        private System.Windows.Forms.Button btnClearProgMsg;
        private System.Windows.Forms.RichTextBox rtbProgMsg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TextBox txtAutoEPC;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.TextBox txtManualEPC;
        private System.Windows.Forms.Button btnWriteTag;
        private System.Windows.Forms.Button btnReadTag;
        private System.Windows.Forms.ListBox lbxTag;
        private System.Windows.Forms.PictureBox imgReaderStatus;
        private System.Windows.Forms.PictureBox imgComPort;
        private System.Windows.Forms.ComboBox cbxSetPower;
        private System.Windows.Forms.Button btnUnmask;
        private System.Windows.Forms.Button btnMask;
        private System.Windows.Forms.TextBox txtWrite;
        private System.Windows.Forms.Button btnClearTagLog;
    }
}