using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Alien : OriginalObject<LocationData>
{
    #region Variables
    NavMeshAgent agent;
    Rigidbody rb;
    enum States { Searching, PlayerInSight, PlayerNoLongerInSight, PlayerDead }
    States state = States.Searching;

    PathNode currentNode;
    [SerializeField]
    [Range(.5f, 2)]
    float acceptableNodeRange = .75f;

    [SerializeField]
    [Range(1, 20)]
    float searchSpeed = 2.5f;

    [SerializeField]
    [Range(1, 20)]
    float runSpeed = 7.5f;

    int fov = 60;
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
            return Vector3.Distance(transform.position, Player.Instance.transform.position) <= 1.5f;
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

    protected override void Update()
    {
        DetermineAction();
        base.Update();
    }

    void DetermineAction()
    {
        switch (state)
        {
            case States.PlayerInSight:
                {
                    Vector3 playerPos = Player.Instance.transform.position;
                    agent.SetDestination(playerPos);

                    if (InRangeOfPlayer)
                    {
                        Debug.Log("In range of player");
                        state = States.PlayerDead;
                        Player.Instance.KillPlayer();
                        return;
                    }

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
                        fov = 60;
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
                        Invoke("PickNode", 3);
                    }

                    if (PlayerInSight())
                    {
                        audio.Stop();
                        audio.PlayOneShot(dunDunClip);


                        currentNode = null;
                        state = States.PlayerInSight;
                        agent.speed = runSpeed;
                        fov = 75;
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
            case States.PlayerDead:
                return;
        }
    }

    bool PlayerInSight()
    {
        Vector3 playerPos = Player.Instance.transform.position;
        float angle = Vector3.Angle(transform.forward, playerPos - transform.position);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red);

        if (angle <= fov)
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

    protected override void AddData()
    {
        EnqueueData(new LocationData(transform.position, transform.rotation));
    }
}
