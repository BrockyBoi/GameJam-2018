using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FPSController : NetworkBehaviour
{
    [SerializeField]
    protected Camera camera;
    [Range(1, 10)]
    public float moveSpeed = 5;
	bool isDead = false;

	void OnEnable()
	{	
		Player.EOnPlayerDeath += IsDead;
	}

	void OnDisable()
	{
		Player.EOnPlayerDeath -= IsDead;
	}

	void IsDead()
	{
		isDead = true;
	}

    // Update is called once per frame
    void Update()
    {
		if(isDead || PauseMenu.Paused)
			return;

        /* === mouse look camera rotation === */
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X"), 0));
        Quaternion yrot = camera.transform.localRotation;
        Vector3 lrangles = yrot.eulerAngles;
        lrangles.x -= Input.GetAxisRaw("Mouse Y");
        camera.transform.localRotation = Quaternion.Euler(lrangles);

        CheckButtonPress();
    }

    void CheckButtonPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var button = hit.collider.GetComponent<LockButton>();
                if (button)
                {
                    button.PressButton();
                }
            }
        }
    }

    void FixedUpdate()
    {
		if(isDead)
			return;

        Vector3 vertical = (transform.forward) * Input.GetAxisRaw("Vertical");
        Vector3 horizontal = (transform.right) * Input.GetAxisRaw("Horizontal");
        Vector3 movement = (vertical + horizontal).normalized * moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (movement), moveSpeed * Time.deltaTime);
    }
}
