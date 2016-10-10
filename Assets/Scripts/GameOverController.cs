using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public AudioClip deathSound;
	public AudioClip jingle;
	public GameObject gameOverScreenPrefab;

	public AudioSource musicSource;

	private AudioSource soundSource;
	private Color newColor;
	private float timer;
	private int finalScore;
	private bool isGameOver = false;
	private GameObject spawn;
	private Text scoreDisplay;
	private Image splashScreen;

	public void EndGame(){
		isGameOver = true;
		timer = 2f;

		musicSource.clip = jingle;
		musicSource.loop = false;
		musicSource.Play ();

		soundSource = GetComponent<AudioSource> ();
		soundSource.clip = deathSound;
		soundSource.Play ();

		GetComponent<Animator> ().SetInteger ("State", 3);
		GetComponent<BoxCollider2D> ().enabled = false;

		spawn = Instantiate (gameOverScreenPrefab);
		splashScreen = spawn.GetComponentInChildren<Image> ();
		scoreDisplay = splashScreen.GetComponentInChildren<Text> ();
		finalScore = GetComponent<Backpack> ().resources [ResourceType.mineral];
		scoreDisplay.text = "Final score: " + finalScore.ToString () + "\nPress c to play again";//scoreString;

		GameObject.Find ("Weather Controller").SetActive (false);
		GameObject.Find ("UI").SetActive (false);

	}

	public void Update(){
		if (isGameOver) {
			if (timer <= 0) {
				newColor = new Color (splashScreen.color.r, splashScreen.color.g, splashScreen.color.b, splashScreen.color.a + Time.deltaTime / 3);
				splashScreen.color = newColor;
				newColor = new Color (scoreDisplay.color.r, scoreDisplay.color.g, scoreDisplay.color.b, scoreDisplay.color.a + Time.deltaTime / 3);
				scoreDisplay.color = newColor;
				if (Input.GetButton ("Place"))
					SceneManager.LoadScene ("Title");
			} else
				timer -= Time.deltaTime;
		}
	}
}
