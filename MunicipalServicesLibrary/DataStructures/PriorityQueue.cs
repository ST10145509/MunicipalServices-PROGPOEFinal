using System;
using System.Collections.Generic;
using MunicipalServicesLibrary.Models;

namespace MunicipalServicesLibrary.DataStructures
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> heap;
        
        public PriorityQueue()
        {
            heap = new List<T>();
        }
        
        public void Enqueue(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }
        
        public T Dequeue()
        {
            if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
            
            T result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            if (heap.Count > 0) HeapifyDown(0);
            
            return result;
        }

        public int Count => heap.Count;
        
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
