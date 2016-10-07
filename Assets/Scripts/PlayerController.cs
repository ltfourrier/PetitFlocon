using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float range;
	public float hitSpeed;

	private float hAxis, vAxis;
	private Vector3 newPosition, movement;
	private Vector2 actionRange;
	private float timer;
	private bool acting;
	private float newZ;
	private RaycastHit2D raycast;
	private Animator animator;
	private BoxCollider2D collider;


	void Start () {
		animator = GetComponent<Animator> ();
		collider = GetComponent<BoxCollider2D> ();
		timer = 0;
	}


	void Update () {
		if (Input.GetButton ("Action") && (raycast = Physics2D.Linecast (collider.transform.position, actionRange, 1 << LayerMask.NameToLayer ("Resource")))) {
			
				acting = true;
		} else {
			acting = false;
			if (timer != 0)
				timer = 0;
		}
		
		newZ = transform.position.y / 1000f;
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}


	void FixedUpdate () {
		if (!acting) {
			hAxis = Input.GetAxis ("Horizontal");
			vAxis = Input.GetAxis ("Vertical");

			if (hAxis == 0 && vAxis == 0)
			if (animator.GetBool ("Moving"))
				animator.SetBool ("Moving", false);

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

		} else {
			if (animator.GetBool ("Moving"))
				animator.SetBool ("Moving", false);
			
			timer += Time.fixedDeltaTime;
			if (timer > hitSpeed) {
				raycast.collider.gameObject.GetComponent<ResourceController> ().Hit ();
				timer = 0;
			}
		}
	}


	void Move(int direction)
	{
		animator.SetBool ("Moving", true);
		switch (direction) {
		case 1: // down
			animator.SetInteger ("Direction", 1);
			actionRange.Set (collider.bounds.center.x, collider.bounds.min.y - range);
			break;
		case 2: // left
			animator.SetInteger ("Direction", 2);
			actionRange.Set (collider.bounds.min.x - range, collider.bounds.center.y);
			break;
		case 3: // up
			animator.SetInteger ("Direction", 3);
			actionRange.Set (collider.bounds.center.x, collider.bounds.max.y + range);
			break;
		case 4: // right
			animator.SetInteger ("Direction", 4);
			actionRange.Set (collider.bounds.max.x + range, collider.bounds.center.y);
			break;
		}
	}
}
