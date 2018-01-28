using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamPipe : MonoBehaviour {
	public ParticleSystem emitter;
	float lastEmission;
	void Awake() {
		lastEmission = Time.time;
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			if(Time.time - lastEmission >= 10) {
				lastEmission = Time.time;
				emitter.Play();
			}
		}
	}
}
