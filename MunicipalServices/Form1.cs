using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.Data;
using MunicipalServices.Forms;

namespace MunicipalServices
{
    public partial class Form1 : Form
    {
        private Form activeForm = null;
        
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            InitializeMDILayout();
            LoadImage();
        }

        private void InitializeMDILayout()
        {
            // Create MenuStrip
            MenuStrip menuStrip = new MenuStrip();
            
            // Issues Menu
            ToolStripMenuItem issuesMenu = new ToolStripMenuItem("Issues");
            issuesMenu.DropDownItems.Add("Report Issue", null, ShowReportIssue);
            issuesMenu.DropDownItems.Add("View Reports", null, ShowViewReports);
            issuesMenu.DropDownItems.Add("Service Request Status", null, ShowServiceRequestStatus);
            
            // Events Menu
            ToolStripMenuItem eventsMenu = new ToolStripMenuItem("Events");
            eventsMenu.DropDownItems.Add("Local Events", null, ShowLocalEvents);

            menuStrip.Items.Add(issuesMenu);
            menuStrip.Items.Add(eventsMenu);
            
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
        }

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
            }
            
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.AutoScroll = true;
            
            // Create a panel to host the child form
            Panel hostPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            
            this.Controls.Add(hostPanel);
            hostPanel.Controls.Add(childForm);
            
            // Center the child form within the panel
            childForm.Location = new Point(
                (hostPanel.Width - childForm.Width) / 2,
                (hostPanel.Height - childForm.Height) / 2
            );
            
            childForm.Show();
            hostPanel.BringToFront();
        }
        
        private void ShowReportIssue(object sender, EventArgs e)
        {
            OpenChildForm(new ReportIssues());
        }

        private void ShowViewReports(object sender, EventArgs e)
        {
            OpenChildForm(new ViewReports(new IssueRepository()));
        }

        private void CloseAllChildForms()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }

        private void LoadImage()
        {
            string imagePath = Path.Combine(Application.StartupPath, "Images", "helping hands.jpeg");
            if (File.Exists(imagePath))
            {
                pictureBoxLogo.Image = Image.FromFile(imagePath);
                pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Image not found: " + imagePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void lblAppName_Click(object sender, EventArgs e)
        {

        }

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            ShowReportIssue(sender, e);
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            var eventRepository = new EventRepository();
            var localEventsForm = new LocalEvents(eventRepository)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(localEventsForm);
            localEventsForm.BringToFront();
            localEventsForm.Show();
        }

        private void ShowLocalEvents(object sender, EventArgs e)
        {
            var eventRepository = new EventRepository();
            OpenChildForm(new LocalEvents(eventRepository));
        }

        private void btnRequestStatus_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.ServiceRequestStatus());
        }

        private void ShowServiceRequestStatus(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.ServiceRequestStatus());
        }
    }
}
