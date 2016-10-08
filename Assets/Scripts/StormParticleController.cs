using UnityEngine;
using System.Collections;

public class StormParticleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 110)
			Destroy (gameObject);
	}
}
