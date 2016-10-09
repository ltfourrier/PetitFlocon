using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float health;
	public float movementSpeed;
	public float range;
	public float hitSpeed;
	public GameObject blueprintPrefab;

	private Blueprint blueprint;
	private float hAxis, vAxis;
	private Vector3 newPosition, movement;
	[HideInInspector] public Vector2 actionRange; // determines the max distance resources can be mined
	private float timer;
	private bool acting, building; // player modes
	private RaycastHit2D raycast;
	private Animator animator;
	private BoxCollider2D coll;
	private SpriteRenderer sprite;
	private Backpack backpack;


	void Start () {
		animator = GetComponent<Animator> ();
		coll = GetComponent<BoxCollider2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		backpack = GetComponent<Backpack> ();
		blueprint = Instantiate (blueprintPrefab).GetComponent<Blueprint>();
		blueprint.transform.parent = transform;
		blueprint.gameObject.SetActive (false);
		timer = 0;
		animator.SetInteger ("Direction", 1);
	}


	void Update () {

		if (health <= 0) {
			GetComponent<GameOverController> ().EndGame ();
			enabled = false;
		}

		if (building && Input.GetButton ("Action")) { // exit building mode if action button is pressed
			building = false;
			Debug.Log ("Exiting building mode");
		}

		// if action button is pressed and a resource is in range to be mined, enter action mode
		if (Input.GetButton ("Action") && (raycast = Physics2D.Linecast (coll.bounds.center, actionRange, 1 << LayerMask.NameToLayer ("Resource")))) {
				acting = true;
		} else {
			// enter movement mode
			acting = false;
			/*if (timer != 0)
				timer = 0;*/

			if (Input.GetButtonDown ("Place")) {
				if (!building) {// if place button is pressed for the first time, enter build mode
					building = true;
					Debug.Log ("Entering building mode");
				}
				else { // if it's the second press, build shit
					blueprint.PlaceConstruction();
					Debug.Log ("PlaceConstruction called from PlayerController");
				}
			}
			if (blueprint.cost > backpack.resources [blueprint.type]) { // if the player cannot build
				building = false;
			}
				
		}
			
		sprite.sortingOrder = Mathf.RoundToInt (-transform.position.y / 8);
	}


	void FixedUpdate () {
		if (!acting) { // MOVEMENT MODE
			hAxis = Input.GetAxis ("Horizontal");
			vAxis = Input.GetAxis ("Vertical");

			if (hAxis == 0 && vAxis == 0)
			if (animator.GetInteger ("State") != 0)
				animator.SetInteger ("State", 0);

			if (hAxis > 0)
				Move (4);
			else if (hAxis < 0)
				Move (2);
			if (vAxis > 0)
				Move (3);
			else if (vAxis < 0)
				Move (1);


			movement = new Vector3 (hAxis * movementSpeed, vAxis * movementSpeed, 0);
			movement.Normalize ();
			newPosition = transform.position + movement;
			transform.position = newPosition;
			

		} else { // ACTION MODE
			if (animator.GetInteger ("State") != 2)
				animator.SetInteger ("State", 2);

			if (timer <= 0) {
				raycast.collider.gameObject.GetComponent<ResourceController> ().Hit ();
				timer = hitSpeed;
			}
		}
		if (timer > 0)
			timer -= Time.fixedDeltaTime;

		blueprint.transform.position = transform.position + ((Vector3)actionRange - coll.bounds.center).normalized * 13; // update position
		if (building) { // BUILDING MODE
			if (!blueprint.gameObject.activeSelf) {
				blueprint.gameObject.SetActive (true);
				//blueprint.setPlaceable (true); // activate blueprint
				Debug.Log ("Activated blueprint");
			}
			
		} else if (blueprint.gameObject.activeSelf) { // if out of building mode, deactivate.
			blueprint.setPlaceable (true);
			blueprint.gameObject.SetActive(false);
			Debug.Log ("Deactivated blueprint");
		}
	}


	void Move(int direction) // changes animation and action range depending on direction
	{
		if ( animator.GetInteger("State") != 1)
			animator.SetInteger ("State", 1);
		switch (direction) {
		case 1: // down
			animator.SetInteger ("Direction", 1);
			actionRange.Set (coll.bounds.center.x, coll.bounds.min.y - range);
			break;
		case 2: // left
			animator.SetInteger ("Direction", 2);
			actionRange.Set (coll.bounds.min.x - range, coll.bounds.center.y);
			break;
		case 3: // up
			animator.SetInteger ("Direction", 3);
			actionRange.Set (coll.bounds.center.x, coll.bounds.max.y + range);
			break;
		case 4: // right
			animator.SetInteger ("Direction", 4);
			actionRange.Set (coll.bounds.max.x + range, coll.bounds.center.y);
			break;
		}
	}
}
