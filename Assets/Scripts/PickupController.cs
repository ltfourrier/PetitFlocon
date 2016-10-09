using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

	public float magnetRadius;
	public float speed;
	public ResourceType type;
	private Transform player;
	private BoxCollider2D coll;
	private Backpack backpack;

	void Start () {
		player = GameObject.Find ("Player").transform;
	}
		
	void FixedUpdate() {
		if (Vector2.Distance(transform.position, player.position) < magnetRadius)
			transform.position = Vector2.MoveTowards (transform.position, player.position, speed);
		//transform.position = Vector2.Lerp (player.position, transform.position, 0.95f + Vector2.Distance(transform.position, player.position)/1000);
		//Debug.Log (Vector2.Distance(transform.position, player.position)/100);
	}

	void OnTriggerEnter2D(Collider2D other){
		if ((backpack = other.gameObject.GetComponent<Backpack> ()) != null) {
			backpack.addResource(type);
			Destroy (gameObject);
		}
	}
}
