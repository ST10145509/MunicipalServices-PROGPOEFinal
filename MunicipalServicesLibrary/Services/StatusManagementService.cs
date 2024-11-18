using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.Data;
using MunicipalServicesLibrary.DataStructures;

namespace MunicipalServicesLibrary.Services
{
    public class StatusManagementService
    {
        private readonly IssueRepository _issueRepository;
        private readonly ServiceRequestGraph _requestGraph;

        public StatusManagementService(IssueRepository issueRepository, ServiceRequestGraph requestGraph)
        {
            _issueRepository = issueRepository;
            _requestGraph = requestGraph;
        }

        public void UpdateStatus(string requestId, RequestStatus newStatus)
        {
            var issue = _issueRepository.GetAllIssues()
                .FirstOrDefault(i => i.Id.ToString("X8") == requestId);

            if (issue != null)
            {
                // Get current RequestStatus
                var currentStatus = MapFromIssueStatus(issue.Status);
                
                // Only allow progression to next status
                if (IsValidStatusProgression(currentStatus, newStatus))
                {
                    // Update issue status
                    issue.Status = MapToIssueStatus(newStatus);
                    
                    // Update request graph
                    _requestGraph.UpdateRequestStatus(requestId, newStatus.ToString());
                    
                    // Log status change
                    LogStatusChange(requestId, newStatus);

                    // Signal UI update needed
                    OnStatusChanged?.Invoke(requestId, newStatus);
                }
            }
        }

        private bool IsValidStatusProgression(RequestStatus current, RequestStatus next)
        {
            // Define valid progressions
            var validProgressions = new Dictionary<RequestStatus, RequestStatus[]>
            {
                { RequestStatus.Reported, new[] { RequestStatus.InReview } },
                { RequestStatus.InReview, new[] { RequestStatus.Assigned } },
                { RequestStatus.Assigned, new[] { RequestStatus.InProgress } },
                { RequestStatus.InProgress, new[] { RequestStatus.Resolved } },
                { RequestStatus.Resolved, new[] { RequestStatus.Closed } },
                { RequestStatus.Closed, new RequestStatus[0] }
            };

            return validProgressions.ContainsKey(current) && 
                   validProgressions[current].Contains(next);
        }

        private RequestStatus MapFromIssueStatus(IssueStatus status)
        {
            switch (status)
            {
                case IssueStatus.Reported:
                    return RequestStatus.Reported;
                case IssueStatus.InProgress:
                    {
                        // Check if it's in an intermediate state
                        var request = _requestGraph.GetRequestStatus(status.ToString());
                        if (request != null)
                        {
                            if (request == "InReview") return RequestStatus.InReview;
                            if (request == "Assigned") return RequestStatus.Assigned;
                            return RequestStatus.InProgress;
                        }
                        return RequestStatus.InProgress;
                    }
                case IssueStatus.Resolved:
                    return RequestStatus.Resolved;
                case IssueStatus.Closed:
                    return RequestStatus.Closed;
                default:
                    return RequestStatus.Reported;
            }
        }

        private IssueStatus MapToIssueStatus(RequestStatus status)
        {
            switch (status)
            {
                case RequestStatus.Reported:
                    return IssueStatus.Reported;
                case RequestStatus.InProgress:
                case RequestStatus.InReview:
                case RequestStatus.Assigned:
                    return IssueStatus.InProgress;
                case RequestStatus.Resolved:
                    return IssueStatus.Resolved;
                case RequestStatus.Closed:
                    return IssueStatus.Closed;
                default:
                    return IssueStatus.Reported;
            }
        }

        private void LogStatusChange(string requestId, RequestStatus newStatus)
        {
            // Add logging logic here
            Console.WriteLine($"Status updated for request {requestId} to {newStatus} at {DateTime.Now}");
        }

        // Add event for status changes
        public event Action<string, RequestStatus> OnStatusChanged;

        public void InitializeStatus(string requestId, RequestStatus status)
        {
            var issue = _issueRepository.GetAllIssues()
                .FirstOrDefault(i => i.Id.ToString("X8") == requestId);

            if (issue != null)
            {
                issue.Status = MapToIssueStatus(status);
                _requestGraph.UpdateRequestStatus(requestId, status.ToString());
            }
        }
    }
}
