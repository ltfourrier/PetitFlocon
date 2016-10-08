using UnityEngine;
using System.Collections;

public class StormParticleController : MonoBehaviour {

	public float damage;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 110)
			Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		PlayerController player;
		if ((player = other.gameObject.GetComponent<PlayerController> ()) != null) {
			player.health -= damage;
			Destroy (gameObject);
		}
		Construction construction;
		if ((construction = other.gameObject.GetComponent<Construction> ()) != null) {
			construction.health -= damage;
			Destroy (gameObject);
		}
	}
}
