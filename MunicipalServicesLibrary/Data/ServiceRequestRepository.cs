using System;
using System.Collections.Generic;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.DataStructures;

namespace MunicipalServicesLibrary.Data
{
    /// <summary>
    /// Repository class that manages service requests using multiple data structures
    /// for efficient storage, retrieval, and relationship tracking
    /// </summary>
    public class ServiceRequestRepository
    {
        /// <summary>
        /// Red-Black Tree for balanced storage and retrieval of service requests
        /// </summary>
        private readonly RedBlackTree<ServiceRequest> _requestTree;

        /// <summary>
        /// Graph structure for managing relationships between service requests
        /// </summary>
        private readonly ServiceRequestGraph _requestGraph;

        /// <summary>
        /// Priority Queue for managing requests based on priority level
        /// </summary>
        private readonly PriorityQueue<ServiceRequest> _priorityQueue;

        /// <summary>
        /// Initializes a new instance of the ServiceRequestRepository
        /// Sets up the required data structures
        /// </summary>
        public ServiceRequestRepository()
        {
            _requestTree = new RedBlackTree<ServiceRequest>();
            _requestGraph = new ServiceRequestGraph();
            _priorityQueue = new PriorityQueue<ServiceRequest>();
        }

        /// <summary>
        /// Adds a new service request to all data structures
        /// </summary>
        /// <param name="request">The service request to add</param>
        public void AddRequest(ServiceRequest request)
        {
            _requestTree.Insert(request);
            _requestGraph.AddRequest(request.RequestId, request.Status);
            _priorityQueue.Enqueue(request);
        }

        /// <summary>
        /// Retrieves a specific service request by ID using the Red-Black Tree
        /// </summary>
        /// <param name="requestId">ID of the request to retrieve</param>
        /// <returns>The requested ServiceRequest or null if not found</returns>
        public ServiceRequest GetRequest(string requestId)
        {
            return _requestTree.Find(new ServiceRequest { RequestId = requestId });
        }

        /// <summary>
        /// Gets all related requests for a given request ID using the graph structure
        /// </summary>
        /// <param name="requestId">ID of the request to find relations for</param>
        /// <returns>List of related service requests</returns>
        public List<ServiceRequest> GetRelatedRequests(string requestId)
        {
            var relatedNodes = _requestGraph.GetRelatedRequests(requestId);
            var result = new List<ServiceRequest>();
            foreach (var node in relatedNodes)
            {
                var request = GetRequest(node.RequestId);
                if (request != null)
                {
                    result.Add(request);
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieves the next priority request from the priority queue
        /// </summary>
        /// <returns>The next priority request or null if none available</returns>
        public ServiceRequest GetNextPriorityRequest()
        {
            return _priorityQueue.Count > 0 ? _priorityQueue.Dequeue() : null;
        }

        /// <summary>
        /// Peeks at the next priority request in the priority queue without removing it
        /// </summary>
        /// <returns>The next priority request or null if none available</returns>
        public ServiceRequest PeekNextPriorityRequest()
        {
            return _priorityQueue.Count > 0 ? _priorityQueue.Peek() : null;
        }
    }
}
