using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> lights;
    [SerializeField]
    GameObject door;
    int numCorrect = 0;
    static RuneManager instance;
    static RuneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Rune Manager", typeof(RuneManager)).GetComponent<RuneManager>();
            }
            return instance;
        }
    }

    void OnEnable()
    {
        Player.EOnPlayerDeath += ClearDict;
    }

    void OnDisable()
    {
        Player.EOnPlayerDeath -= ClearDict;
    }

    void ClearDict()
    {
        Instance.runeKey.Clear();
		RuneStation.numStations = 0;
    }
    Dictionary<int, RuneStation.Colors> runeKey;
    public static Dictionary<int, RuneStation.Colors> RuneKey
    {
        get
        {
            return Instance.runeKey;
        }
        set
        {
            Instance.runeKey = value;
        }
    }
    // Use this for initialization

    void Awake()
    {
        instance = this;
        runeKey = new Dictionary<int, RuneStation.Colors>();
        // initialize runekey randomly
        RuneStation.Colors[] c = { RuneStation.Colors.Blue, RuneStation.Colors.Red, RuneStation.Colors.NeonGreen, RuneStation.Colors.Purple };
        List<RuneStation.Colors> colors = new List<RuneStation.Colors>(c);
        int i = 0;
        while (i < 4)
        {
            int k = Random.Range(0, colors.Count);
            runeKey[i] = colors[k];
            colors.RemoveAt(k);
            i++;
        }

    }

    public static void CorrectbuttonPressed()
    {
        Instance.lights[Instance.numCorrect].GetComponent<MeshRenderer>().material = Resources.Load("Materials/Lights/GreenLight") as Material;
        Instance.numCorrect++;
        // open door?
        if (Instance.numCorrect == 4)
        {
            Debug.Log("raisin da dor");
            Instance.door.transform.Translate(0, 0, 5);
        }
    }

}
