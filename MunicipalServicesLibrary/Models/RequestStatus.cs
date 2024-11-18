using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Models
{
    /// <summary>
    /// Represents the various states a service request can be in throughout its lifecycle
    /// </summary>
    public enum RequestStatus
    {
        /// <summary>Initial status when a citizen reports an issue</summary>
        Reported,

        /// <summary>Issue is being reviewed by municipal staff</summary>
        InReview,

        /// <summary>Issue has been assigned to specific department or worker</summary>
        Assigned,

        /// <summary>Work on resolving the issue has begun</summary>
        InProgress,

        /// <summary>Issue has been resolved but pending final verification</summary>
        Resolved,

        /// <summary>Issue has been verified and case is closed</summary>
        Closed
    }
}

