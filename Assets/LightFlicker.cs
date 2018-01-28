using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    float currentTime;
    float flickerFreq;
    bool flickering;

	float turnOffWeight = 0;

    void Start()
    {
        SetRandomFreq();
    }
    void Update()
    {
        currentTime += flickering ? 0 : Time.deltaTime;
        if (currentTime > flickerFreq)
        {
			currentTime = 0;
            StartCoroutine(FlickerOnAndOff());
        }
    }

    IEnumerator FlickerOnAndOff()
    {
        flickering = true;

        int flickerTimes = 45;
        WaitForSeconds wait = new WaitForSeconds(.09f);
        for (int i = 0; i < flickerTimes; i++)
        {
            TurnOffLight(Random.value <= .35f + turnOffWeight);

            yield return wait;
        }

		SetRandomFreq();
        TurnOffLight(true);
        flickering = false;
    }

    void TurnOffLight(bool on)
    {
		if(on)
		{
			turnOffWeight -= .05f;
		}
		else
		{
			turnOffWeight += .025f;
		}
        GetComponent<Light>().enabled = on;
    }

    void SetRandomFreq()
    {
		turnOffWeight = 0;
        currentTime = 0;
        flickerFreq = Random.Range(20, 30);
    }
}
