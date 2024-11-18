using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; }
        public IssueStatus Status { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
        public byte[] AttachmentData { get; set; }
    }

    public enum IssueStatus
    {
        Reported,
        InProgress,
        Resolved,
        Closed
    }
}
