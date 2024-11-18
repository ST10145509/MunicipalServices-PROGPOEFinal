using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MunicipalServices.Utils;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.Data;
using System.IO;

namespace MunicipalServices
{
    public partial class ReportIssues : Form
    {
        private readonly IssueRepository _issueRepository;
        private byte[] attachmentData;
        private string attachmentName;
        private string attachmentType;
        private readonly int progressStep = 25;

        public ReportIssues()
        {
            InitializeComponent();
            InitializeFormStyle();
            SetupEventHandlers();
            _issueRepository = new IssueRepository();
        }

        private void InitializeFormStyle()
        {
            // Apply theme colors and styling
            this.BackColor = ThemeColors.Background;
            
            // Style the title
            lblTitle.ForeColor = ThemeColors.Primary;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F);
            
            // Populate and style category dropdown
            cmbCategory.Items.AddRange(new object[] {
                "Infrastructure",
                "Public Safety",
                "Environmental",
                "Utilities",
                "Transportation",
                "Parks and Recreation",
                "Public Health",
                "Other"
            });
            cmbCategory.SelectedIndex = -1; // No default selection
            StyleComboBox(cmbCategory);
            
            // Style input fields
            StyleTextBox(txtLocation, "Enter location");
            StyleRichTextBox(txtDescription, "Enter description of the issue here...");
            
            // Style buttons
            StyleButton(btnSubmit, ThemeColors.Primary);
            StyleButton(btnViewReports, ThemeColors.Secondary);
            StyleButton(btnAttachFile, ThemeColors.TextSecondary);
            StyleButton(btnBack, ThemeColors.TextSecondary);
            
            // Style progress bar
            StyleProgressBar();
            
            // Style labels
            StyleLabels();
        }

        private void StyleTextBox(TextBox textBox, string placeholder)
        {
            textBox.Font = new Font("Segoe UI", 11F);
            textBox.BackColor = ThemeColors.CardBackground;
            textBox.ForeColor = ThemeColors.TextSecondary;
            textBox.Text = placeholder;
            textBox.BorderStyle = BorderStyle.None;
            
            Panel underline = new Panel
            {
                Height = 2,
                Dock = DockStyle.Bottom,
                BackColor = ThemeColors.Border
            };
            textBox.Controls.Add(underline);
        }

        private void StyleComboBox(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Font = new Font("Segoe UI", 11F);
            comboBox.BackColor = ThemeColors.CardBackground;
            comboBox.ForeColor = ThemeColors.TextPrimary;
        }

        private void StyleRichTextBox(RichTextBox richTextBox, string placeholder)
        {
            richTextBox.Font = new Font("Segoe UI", 11F);
            richTextBox.BackColor = ThemeColors.CardBackground;
            richTextBox.ForeColor = ThemeColors.TextSecondary;
            richTextBox.Text = placeholder;
            richTextBox.BorderStyle = BorderStyle.None;
        }

        private void StyleButton(Button button, Color color)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI Semibold", 11F);
            button.Cursor = Cursors.Hand;
            
