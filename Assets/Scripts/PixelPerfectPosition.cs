using UnityEngine;
using System.Collections;

public class PixelPerfectPosition : MonoBehaviour {

	private Vector3 roundedPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		roundedPosition.Set(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
		transform.position = roundedPosition;
	}
}
