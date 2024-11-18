using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.Data;
using MunicipalServices.Utils;
using System.Drawing.Drawing2D;

namespace MunicipalServices
{
    public partial class ViewReports : Form
    {
        private readonly IssueRepository _issueRepository;
        private string currentAttachmentPath;

        public ViewReports(IssueRepository issueRepository)
        {
            InitializeComponent();
            _issueRepository = issueRepository;
            InitializeFormStyle();
            SetupInitialState();
            LoadReports();
            this.FormClosing += ViewReports_FormClosing;
            btnOpenDocument.Click += btnOpenDocument_Click;
        }

        private void InitializeFormStyle()
        {
            // Form styling
            this.BackColor = ThemeColors.Background;
            
            // Style the title
            lblTitle.ForeColor = ThemeColors.Primary;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F);
            
            // Style the list box
            lstReports.BackColor = ThemeColors.CardBackground;
            lstReports.Font = new Font("Segoe UI", 11F);
            lstReports.BorderStyle = BorderStyle.FixedSingle;
            lstReports.ForeColor = ThemeColors.TextPrimary;
            
            // Style description box
            txtDescription.BackColor = ThemeColors.CardBackground;
            txtDescription.BorderStyle = BorderStyle.FixedSingle;
            
            // Style picture box
            pictureBox.BackColor = ThemeColors.CardBackground;
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            
            // Style labels
            StyleLabel(lblLocation);
            StyleLabel(lblCategory);
            StyleLabel(lblAttachment);
            
            // Style button
            StyleButton(btnOpenDocument);
            
            // Connect the event handler
            lstReports.SelectedIndexChanged += lstReports_SelectedIndexChanged;
        }

        private void StyleButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.BackColor = ThemeColors.Primary;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI Semibold", 11F);
            button.Cursor = Cursors.Hand;
            
            GraphicsPath path = new GraphicsPath();
            path.AddRoundedRectangle(button.ClientRectangle, 5);
            button.Region = new Region(path);
        }

        private void StyleLabel(Label label)
        {
            label.Font = new Font("Segoe UI", 11F);
            label.ForeColor = ThemeColors.TextPrimary;
        }

        private void SetupInitialState()
        {
            // Set initial placeholder texts
            lblLocation.Text = "Location: No issue selected";
            lblCategory.Text = "Category: No issue selected";
            txtDescription.Text = "Select an issue from the list to view details...";
            lblAttachment.Text = "Attachment: None";
            
            // Style the placeholder text
            txtDescription.ForeColor = ThemeColors.TextSecondary;
            lblLocation.ForeColor = ThemeColors.TextSecondary;
            lblCategory.ForeColor = ThemeColors.TextSecondary;
            lblAttachment.ForeColor = ThemeColors.TextSecondary;
        }

        private void LoadReports()
        {
            lstReports.Items.Clear(); // Clear existing items
            var issues = _issueRepository.GetAllIssues();
            
            if (issues.Count == 0)
            {
                lstReports.Items.Add("No reports available");
                lstReports.Enabled = false;
            }
            else
            {
                foreach (var issue in issues)
                {
                    lstReports.Items.Add($"{issue.Location} - {issue.Category}");
                }
            }
        }

        private void lstReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = lstReports.SelectedIndex;
            var issues = _issueRepository.GetAllIssues();
            
            if (selectedIndex >= 0 && selectedIndex < issues.Count)
            {
                Issue selectedIssue = issues[selectedIndex];
                DisplayIssueDetails(selectedIssue);
            }
            else
            {
                SetupInitialState();
            }
        }

        private void DisplayIssueDetails(Issue issue)
        {
            if (issue == null) return;

            // Reset text colors to primary
            lblLocation.ForeColor = ThemeColors.TextPrimary;
            lblCategory.ForeColor = ThemeColors.TextPrimary;
            txtDescription.ForeColor = ThemeColors.TextPrimary;
            lblAttachment.ForeColor = ThemeColors.TextPrimary;

            // Update the display fields
            lblLocation.Text = $"Location: {issue.Location}";
            lblCategory.Text = $"Category: {issue.Category}";
            txtDescription.Text = issue.Description;

            if (issue.AttachmentData != null && !string.IsNullOrEmpty(issue.AttachmentName))
            {
                string fileExtension = Path.GetExtension(issue.AttachmentName).ToLower();
                lblAttachment.Text = $"Attachment: {issue.AttachmentName}";

                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    try
                    {
                        using (var ms = new MemoryStream(issue.AttachmentData))
                        {
                            if (pictureBox.Image != null)
                            {
                                pictureBox.Image.Dispose();
                            }
                            pictureBox.Image = Image.FromStream(ms);
                            pictureBox.Visible = true;
                            btnOpenDocument.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        pictureBox.Visible = false;
                        lblAttachment.Text = "Error loading image";
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    pictureBox.Visible = false;
                    btnOpenDocument.Visible = true;
                }
            }
            else
            {
                pictureBox.Visible = false;
                btnOpenDocument.Visible = false;
                lblAttachment.Text = "No attachment";
            }
        }

        private void btnOpenDocument_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstReports.SelectedIndex;
            var issues = _issueRepository.GetAllIssues();
            
            if (selectedIndex >= 0 && selectedIndex < issues.Count)
            {
                Issue selectedIssue = issues[selectedIndex];
                OpenAttachment(selectedIssue);
            }
        }

        private void OpenAttachment(Issue issue)
        {
            if (issue.AttachmentData == null || string.IsNullOrEmpty(issue.AttachmentName))
            {
                MessageBox.Show("No attachment available.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), issue.AttachmentName);
                File.WriteAllBytes(tempPath, issue.AttachmentData);
                Process.Start(tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open attachment: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;  // Prevent actual form closure
                this.Hide();      // Just hide the form
            }
        }
    }
}
