using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {

	public bool storm;
	public float maxStrenght;
	public GameObject StormParticlePrefab;

	public float nextTimer;
	public float durationTimer;
	public float strenght;

	private float frequency;
	private int density;
	private float speed;
	private float shakeIntensity;
	private float particleScale;
	private float damage;
	public Vector2 stormDirection;
	private float timer;
	private Rigidbody2D particle;
	private float spawnDelay;

	private Vector2 direction;
	private Vector2 parallel;
	private Vector2 stormOrigin;

	// Use this for initialization
	void Start () {
		timer = 0;
		spawnDelay = 1 / frequency;
		GetComponent<SpriteRenderer> ().enabled = false;
		parallel = Rotate(stormDirection, 45f);
		strenght = Random.Range (1, maxStrenght);
		GenerateStorm (strenght);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (storm) {
			durationTimer -= Time.fixedDeltaTime;
			if (durationTimer <= 0) {
				storm = false;
				strenght = Random.Range (1, maxStrenght);
				GenerateStorm (strenght);
			}
			stormOrigin = (Vector2)Camera.main.transform.position - stormDirection * 120;
			Camera.main.GetComponent<CameraShake> ().shake (shakeIntensity, 2f);

			timer += Time.fixedDeltaTime;
			if (timer >= spawnDelay) {
				for (int i = 0; i < density; i++) {
					particle = Instantiate (StormParticlePrefab).GetComponent<Rigidbody2D> ();
					particle.transform.parent = transform;
					particle.transform.position = Vector3.Lerp (stormOrigin - parallel * 100, stormOrigin + parallel * 100, Random.Range (0f, 1f));  //new Vector3 (-120, Random.Range (-150, 150), 0);
					direction = Vector3.Slerp (Rotate (stormDirection, 45f), Rotate (stormDirection, -45f), Random.Range (0f, 1f));
					particle.transform.localScale = transform.localScale * particleScale;
					particle.transform.rotation = Quaternion.FromToRotation (Vector3.up, direction);
					particle.AddForce (direction * speed * 1000);
					particle.GetComponent<StormParticleController> ().damage = damage;
				}
				timer = 0;
			}
		} else {
			nextTimer -= Time.fixedDeltaTime;
			if (nextTimer <= 0)
				storm = true;
		}
	}

	private void GenerateStorm(float strenghtFactor) {
		stormDirection = Rotate(Vector2.up, Random.Range(0f, 360f));
		parallel = Rotate(stormDirection, 90f);
		density = Mathf.RoundToInt(strenghtFactor * 1.5f);
		frequency = 1f + strenghtFactor * 3f;
		spawnDelay = 1 / frequency;
		speed = 10f + strenghtFactor;
		damage = 1f + strenghtFactor / 5;
		shakeIntensity = 2f + strenghtFactor * 3f;
		particleScale = 0.5f + strenghtFactor / 10;
		durationTimer = Random.Range(25f, 35f) - strenghtFactor * 2;
		nextTimer = Random.Range(25f, 35f) + strenghtFactor * strenghtFactor * 3;
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