            // Add rounded corners
            GraphicsPath path = new GraphicsPath();
            path.AddRoundedRectangle(button.ClientRectangle, 5);
            button.Region = new Region(path);
        }

        private void StyleProgressBar()
        {
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Height = 6;
            progressBar.ForeColor = ThemeColors.Success;
            progressBar.BackColor = ThemeColors.Border;
        }

        private void StyleLabels()
        {
            lblEngagementMessage.Font = new Font("Segoe UI", 11F);
            lblEngagementMessage.ForeColor = ThemeColors.Primary;
            
            lblAttachmentStatus.Font = new Font("Segoe UI", 10F);
            lblAttachmentStatus.ForeColor = ThemeColors.TextSecondary;
        }

        private void SetupEventHandlers()
        {
            // Input field events
            txtLocation.Enter += txtLocation_Enter;
            txtLocation.Leave += txtLocation_Leave;
            txtDescription.Enter += txtDescription_Enter;
            txtDescription.Leave += txtDescription_Leave;
            txtDescription.TextChanged += txtDescription_TextChanged;
            
            // Category selection event
            cmbCategory.SelectedIndexChanged += (s, e) => UpdateProgress(s, e);
            
            // Button events
            btnSubmit.Click += btnSubmit_Click;
            btnViewReports.Click += btnViewReports_Click;
            btnAttachFile.Click += btnAttachFile_Click;
            
            // Form events
            this.FormClosing += ReportIssues_FormClosing;
        }

        private void ButtonHoverEffect(Button button, bool isHovered)
        {
            if (isHovered)
            {
                button.BackColor = ControlPaint.Light(button.BackColor);
            }
            else
            {
                button.BackColor = ControlPaint.Dark(button.BackColor);
            }
        }

        private void UpdateProgress(object sender, EventArgs e)
        {
            int progress = 0;
            int progressStep = 25;  // Each step is worth 25%

            // Check if location is filled
            if (!string.IsNullOrEmpty(txtLocation.Text) && txtLocation.Text != "Enter location")
                progress += progressStep;

            // Check if category is selected
            if (!string.IsNullOrEmpty(cmbCategory.Text) && cmbCategory.Text != "Select category")
                progress += progressStep;

            // Check if description is filled
            if (!string.IsNullOrEmpty(txtDescription.Text) && txtDescription.Text != "Enter description of the issue here...")
                progress += progressStep;

            // Check if a file is attached
            if (!string.IsNullOrEmpty(attachmentName))  // Check if attachmentName has a value
                progress += progressStep;

            // Update progress bar value
            progressBar.Value = progress;

            // Update engagement message based on progress
            if (progress == 0)
            {
                lblEngagementMessage.Text = "Let’s get started!";
            }
            else if (progress == 25)
            {
                lblEngagementMessage.Text = "Good start!";
            }
            else if (progress == 50)
            {
                lblEngagementMessage.Text = "You're halfway there!";
            }
            else if (progress == 75)
            {
                lblEngagementMessage.Text = "Almost done!";
            }
            else if (progress == 100)
            {
                lblEngagementMessage.Text = "Great! Ready to submit!";
            }
        }

        private void btnViewReports_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewReports viewReportsForm = new ViewReports(_issueRepository);
            viewReportsForm.TopLevel = false;
            viewReportsForm.FormBorderStyle = FormBorderStyle.None;
            viewReportsForm.Dock = DockStyle.Fill;
            ((Form1)this.ParentForm).Controls.Add(viewReportsForm);
            viewReportsForm.BringToFront();
            viewReportsForm.Show();
        }

        private void txtLocation_Enter(object sender, EventArgs e)
        {
            if (txtLocation.Text == "Enter location")
            {
                txtLocation.Text = "";
                txtLocation.ForeColor = ThemeColors.TextPrimary;
            }
        }

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                txtLocation.Text = "Enter location";
                txtLocation.ForeColor = ThemeColors.TextSecondary;
            }
            UpdateProgress(sender, e);
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            if (txtDescription.Text == "Enter description of the issue here...")
            {
                txtDescription.Text = "";
                txtDescription.ForeColor = ThemeColors.TextPrimary;
            }
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.Text = "Enter description of the issue here...";
                txtDescription.ForeColor = ThemeColors.TextSecondary;
            }
            UpdateProgress(sender, e);
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgress(sender, e);
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image and Document files|*.jpg;*.jpeg;*.png;*.pdf;*.docx|All files|*.*";
            fileDialog.Title = "Select an Image or Document";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Read file into byte array
                    attachmentData = File.ReadAllBytes(fileDialog.FileName);
                    attachmentName = Path.GetFileName(fileDialog.FileName);
                    attachmentType = Path.GetExtension(fileDialog.FileName).ToLower();

                    // Handle different file types
                    if (attachmentType == ".jpg" || attachmentType == ".jpeg" || attachmentType == ".png")
                    {
                        lblAttachmentStatus.Text = $"Image attached: {attachmentName}";
                    }
                    else if (attachmentType == ".pdf" || attachmentType == ".docx")
                    {
                        lblAttachmentStatus.Text = $"Document attached: {attachmentName}";
                    }
                    else
                    {
                        lblAttachmentStatus.Text = "Unsupported file type. Please attach an image or a document.";
                        ClearAttachment();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading file: " + ex.Message, "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAttachment();
                }
            }
            else
            {
                ClearAttachment();
                lblAttachmentStatus.Text = "No file attached.";
            }
            UpdateProgress(null, null);
        }

        private void ClearAttachment()
        {
            attachmentData = null;
            attachmentName = null;
            attachmentType = null;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Gather input data
            string location = txtLocation.Text;
            string category = cmbCategory.SelectedItem?.ToString();
            string description = txtDescription.Text;
            //-------------------------------------------------------------------------------------------------------
            // Validate input fields
            if (string.IsNullOrEmpty(location) || location == "Enter location" ||
                string.IsNullOrEmpty(category) || category == "Select category" ||
                string.IsNullOrEmpty(description) || description == "Enter description of the issue here...")
            {
                MessageBox.Show("Please fill in all fields before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //-------------------------------------------------------------------------------------------------------
            // Store issue in the list
            Issue newIssue = new Issue
            {
                Location = location,
                Category = category,
                Description = description,
                ReportDate = DateTime.Now,
                Status = IssueStatus.Reported,
                AttachmentName = attachmentName,
                AttachmentType = attachmentType,
                AttachmentData = attachmentData
            };
            //-------------------------------------------------------------------------------------------------------
            _issueRepository.AddIssue(newIssue);
            //-------------------------------------------------------------------------------------------------------
            // Simulate submission
            MessageBox.Show("Your report has been successfully submitted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //-------------------------------------------------------------------------------------------------------
            // Reset the form after submission
            txtLocation.Text = "Enter location";
            cmbCategory.SelectedIndex = -1;  // Reset category
            txtDescription.Text = "Enter description of the issue here...";
            lblAttachmentStatus.Text = "No file attached.";
            ClearAttachment();
            UpdateProgress(null, null);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (this.ParentForm != null)
            {
                this.ParentForm.Show();
            }
            this.Close();
            this.Dispose();
        }

        private void ReportIssues_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Handle form closing event
        }

    }

    public static class GraphicsExtensions
    {
        public static void AddRoundedRectangle(this GraphicsPath path, Rectangle bounds, int radius)
        {
            path.AddArc(bounds.X, bounds.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(bounds.Right - radius * 2, bounds.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(bounds.Right - radius * 2, bounds.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
        }
    }
}
