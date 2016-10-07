using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float range;

	private float hAxis, vAxis;
	private Vector3 newPosition, movement;
	private Vector2 actionRange;
	private RaycastHit2D raycast;
	private Animator animator;
	private BoxCollider2D collider;


	void Start () {
		animator = GetComponent<Animator> ();
		collider = GetComponent<BoxCollider2D> ();
	}


	/*void Update () {
		if (Input.GetButton("Action"))
		{
			if (raycast = Physics2D.Linecast (collider.transform.position, actionRange, 1 << LayerMask.NameToLayer ("Resource")))
				raycast.collider.
		}
	}*/


	void FixedUpdate () {
		hAxis = Input.GetAxis ("Horizontal");
		vAxis = Input.GetAxis ("Vertical");

		if (hAxis > 0)
			animator.SetInteger ("Direction", 4);
		else if (hAxis < 0)
			animator.SetInteger ("Direction", 2);
		if (vAxis > 0)
			animator.SetInteger ("Direction", 3);
		else if (vAxis < 0)
			animator.SetInteger ("Direction", 1);

		movement = new Vector3 (hAxis * movementSpeed, vAxis * movementSpeed, 0);
		movement.Normalize ();
		newPosition = transform.position + movement;
		transform.position = newPosition;
	}


	void setDirection(int direction)
	{
		switch (direction) {
		case 1:
			animator.SetInteger ("Direction", 1);
			actionRange.Set (collider.bounds.min.x - range, collider.bounds.center.y);
			break;
		}
	}
}
