using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	public int hitPoint;
	public int resourceAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(){
		hitPoint--;
		Camera.main.GetComponent<CameraShake> ().shake (10f, 3f);
		if (hitPoint <= 0)
			gameObject.SetActive (false);
	}
}
