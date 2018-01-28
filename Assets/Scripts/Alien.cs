using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Alien : MonoBehaviour
{
    #region Variables
    NavMeshAgent agent;
    Rigidbody rb;
    enum States { Searching, PlayerInSight, PlayerNoLongerInSight, }
    States state = States.Searching;

    PathNode currentNode;
    [SerializeField]
    [Range(.5f, 2)]
    float acceptableNodeRange = .75f;

    [SerializeField]
    [Range(5, 50)]
    int searchSpeed = 5;

    [SerializeField]
    [Range(5, 50)]
    int runSpeed = 15;
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
    float lastTimePlayedAudio = 0;
    new AudioSource audio;
    [SerializeField]
    AudioClip roarClip, gargleClip, dunDunClip;
    #endregion

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        PickNode();
        agent.speed = searchSpeed;
    }

    void PickNode()
    {
        currentNode = PathNode.RandomNode;
        agent.SetDestination(currentNode.transform.position);
    }

    void LateUpdate()
    {
        DetermineAction();
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
                        agent.speed = searchSpeed;
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
                        agent.speed = runSpeed;
                        timeNotSeenPlayer = 0;
                    }
                    if (timeNotSeenPlayer > 3.5f)
                    {
                        state = States.Searching;
                        agent.speed = searchSpeed;
                        timeNotSeenPlayer = 0;
                        lastTimePlayedAudio = 0;
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
                        if (!audio.isPlaying)
                        {
                            audio.PlayOneShot(dunDunClip);
                        }

                        currentNode = null;
                        state = States.PlayerInSight;
                        agent.speed = runSpeed;
                    }

                    lastTimePlayedAudio += Time.deltaTime;

                    if (lastTimePlayedAudio > 15)
                    {
                        if (Random.value < .5f)
                        {
                            audio.PlayOneShot(gargleClip);
                        }
                        else
                        {
                            audio.PlayOneShot(roarClip);
                        }

                        lastTimePlayedAudio = 0;
                    }
                    break;
                }
        }
    }

    bool PlayerInSight()
    {
        Vector3 playerPos = Player.Instance.transform.position;
        float angle = Vector3.Angle(transform.forward, playerPos - transform.position);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);

        if (angle <= 60)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, playerPos - transform.position, out hit))
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
