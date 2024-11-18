using System.Windows.Forms;

namespace MunicipalServices
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Handles the cleanup of Windows Forms components and resources
        /// Called when the form is being disposed
        /// Ensures proper disposal of managed and unmanaged resources
        /// </summary>
        /// <param name="disposing">True if managed resources should be disposed</param>
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
            this.btnReportIssues = new System.Windows.Forms.Button();
            this.btnLocalEvents = new System.Windows.Forms.Button();
            this.btnRequestStatus = new System.Windows.Forms.Button();
            this.lblAppName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReportIssues
            // 
            this.btnReportIssues.BackColor = System.Drawing.Color.SteelBlue;
            this.btnReportIssues.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportIssues.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnReportIssues.ForeColor = System.Drawing.Color.White;
            this.btnReportIssues.Location = new System.Drawing.Point(150, 215);
            this.btnReportIssues.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReportIssues.Name = "btnReportIssues";
            this.btnReportIssues.Size = new System.Drawing.Size(375, 88);
            this.btnReportIssues.TabIndex = 0;
            this.btnReportIssues.Text = "Report Issues";
            this.btnReportIssues.UseVisualStyleBackColor = false;
            this.btnReportIssues.Click += new System.EventHandler(this.btnReportIssues_Click);
            // 
            // btnLocalEvents
            // 
            this.btnLocalEvents.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLocalEvents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocalEvents.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLocalEvents.ForeColor = System.Drawing.Color.White;
            this.btnLocalEvents.Location = new System.Drawing.Point(150, 328);
            this.btnLocalEvents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLocalEvents.Name = "btnLocalEvents";
            this.btnLocalEvents.Size = new System.Drawing.Size(375, 94);
            this.btnLocalEvents.TabIndex = 1;
            this.btnLocalEvents.Text = "Local Events and Announcements";
            this.btnLocalEvents.UseVisualStyleBackColor = false;
            this.btnLocalEvents.Click += new System.EventHandler(this.btnLocalEvents_Click);
            // 
            // btnRequestStatus
            // 
            this.btnRequestStatus.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRequestStatus.Enabled = true;
            this.btnRequestStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRequestStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnRequestStatus.ForeColor = System.Drawing.Color.White;
            this.btnRequestStatus.Location = new System.Drawing.Point(150, 448);
            this.btnRequestStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRequestStatus.Name = "btnRequestStatus";
            this.btnRequestStatus.Size = new System.Drawing.Size(375, 88);
            this.btnRequestStatus.TabIndex = 2;
            this.btnRequestStatus.Text = "Request Status";
            this.btnRequestStatus.UseVisualStyleBackColor = false;
            this.btnRequestStatus.Click += new System.EventHandler(this.btnRequestStatus_Click);
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblAppName.Location = new System.Drawing.Point(72, 40);
            this.lblAppName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(515, 60);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "Helping Hands Connect";
            this.lblAppName.Click += new System.EventHandler(this.lblAppName_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Location = new System.Drawing.Point(224, 108);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(228, 98);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.TabIndex = 3;
            this.pictureBoxLogo.TabStop = false;
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = AnchorStyles.None;
            this.mainPanel.BackColor = System.Drawing.Color.LightCyan;
            this.mainPanel.Controls.Add(this.pictureBoxLogo);
            this.mainPanel.Controls.Add(this.lblAppName);
            this.mainPanel.Controls.Add(this.btnRequestStatus);
            this.mainPanel.Controls.Add(this.btnLocalEvents);
            this.mainPanel.Controls.Add(this.btnReportIssues);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(675, 569);
            this.mainPanel.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(675, 569);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Helping Hands Connect";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReportIssues;
        private System.Windows.Forms.Button btnLocalEvents;
        private System.Windows.Forms.Button btnRequestStatus;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Panel mainPanel;
    }
}

