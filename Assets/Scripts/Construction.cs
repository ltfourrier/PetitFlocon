using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {

	public float health;
	public float automaticDecay; // in percent per frame
	public Sprite horizontal;
	public Sprite vertical;
	public Sprite dfault;

	private BoxCollider2D coll;
	private SpriteRenderer rend;
	private RaycastHit2D[] raycast;

	void Start () {
		coll = GetComponent<BoxCollider2D> ();
		rend = GetComponent<SpriteRenderer> ();
		raycast = new RaycastHit2D[4];

		Vector3 roundedPosition = Vector3.zero;
		roundedPosition.Set(Mathf.Round(transform.position.x / 8) * 8, Mathf.Round(transform.position.y / 8) * 8, transform.position.z);
		transform.position = roundedPosition;

		rend.sortingOrder = Mathf.RoundToInt (-transform.position.y / 8);

		TriggerUpdate (true);
	}

	void FixedUpdate(){
		health -= automaticDecay / 100;		
	}

	void Update() {
		if (health <= 0) {
			TriggerUpdate (true);
			Destroy (gameObject);
		}
	}

	void TriggerUpdate(bool updateNeighbours){

		raycast [0] = Physics2D.Linecast (coll.bounds.center + Vector3.down * 9, coll.bounds.center + Vector3.down * 10, 1 << LayerMask.NameToLayer ("Construction"));
		raycast [1] = Physics2D.Linecast (coll.bounds.center + Vector3.left * 9, coll.bounds.center + Vector3.left * 10, 1 << LayerMask.NameToLayer ("Construction"));
		raycast [2] = Physics2D.Linecast (coll.bounds.center + Vector3.up * 9, coll.bounds.center + Vector3.up * 10, 1 << LayerMask.NameToLayer ("Construction"));
		raycast [3] = Physics2D.Linecast (coll.bounds.center + Vector3.right * 9, coll.bounds.center + Vector3.right * 10, 1 << LayerMask.NameToLayer ("Construction"));

		if (updateNeighbours)
			foreach (RaycastHit2D r in raycast)
				if (r)
					r.collider.gameObject.GetComponent<Construction> ().TriggerUpdate (false);

		if (raycast [0] && raycast [2] && !raycast [1] && !raycast [3])
			rend.sprite = vertical;
		else if (!raycast [0] && !raycast [2] && raycast [1] && raycast [3])
			rend.sprite = horizontal;
		else
			rend.sprite = dfault;
	}
}
