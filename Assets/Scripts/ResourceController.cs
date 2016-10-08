using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	public int hitPoint;
	public GameObject resourcePrefab;
	public int resourceAmount;
	public float spawnRange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit(){
		hitPoint--;
		Camera.main.GetComponent<CameraShake> ().shake (10f, 4f);
		if (hitPoint <= 0) {
			for (int i = 0; i < resourceAmount; i++) {
				Instantiate (resourcePrefab, randomVector(), transform.rotation);
			}
			Destroy (gameObject);
		}
	}

	private Vector3 randomVector(){
		return new Vector3 (
			transform.position.x + Random.Range (-spawnRange, spawnRange),
			transform.position.y + Random.Range (-spawnRange, spawnRange),
			transform.position.z);
	}
}
