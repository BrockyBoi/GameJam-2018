using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PathNode : MonoBehaviour
{
    static List<PathNode> nodes = new List<PathNode>();
    public static List<PathNode> Nodes { get { return nodes; } }
    public static PathNode RandomNode { get { return nodes[Random.Range(0, nodes.Count)]; } }
    static Transform parentTransform;
    static Transform ParentTransform
    {
        get
        {
			if(parentTransform == null)
			{
				parentTransform = new GameObject("Path Nodes").transform;
			}
			return parentTransform;
        }
		
    }
    void Awake()
    {
        nodes.Add(this);
        GetComponent<MeshRenderer>().enabled = false;

		transform.SetParent(ParentTransform);
    }

	void OnDisable()
	{
		nodes.Clear();
	}

}
