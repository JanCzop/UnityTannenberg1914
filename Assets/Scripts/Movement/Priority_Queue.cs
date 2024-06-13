using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priority_Queue<T>
{
    private List<(T item, float priority)> elements = new List<(T item, float priority)>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority)
    {
        elements.Add((item, priority));
        HeapifyUp(elements.Count - 1);
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("The priority queue is empty.");

        var result = elements[0].item;
        elements[0] = elements[^1];
        elements.RemoveAt(elements.Count - 1);

        if (elements.Count > 0)
            HeapifyDown(0);

        return result;
    }

    public bool Contains(T item)
    {
        return elements.Exists(e => EqualityComparer<T>.Default.Equals(e.item, item));
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (elements[index].priority >= elements[parentIndex].priority)
                break;

            (elements[index], elements[parentIndex]) = (elements[parentIndex], elements[index]);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        int lastIndex = elements.Count - 1;
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            if (leftChildIndex > lastIndex)
                break;

            int smallerChildIndex = (rightChildIndex > lastIndex || elements[leftChildIndex].priority < elements[rightChildIndex].priority)
                ? leftChildIndex
                : rightChildIndex;

            if (elements[index].priority <= elements[smallerChildIndex].priority)
                break;

            (elements[index], elements[smallerChildIndex]) = (elements[smallerChildIndex], elements[index]);
            index = smallerChildIndex;
        }
    }
}