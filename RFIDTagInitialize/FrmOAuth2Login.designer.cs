﻿namespace RFIDTagInitialize
{
    partial class FrmOAuth2Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOAuth2Login));
            this.webBrowserOAuth = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserOAuth
            // 
            this.webBrowserOAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserOAuth.Location = new System.Drawing.Point(0, 0);
            this.webBrowserOAuth.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserOAuth.Name = "webBrowserOAuth";
            this.webBrowserOAuth.Size = new System.Drawing.Size(772, 749);
            this.webBrowserOAuth.TabIndex = 0;
            this.webBrowserOAuth.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowseOAuth_Navigated);
            // 
            // FrmOAuth2Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 749);
            this.Controls.Add(this.webBrowserOAuth);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmOAuth2Login";
            this.Text = "SSO 2.0";
            this.Load += new System.EventHandler(this.FrmOAuth2Login_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserOAuth;
    }
}