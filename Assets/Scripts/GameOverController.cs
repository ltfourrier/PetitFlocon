using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public AudioClip deathSound;
	public AudioClip jingle;

	public AudioSource musicSource;

	private AudioSource soundSource;

	public void EndGame(){

		GetComponent<Animator> ().SetInteger ("State", 3);
		GetComponent<BoxCollider2D> ().enabled = false;

		musicSource.clip = jingle;
		musicSource.loop = false;
		musicSource.Play ();

		soundSource = GetComponent<AudioSource> ();
		soundSource.clip = deathSound;
		soundSource.Play ();

		GameObject.Find ("Weather Controller").SetActive (false);
	}
}
