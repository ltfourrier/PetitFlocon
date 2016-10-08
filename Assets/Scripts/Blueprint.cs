using UnityEngine;
using System.Collections;

public class Blueprint : MonoBehaviour {

	private Vector3 roundedPosition;
	private Vector3 roundedScale;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		roundedPosition.Set(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
		transform.position = roundedPosition;
	}
}
