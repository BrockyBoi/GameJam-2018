using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class Alien : OriginalObject<LocationData>
{
    #region Variables
    private static event DelegateManager.VecVoid EOnAttracted;
    NavMeshAgent agent;
    Rigidbody rb;
    enum States { Searching, PlayerInSight, PlayerNoLongerInSight, PlayerDead, AttractedToNoise }
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

    bool InRangeOfNoise
    {
        get
        {
            return Vector3.Distance(transform.position, noiseAttractionSpot) <= 2;
        }
    }

    float timeNotSeenPlayer = 0;
    Vector3 lastPlayerPos;
    float lastTimePlayedAudio = 0;
    new AudioSource audio;
    [SerializeField]
    AudioClip roarClip, gargleClip, dunDunClip;

    static Vector3 noiseAttractionSpot;
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
                        state = States.PlayerDead;
                        Player.Instance.KillPlayer();
                        return;
                    }

                    if (!PlayerInSight())
                    {
                        lastPlayerPos = playerPos;
                        state = States.PlayerNoLongerInSight;
                        agent.speed = searchSpeed;
                        agent.SetDestination(lastPlayerPos);
                    }
                    break;
                }
            case States.PlayerNoLongerInSight:
                {
                    timeNotSeenPlayer += Time.deltaTime;


                    if (PlayerInSight())
                    {
                        ChasePlayer();
                    }
                    if (timeNotSeenPlayer > 3.5f)
                    {
                        StartSearch();
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
                        ChasePlayer();
                    }

                    CheckPlayRandomAudio();
                    break;
                }
            case States.PlayerDead:
                return;
            case States.AttractedToNoise:
                {
                    if (PlayerInSight())
                    {
                        ChasePlayer();
                    }

                    if (InRangeOfNoise)
                    {
                        Invoke("PickNode", 3);
                    }
                    break;
                }
        }
    }

    void StartSearch()
    {
        state = States.Searching;
        agent.speed = searchSpeed;
        timeNotSeenPlayer = 0;
        lastTimePlayedAudio = 0;
        fov = 60;
    }

    void ChasePlayer()
    {
        CancelInvoke("PickNode");
        audio.Stop();
        audio.PlayOneShot(dunDunClip);

        currentNode = null;
        state = States.PlayerInSight;
        agent.speed = runSpeed;
        fov = 75;
        timeNotSeenPlayer = 0;
    }

    void CheckPlayRandomAudio()
    {
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
    }

    public static void AttractAliens(Vector3 spot)
    {
        if (EOnAttracted != null)
            EOnAttracted(spot);
    }

    void OnEnable()
    {
        EOnAttracted += MoveToAttractionSpot;
    }

    void OnDisable()
    {
        EOnAttracted -= MoveToAttractionSpot;
    }

    void MoveToAttractionSpot(Vector3 spot)
    {
        noiseAttractionSpot = spot;
        agent.speed = runSpeed;
        state = States.AttractedToNoise;
        agent.SetDestination(spot);
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
