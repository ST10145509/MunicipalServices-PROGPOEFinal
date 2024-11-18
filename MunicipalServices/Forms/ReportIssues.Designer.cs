using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MunicipalServices
{
    partial class ReportIssues
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
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.lblAttachmentStatus = new System.Windows.Forms.Label();
            this.lblEngagement = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnViewReports = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblEngagementMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(51, 133);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(925, 26);
            this.txtLocation.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(51, 213);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(925, 28);
            this.cmbCategory.TabIndex = 2;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(51, 293);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(925, 239);
            this.txtDescription.TabIndex = 3;
            this.txtDescription.Text = "";
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.Location = new System.Drawing.Point(51, 567);
            this.btnAttachFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(926, 53);
            this.btnAttachFile.TabIndex = 4;
            this.btnAttachFile.Text = "Attach File";
            // 
            // lblAttachmentStatus
            // 
            this.lblAttachmentStatus.Location = new System.Drawing.Point(51, 633);
            this.lblAttachmentStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttachmentStatus.Name = "lblAttachmentStatus";
            this.lblAttachmentStatus.Size = new System.Drawing.Size(926, 33);
            this.lblAttachmentStatus.TabIndex = 5;
            this.lblAttachmentStatus.Text = "No file attached";
            // 
            // lblEngagement
            // 
            this.lblEngagement.Location = new System.Drawing.Point(0, 0);
            this.lblEngagement.Name = "lblEngagement";
            this.lblEngagement.Size = new System.Drawing.Size(100, 23);
            this.lblEngagement.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(51, 773);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(257, 60);
            this.btnSubmit.TabIndex = 8;
            this.btnSubmit.Text = "Submit Report";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(720, 773);
            this.btnBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(257, 60);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back";
            // 
            // btnViewReports
            // 
            this.btnViewReports.Location = new System.Drawing.Point(334, 773);
            this.btnViewReports.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnViewReports.Name = "btnViewReports";
            this.btnViewReports.Size = new System.Drawing.Size(257, 60);
            this.btnViewReports.TabIndex = 9;
            this.btnViewReports.Text = "View Reports";
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(51, 40);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(926, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Report an Issue";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(51, 687);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(926, 11);
            this.progressBar.TabIndex = 6;
            // 
            // lblEngagementMessage
            // 
            this.lblEngagementMessage.Location = new System.Drawing.Point(51, 713);
            this.lblEngagementMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEngagementMessage.Name = "lblEngagementMessage";
            this.lblEngagementMessage.Size = new System.Drawing.Size(926, 33);
            this.lblEngagementMessage.TabIndex = 7;
            this.lblEngagementMessage.Text = "Let\'s get started!";
            // 
            // ReportIssues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1029, 867);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnAttachFile);
            this.Controls.Add(this.lblAttachmentStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblEngagementMessage);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnViewReports);
            this.Controls.Add(this.btnBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ReportIssues";
            this.Padding = new System.Windows.Forms.Padding(51, 53, 51, 53);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Helping Hands Connect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.Label lblAttachmentStatus;
        private System.Windows.Forms.Label lblEngagement;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnViewReports;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblEngagementMessage;
    }
}