using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	public AudioClip buttonPressed;
	private AudioSource audioSource;
	private float timer;
	private bool launching;
	private SpriteRenderer rend;
	private Color newColor;
	// Use this for initialization
	void Start () {
		timer = 2.5f;
		audioSource = GetComponent<AudioSource> ();
		launching = false;
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Action")) {
			launching = true;
			audioSource.clip = buttonPressed;
			audioSource.loop = false;
			audioSource.Play ();
		}

		if (launching) {
			newColor = new Color(rend.color.r, rend.color.g, rend.color.b, rend.color.a - Time.deltaTime / 1.5f);
			rend.color = newColor;
			timer -= Time.deltaTime;
			if (timer <= 0) {
				SceneManager.LoadScene("Main");
			}
		}
	}
}
