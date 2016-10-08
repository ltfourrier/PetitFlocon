using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {

	public bool storm;
	public float stormTimer;
	public float frequency;
	public int density;
	public float speed;
	public Vector2 stormGeneralDirection;
	public GameObject StormParticlePrefab;

	private float timer;
	private Rigidbody2D particle;
	private float spawnDelay;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		timer = 0;
		spawnDelay = 1 / frequency;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (storm) {
			Camera.main.GetComponent<CameraShake> ().shake (5f + density, 2f);
			spawnDelay = 1 / frequency;
			timer += Time.fixedDeltaTime;
			if (timer >= spawnDelay) 
			{
				for (int i = 0; i < density; i++) {
					particle = Instantiate (StormParticlePrefab).GetComponent<Rigidbody2D> ();
					particle.transform.parent = transform;
					particle.transform.position = new Vector3 (-120, Random.Range (-150, 150), 0);
					direction = Vector3.Slerp(Rotate(stormGeneralDirection, 45f), Rotate(stormGeneralDirection, -45f), Random.Range(0f, 1f));
					particle.transform.rotation = Quaternion.FromToRotation (Vector3.up, direction);
					particle.AddForce (direction * speed * 1000);
				}
				timer = 0;
			}
		}
	}

	private Vector2 Rotate( Vector2 v, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}

}