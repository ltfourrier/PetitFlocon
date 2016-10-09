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
	private bool isPlaceable;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		backpack = GetComponentInParent<Backpack> ();
		isPlaceable = true;
	}
	
	// Update is called once per frame
	void Update () {
		roundedPosition.Set(Mathf.Round(transform.position.x / 8) * 8, Mathf.Round(transform.position.y / 8) * 8, transform.position.z);
		transform.position = roundedPosition;
		rend.sortingOrder = Mathf.RoundToInt (-transform.position.y / 8);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer != LayerMask.NameToLayer("Weather")){
			rend.enabled = false;
			isPlaceable = false;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.layer != LayerMask.NameToLayer ("Weather")) {
			rend.enabled = true;
			isPlaceable = true;
		}
	}

	public void PlaceConstruction(){
		if (isPlaceable && backpack.resources [type] >= cost) {
			GameObject construction = Instantiate (constructionPrefab);
			construction.transform.position = transform.position;
			backpack.resources [type] -= cost;
		}
	}

	private void setPlaceable(bool b){
		if (b = false || backpack.resources [type] < cost) {
			isPlaceable = false;
			rend.enabled = false;
		} else {
			isPlaceable = true;
			rend.enabled = false;
		}
	}
}