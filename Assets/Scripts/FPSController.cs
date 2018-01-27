using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour {
	[SerializeField]
	protected Camera camera;
	[Range(0,1)]
	public float moveSpeed = 1;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/* === mouse look camera rotation === */
		transform.Rotate (new Vector3(0, Input.GetAxisRaw("Mouse X"),0));
		Quaternion yrot = camera.transform.localRotation;
		Vector3 lrangles = yrot.eulerAngles;
		lrangles.x -= Input.GetAxisRaw ("Mouse Y");
		camera.transform.localRotation = Quaternion.Euler (lrangles);
		Debug.DrawLine (transform.position, transform.position + transform.forward, Color.red);
	}

	void FixedUpdate() {
		Vector3 vertical = (transform.forward) * Input.GetAxisRaw("Vertical");
		Vector3 horizontal = (transform.right) * Input.GetAxisRaw("Horizontal");
		Vector3 movement = (vertical + horizontal).normalized * moveSpeed;
		Debug.Log (movement.magnitude);
//		transform.position += movement;
		if(movement.magnitude != 0.0f)
			Debug.Log(movement);
//		Vector3 movement = (new Vector3 (Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"))).normalized * moveSpeed;
		transform.position = Vector3.MoveTowards (transform.position, transform.position + (movement), moveSpeed);
	}
}
