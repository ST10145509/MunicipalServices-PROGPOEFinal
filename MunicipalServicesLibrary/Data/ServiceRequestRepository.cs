using System;
using System.Collections.Generic;
using MunicipalServicesLibrary.Models;
using MunicipalServicesLibrary.DataStructures;

namespace MunicipalServicesLibrary.Data
{
    public class ServiceRequestRepository
    {
        private readonly RedBlackTree<ServiceRequest> _requestTree;
        private readonly ServiceRequestGraph _requestGraph;
        private readonly PriorityQueue<ServiceRequest> _priorityQueue;

        public ServiceRequestRepository()
        {
            _requestTree = new RedBlackTree<ServiceRequest>();
            _requestGraph = new ServiceRequestGraph();
            _priorityQueue = new PriorityQueue<ServiceRequest>();
        }

        public void AddRequest(ServiceRequest request)
        {
            _requestTree.Insert(request);
            _requestGraph.AddRequest(request.RequestId, request.Status);
            _priorityQueue.Enqueue(request);
        }

        public ServiceRequest GetRequest(string requestId)
        {
            return _requestTree.Find(new ServiceRequest { RequestId = requestId });
        }

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

        public ServiceRequest GetNextPriorityRequest()
        {
            return _priorityQueue.Count > 0 ? _priorityQueue.Dequeue() : null;
        }

        public ServiceRequest PeekNextPriorityRequest()
        {
            return _priorityQueue.Count > 0 ? _priorityQueue.Peek() : null;
        }
    }
}
