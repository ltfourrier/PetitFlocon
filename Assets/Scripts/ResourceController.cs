using UnityEngine;
using System.Collections;

public class ResourceController : MonoBehaviour {

	public int hitPoint;
	public int maxHitPoint;
	public GameObject resourcePrefab;
	public int minResourceAmount;
	public int maxResourceAmount;
	public int dropOnHit;
	public float spawnRange;
	public Sprite remainsSprite;
	public AudioClip destroySound;
	private AudioSource audioSource;

	private int resourceAmount;

	// Use this for initialization
	void Start () {
		hitPoint = Random.Range (hitPoint, maxHitPoint +1);
		audioSource = GetComponent<AudioSource> ();
	}


	public void Hit(){
		audioSource.Play ();
		//AudioSource.PlayClipAtPoint (hitSound, transform.position);
		hitPoint--;
		for (int i = 0; i < Random.Range(0, dropOnHit+1); i++) {
			Instantiate (resourcePrefab, randomVector(), transform.rotation);
		}
		Camera.main.GetComponent<CameraShake> ().shake (25f, 10f);
		if (hitPoint <= 0) {
			audioSource.clip = destroySound;
			audioSource.Play ();
			resourceAmount = Random.Range (minResourceAmount, maxResourceAmount + 1);
			for (int i = 0; i < resourceAmount; i++) {
				Instantiate (resourcePrefab, randomVector(), transform.rotation);
			}
			GetComponent<SpriteRenderer> ().sprite = remainsSprite;
			GetComponent<BoxCollider2D> ().enabled = false;
			this.enabled = false;
		}
	}

	private Vector3 randomVector(){
		return new Vector3 (
			transform.position.x + Random.Range (-spawnRange, spawnRange),
			transform.position.y + Random.Range (-spawnRange, spawnRange),
			transform.position.z);
	}
}
