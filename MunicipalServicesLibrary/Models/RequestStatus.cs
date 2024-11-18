using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Models
{
    public enum RequestStatus
    {
        Reported,      // Initial status when issue is reported
        InReview,       // Municipal staff reviewing the issue
        Assigned,       // Assigned to relevant department/worker
        InProgress,     // Work has started
        Resolved,      // Issue has been resolved
        Closed         // Final status after verification
    }
}

