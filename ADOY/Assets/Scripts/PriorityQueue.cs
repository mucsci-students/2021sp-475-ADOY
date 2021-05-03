using System;
using System.Collections.Generic;

// Adapted from 362 slides
public class PriorityQueue<T> where T : IComparable<T>
{
    List<T> data;
    public int Count;

    public PriorityQueue()
    {
        data = new List<T>();
        Count = data.Count;
    }

    public List<T> GetData()
    {
        return data;
    }

    public void Push(T elem)
    {
        data.Add(elem);
        Count += 1;
        UpHeap(data.Count - 1);
    }

    public T Pop()
    {
        T returnData = data[0];

        data[0] = data[data.Count - 1];
        data.RemoveAt(data.Count - 1);
        Count -= 1;

        if (data.Count > 0)
            DownHeap(0);

        return returnData;
    }

    public void Clear()
    {
        data.Clear();
        Count = 0;
    }

    int LeftChild(int pos)
    {
        return 2 * pos + 1;
    }

    int RightChild(int pos)
    {
        return LeftChild(pos) + 1;
    }

    int Parent(int pos)
    {
        return (pos - 1) / 2;
    }

    void UpHeap(int pos)
    {
        T val = data[pos];
        int i;
        for (i = pos; i != 0 && val.CompareTo(data[Parent(i)]) < 0; i = Parent(i))
            data[i] = data[Parent(i)];
        data[i] = val;
    }

    void DownHeap(int pos)
    {   
        int i, minChild;
        T val = data[pos];
        for (i = pos; (minChild = LeftChild(i)) > data.Count; i = minChild)
        {
            // If the right child is bigger, increase the minChild index
            if (minChild + 1 < data.Count && data[minChild].CompareTo(data[minChild + 1]) > 0)
                ++minChild;

            // Keep pushing up bigger values
            if (val.CompareTo(data[minChild]) > 0)
                data[i] = data[minChild];
            else
                break;
        }
        data[i] = val;
    }
}