using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    enum States { Searching, PlayerInSight, PlayerNoLongerInSight, }
    States state = States.Searching;

    PathNode currentNode;
    [SerializeField]
    float acceptableNodeRange = .2f;
    bool InRangeOfNode
    {
        get
        {
            return Vector3.Distance(transform.position, currentNode.transform.position) < acceptableNodeRange;
        }
    }

    bool InRangeOfPlayer
    {
        get
        {
            return Vector3.Distance(transform.position, Player.Instance.transform.position) <= .5f;
        }
    }

    float timeNotSeenPlayer = 0;
    Vector3 lastPlayerPos;

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
    }

    void LateUpdate()
    {
        if (InRangeOfNode)
        {
            PickNode();
        }
    }

    void DetermineAction()
    {
        switch (state)
        {
            case States.PlayerInSight:
                {
                    Vector3 playerPos = Player.Instance.transform.position;
                    agent.SetDestination(playerPos);

                    if (!PlayerInSight())
                    {
                        lastPlayerPos = playerPos;
                        state = States.PlayerNoLongerInSight;
                    }
                    break;
                }
            case States.PlayerNoLongerInSight:
                {
                    timeNotSeenPlayer += Time.deltaTime;
                    agent.SetDestination(lastPlayerPos);

                    if (PlayerInSight())
                    {
                        state = States.PlayerInSight;
                        timeNotSeenPlayer = 0;
                    }
                    if (timeNotSeenPlayer > 3.5f)
                    {
                        state = States.Searching;
                        timeNotSeenPlayer = 0;
                    }
                    break;
                }
            case States.Searching:
                {
                    if (currentNode == null)
                    {
                        PickNode();
                    }

                    if (InRangeOfNode)
                    {
                        PickNode();
                    }

                    if (PlayerInSight())
                    {
                        currentNode = null;
                        state = States.PlayerInSight;
                    }
                    break;
                }
        }
    }

    bool PlayerInSight()
    {
        Vector3 playerPos = Player.Instance.transform.position;
        if (Vector3.Angle(transform.position, playerPos) <= 45)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerPos, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
