using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{

    NavMeshAgent agent;
	Rigidbody rb;
    enum States { Searching, InSight, Running, }
    States state = States.Searching;

	PathNode currentNode;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody>();
    }

	void Start()
	{
		PickNode();
	}

	void PickNode()
	{
		currentNode = PathNode.RandomNode;
		agent.SetDestination(currentNode.transform.position);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var button = hit.collider.GetComponent<LockButton>();
                if (button != null)
                {
                    button.PressButton();
                }
            }
        }

		if(rb.velocity == Vector3.zero)
		{
			PickNode();
		}
		
    }

    void DetermineAction()
    {
        switch (state)
        {
            case States.InSight:
                break;
            case States.Running:
                break;
            case States.Searching:
                break;
        }
    }
}
