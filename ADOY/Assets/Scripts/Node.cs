using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : IComparable<Node>
{
    public HashSet<GameObject> neighbors;
    public float f, g, h;
    public Vector3 position;
    public bool visited;

    public int CompareTo(Node other)
    {
        return f.CompareTo(other.f);
    }
}
