using UnityEngine;
using System.Collections.Generic;

public class NodeAttributes : MonoBehaviour
{
    public Node node;
    public LayerMask nodeMask;

    HashSet<GameObject> neighbors;
    public Material foundMat;

    void Start()
    {
        neighbors = new HashSet<GameObject>();
        node = new Node();
        node.position = transform.position;
        node.g = Mathf.Infinity;

        Collider[] overlap = Physics.OverlapSphere(transform.position, 5.0f);
        
        foreach (Collider c in overlap)
        {
            // c.gameObject.GetComponent<MeshRenderer>().material = foundMat;
            neighbors.Add(c.gameObject);
            print("SOURCE: " + transform.position + "    NEIGHBOR: " + c.transform.position);
        }
    }

    void Update()
    {
        node.neighbors = neighbors;
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     if ((other.CompareTag("Node") || other.CompareTag("Player")) 
    //         && !Physics.Raycast(transform.position, 
    //             (other.transform.position - transform.position).normalized, 
    //             Vector3.Distance(transform.position, other.transform.position), wallMask))
    //     {
    //         neighbors.Add(other.gameObject);
    //     }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //         neighbors.Remove(other.gameObject);
    // }
}
