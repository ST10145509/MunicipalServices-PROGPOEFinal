using MunicipalServicesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MunicipalServicesLibrary.DataStructures
{
    public class ServiceRequestNode
    {
        public string RequestId { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public Dictionary<ServiceRequestNode, int> Edges { get; set; }

        public ServiceRequestNode(string requestId, string status)
        {
            RequestId = requestId;
            Status = status;
            Timestamp = DateTime.Now;
            Edges = new Dictionary<ServiceRequestNode, int>();
        }
    }

    public class ServiceRequestGraph
    {
        private Dictionary<string, Dictionary<string, int>> edges;
        private Dictionary<string, ServiceRequestNode> nodes;
        private List<string> statusOrder = new List<string> 
        { 
            "Submitted", 
            "In Review", 
            "Assigned", 
            "In Progress", 
            "Completed" 
        };

        public ServiceRequestGraph()
        {
            edges = new Dictionary<string, Dictionary<string, int>>();
            nodes = new Dictionary<string, ServiceRequestNode>();
        }

        public void AddRequest(string requestId, string status)
        {
            if (!nodes.ContainsKey(requestId))
            {
                nodes[requestId] = new ServiceRequestNode(requestId, status);
                edges[requestId] = new Dictionary<string, int>();
                
                // Connect to existing nodes based on status
                foreach (var existingNode in nodes)
                {
                    if (existingNode.Key != requestId)
                    {
                        int weight = CalculateWeight(status, existingNode.Value.Status);
                        if (weight > 0)
                        {
                            edges[requestId][existingNode.Key] = weight;
                            edges[existingNode.Key][requestId] = weight;
                        }
                    }
                }
            }
        }

        private int CalculateWeight(string status1, string status2)
        {
            var index1 = statusOrder.IndexOf(status1);
            var index2 = statusOrder.IndexOf(status2);
            if (index1 == -1 || index2 == -1) return 0;
            
            // Calculate weight as an integer (1-10 scale)
            int difference = Math.Abs(index1 - index2);
            return Math.Max(10 - difference * 2, 1); // Higher weight for closer statuses
        }

        public List<ServiceRequestNode> GetRelatedRequests(string requestId, int maxDistance = 2)
        {
            var result = new List<ServiceRequestNode>();
            if (!nodes.ContainsKey(requestId)) return result;

            var visited = new HashSet<string>();
            var queue = new Queue<Tuple<ServiceRequestNode, int>>();
            queue.Enqueue(new Tuple<ServiceRequestNode, int>(nodes[requestId], 0));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var node = current.Item1;
                var distance = current.Item2;

                if (!visited.Contains(node.RequestId))
                {
                    visited.Add(node.RequestId);
                    if (node.RequestId != requestId)
                        result.Add(node);

                    if (distance < maxDistance)
                    {
                        foreach (var edge in node.Edges)
                        {
                            if (!visited.Contains(edge.Key.RequestId))
                            {
                                queue.Enqueue(new Tuple<ServiceRequestNode, int>(edge.Key, distance + 1));
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void AddRequestRelation(string requestId1, string requestId2)
        {
            if (!nodes.ContainsKey(requestId1) || !nodes.ContainsKey(requestId2))
            {
                throw new ArgumentException("One or both request IDs do not exist in the graph");
            }

            var node1 = nodes[requestId1];
            var node2 = nodes[requestId2];

            // Calculate weight based on status progression
            int weight = Math.Abs(statusOrder.IndexOf(node1.Status) - statusOrder.IndexOf(node2.Status));

            // Add bidirectional edges
            if (!node1.Edges.ContainsKey(node2))
            {
                node1.Edges[node2] = weight;
            }
            if (!node2.Edges.ContainsKey(node1))
            {
                node2.Edges[node1] = weight;
            }
        }

        public IEnumerable<ServiceRequest> GetRelatedRequests(string requestId)
        {
            if (!nodes.ContainsKey(requestId))
            {
                return new List<ServiceRequest>();
            }

            var relatedRequests = new List<ServiceRequest>();
            var relatedNodes = GetRelatedRequests(requestId, 2);

            foreach (var node in relatedNodes)
            {
                relatedRequests.Add(new ServiceRequest
                {
                    RequestId = node.RequestId,
                    Status = node.Status
                });
            }

            // Sort by weight (closer status = more related)
            relatedRequests.Sort((a, b) => 
                Math.Abs(statusOrder.IndexOf(a.Status) - statusOrder.IndexOf(nodes[requestId].Status))
                .CompareTo(Math.Abs(statusOrder.IndexOf(b.Status) - statusOrder.IndexOf(nodes[requestId].Status)))
            );

            return relatedRequests;
        }

        public void UpdateRequestStatus(string requestId, string newStatus)
        {
            if (nodes.ContainsKey(requestId))
            {
                var node = nodes[requestId];
                node.Status = newStatus;
                
                // Recalculate edges based on new status
                foreach (var otherNode in nodes.Values)
                {
                    if (otherNode.RequestId != requestId)
                    {
                        int weight = CalculateWeight(newStatus, otherNode.Status);
                        if (weight > 0)
                        {
                            node.Edges[otherNode] = weight;
                            otherNode.Edges[node] = weight;
                        }
                        else
                        {
                            node.Edges.Remove(otherNode);
                            otherNode.Edges.Remove(node);
                        }
                    }
                }
            }
        }

        public void AddRelatedRequests(string requestId, string relatedId)
        {
            if (!nodes.ContainsKey(requestId) || !nodes.ContainsKey(relatedId))
                return;

            var node1 = nodes[requestId];
            var node2 = nodes[relatedId];

            // Create relationship based on status proximity
            int statusDiff = Math.Abs(statusOrder.IndexOf(node1.Status) - statusOrder.IndexOf(node2.Status));
            int weight = 1 / (statusDiff + 1);

            // Add or update edge
            if (!edges.ContainsKey(requestId))
                edges[requestId] = new Dictionary<string, int>();
            
            edges[requestId][relatedId] = weight;

            // Add reverse edge for undirected graph
            if (!edges.ContainsKey(relatedId))
                edges[relatedId] = new Dictionary<string, int>();
            
            edges[relatedId][requestId] = weight;
        }

        public string GetRequestStatus(string requestId)
        {
            if (nodes.ContainsKey(requestId))
            {
                return nodes[requestId].Status;
            }
            return null;
        }
    }
}
