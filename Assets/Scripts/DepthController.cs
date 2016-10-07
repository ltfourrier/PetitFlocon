using UnityEngine;
using System.Collections;

public class DepthController : MonoBehaviour {

	public bool alwaysUnder;
	private float newZ;
	// Use this for initialization
	void Start () {
		newZ = transform.position.y / 1000f;
		if (alwaysUnder) 
			newZ+=10;
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}
	
	// Update is called once per frame
	/*void Update () {
		newZ = (transform.position.y - player.transform.position.y) / 100f;
		gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}*/
}
