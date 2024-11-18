using System;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.Data;
using MunicipalServicesLibrary.DataStructures;
using System.Linq;

namespace MunicipalServicesLibrary.Models
{
    /// <summary>
    /// Represents a service request in the municipal system
    /// Implements IComparable for priority-based sorting
    /// </summary>
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        /// <summary>
        /// Unique identifier for the service request
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Detailed description of the service request
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date and time when the request was submitted
        /// </summary>
        public DateTime SubmissionDate { get; set; }

        /// <summary>
        /// Current status of the request (e.g., Reported, In Progress, Completed)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Category of the service request (e.g., Infrastructure, Utilities)
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Priority level of the request (lower number = higher priority)
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Default constructor that initializes a new service request with
        /// a unique ID, current timestamp, and "Reported" status
        /// </summary>
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
