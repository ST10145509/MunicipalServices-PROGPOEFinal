using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MunicipalServicesLibrary.Models;

namespace MunicipalServices.Forms
{
    public partial class StatusUpdateForm : Form
    {
        public RequestStatus SelectedStatus { get; private set; }
        private ComboBox cmbStatus;

        public StatusUpdateForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            cmbStatus = new ComboBox
            {
                DataSource = Enum.GetValues(typeof(RequestStatus)),
                Location = new Point(12, 12),
                Size = new Size(200, 30)
            };

            var btnOk = new Button
            {
                Text = "Update",
                DialogResult = DialogResult.OK,
                Location = new Point(12, 50)
            };

            btnOk.Click += (s, e) =>
            {
                SelectedStatus = (RequestStatus)cmbStatus.SelectedItem;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { cmbStatus, btnOk });
            this.Size = new Size(240, 120);
        }
    }
}
