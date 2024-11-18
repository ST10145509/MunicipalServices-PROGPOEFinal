using System;
using System.Collections.Generic;
using MunicipalServicesLibrary.Models;

namespace MunicipalServicesLibrary.DataStructures
{
    /// <summary>
    /// Generic Priority Queue implementation using a min-heap structure
    /// Allows for efficient retrieval of items based on their priority
    /// </summary>
    /// <typeparam name="T">Type that implements IComparable for priority comparison</typeparam>
    public class PriorityQueue<T> where T : IComparable<T>
    {
        /// <summary>
        /// Internal list representing the heap structure
        /// </summary>
        private List<T> heap;
        
        /// <summary>
        /// Initializes a new empty priority queue
        /// </summary>
        public PriorityQueue()
        {
            heap = new List<T>();
        }
        
        /// <summary>
        /// Adds an item to the queue and maintains heap property
        /// Time Complexity: O(log n)
        /// </summary>
        /// <param name="item">Item to be added to the queue</param>
        public void Enqueue(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }
        
        /// <summary>
        /// Removes and returns the highest priority item (minimum value)
        /// Time Complexity: O(log n)
        /// </summary>
        /// <returns>The highest priority item</returns>
        /// <exception cref="InvalidOperationException">Thrown when queue is empty</exception>
        public T Dequeue()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
            
            T result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            if (heap.Count > 0) HeapifyDown(0);
            
            return result;
        }

        /// <summary>
        /// Gets the current number of items in the queue
        /// </summary>
        public int Count => heap.Count;
        
        /// <summary>
        /// Restores heap property by moving an item up the tree
        /// </summary>
        /// <param name="index">Starting index of the item to move</param>
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[parentIndex].CompareTo(heap[index]) <= 0) break;
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }
        
        /// <summary>
        /// Restores heap property by moving an item down the tree
        /// </summary>
        /// <param name="index">Starting index of the item to move</param>
        private void HeapifyDown(int index)
        {
            while (true)
            {
                int smallest = index;
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                
                if (left < heap.Count && heap[left].CompareTo(heap[smallest]) < 0)
                    smallest = left;
                if (right < heap.Count && heap[right].CompareTo(heap[smallest]) < 0)
                    smallest = right;
                
                if (smallest == index) break;
                Swap(index, smallest);
                index = smallest;
            }
        }
        
        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public T Peek()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
            return heap[0];
        }
    }
}
