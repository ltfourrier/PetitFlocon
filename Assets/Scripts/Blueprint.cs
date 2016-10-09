using UnityEngine;
using System.Collections;

public class Blueprint : MonoBehaviour {

	public GameObject constructionPrefab;
	public ResourceType type;
	public int cost;

	private Vector3 roundedPosition;
	private Vector3 roundedScale;
	private SpriteRenderer rend;
	private Backpack backpack;
	public bool isPlaceable;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		backpack = GetComponentInParent<Backpack> ();
		player = GetComponentInParent<PlayerController> ();
		isPlaceable = true;
	}
	
	// Update is called once per frame
	void Update () {
		roundedPosition.Set(Mathf.Round(transform.position.x / 8) * 8, Mathf.Round(transform.position.y / 8) * 8, transform.position.z);
		transform.position = roundedPosition;
		rend.sortingOrder = Mathf.RoundToInt (-transform.position.y / 8);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer != LayerMask.NameToLayer ("Weather")) {
			setPlaceable (false);
			Debug.Log ("Blueprint collided - deactivating");
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.layer != LayerMask.NameToLayer ("Weather")) {
			setPlaceable (true);
			Debug.Log ("Blueprint left collision - reactivating");
		}
	}

	public void PlaceConstruction(){
		if (isPlaceable && backpack.resources [type] >= cost) {
			Debug.Log ("Construction successfully placed");
			GameObject construction = Instantiate (constructionPrefab);
			construction.transform.position = transform.position;
			backpack.resources [type] -= cost;
		}
	}

	public void setPlaceable(bool b){
		if (!b) {
			isPlaceable = false;
			rend.enabled = false;
		} else {
			isPlaceable = true;
			rend.enabled = true;
		}
	}
}