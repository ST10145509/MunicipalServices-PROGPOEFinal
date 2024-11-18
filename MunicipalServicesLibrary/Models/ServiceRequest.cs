using System;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.Data;
using MunicipalServicesLibrary.DataStructures;
using System.Linq;

namespace MunicipalServicesLibrary.Models
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        public string RequestId { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }

        public ServiceRequest()
        {
            RequestId = Guid.NewGuid().ToString().Substring(0, 8);
            SubmissionDate = DateTime.Now;
            Status = "Reported";
        }

        public int CompareTo(ServiceRequest other)
        {
            int priorityComparison = this.Priority.CompareTo(other.Priority);
            if (priorityComparison != 0)
                return -priorityComparison;
            
            int dateComparison = this.SubmissionDate.CompareTo(other.SubmissionDate);
            if (dateComparison != 0)
                return dateComparison;
            
            return this.RequestId.CompareTo(other.RequestId);
        }
    }
}
