using UnityEngine;
using System.Collections;

public class Blueprint : MonoBehaviour {

	private Vector3 roundedPosition;
	private Vector3 roundedScale;
	private SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		roundedPosition.Set(Mathf.Round(transform.position.x / 8) * 8, Mathf.Round(transform.position.y / 8) * 8, transform.position.z);
		transform.position = roundedPosition;
		rend.sortingOrder = Mathf.RoundToInt (-transform.position.y / 8);
	}

	void OnTriggerEnter2D(Collider2D other){
		rend.enabled = false;
	}
	void OnTriggerExit2D(Collider2D other){
		rend.enabled = true;
	}
}
