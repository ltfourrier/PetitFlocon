using UnityEngine;
using System.Collections;

public class UnloadBehaviour : MonoBehaviour {

	private Transform cam;
	private Collider coll;
	private Renderer rend;
	private float timer;
	private float updateRate;
	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;
		coll = GetComponent<Collider> ();
		rend = GetComponent<Renderer> ();
		updateRate = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.fixedDeltaTime;
		if (timer <= 0){
			if (Vector3.Distance (transform.position, cam.position) > 200) {
				if (coll)
					coll.enabled = false;
				if (rend)
					rend.enabled = false;
			} else {
				if (coll)
					coll.enabled = true;
				if (rend)
					rend.enabled = true;
			}
			timer = updateRate;
		}
			
	}
}
