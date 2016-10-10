using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public WeatherController weather;
	public Canvas canva;
	public Text dialog;
	public ResourceController rock;
	public ResourceController tree;
	public GameObject boundaries;

	private CameraController cam;
	private int step;
	private string dialogContent;
	private float timer;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
		cam = Camera.main.GetComponent<CameraController> ();
		cam.topRight = transform.position + new Vector3 (80, 72, 0);
		cam.bottomLeft = transform.position + new Vector3 (-80, -72, 0);
		step = 0;

		timer = 7f;
		rock.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.K)) {
			weather.GenerateStorm (1.5f);
			cam.topRight = transform.position;
			cam.bottomLeft = transform.position;
			Destroy (gameObject);
		}

		Step (step);
	}

	private void Step(int i){
		if (i == 0) {
			if (tree.hitPoint > 0) {
				dialogContent = "Maintain x in front of a tree to cut it down and collect logs.";
				dialog.text = dialogContent;
				canva.transform.position = tree.transform.position + Vector3.down * 20 + Vector3.right * 24;
				weather.nextTimer = 40f;
			} else {
				step = 1;
				rock.gameObject.SetActive (true);
				weather.GenerateStorm (1f);
				weather.nextTimer = 30f;
				weather.strenght = 1;
			}
		} else if (i == 1) {
			if (rock.hitPoint > 0) {
				dialogContent = "Collect gems by mining rocks. They represent your score!";
				dialog.text = dialogContent;
				canva.transform.position = rock.transform.position + Vector3.down * 20 + Vector3.left * 12;
				weather.nextTimer = 20f;
			} else
				step = 2;
		} else if (i == 2) {
			dialogContent = "Oh no! A storm is coming.\nPress c to enter build mode.";
			dialog.text = dialogContent;
			canva.transform.position = transform.position + Vector3.down * 64 + Vector3.left * 0;
			weather.nextTimer = 10f;
			if (Input.GetButtonDown ("Place")) { // TODO change condition
				step = 3;
			}
		} else if (i == 3) {
			dialogContent = "Now press c again to place a wall.\nThis consumes 10 logs.";
			dialog.text = dialogContent;
			weather.nextTimer = 10f;
			weather.durationTimer = 7f;
			if (Input.GetButtonDown ("Place")) { // TODO change condition
				step = 4;
			}
		} else if (i == 4) {
			dialogContent = "The arrow on top shows the direction of the wind.\nPrepare a cover!";
			dialog.text = dialogContent;
			weather.nextTimer = 10f;
			if (Input.GetButtonDown ("Place")) { // TODO change condition
				step = 5;
			}
		} else if (i == 5) {
			if (weather.storm) { // TODO change condition
				step = 6;
			}
		} else if (i == 6) {
			dialogContent = "Quick! Get in Cover! Wait until it stops.";
			dialog.text = dialogContent;
			if (!weather.storm) { // TODO change condition
				step = 7;
			}
		} else if (i == 7) {
			dialogContent = "If you get caught in the storm, you start losing health. Reach 0 and it's game over!";
			dialog.text = dialogContent;
			timer -= Time.deltaTime;
			if (timer <= 0) { // TODO change condition
				step = 8;
				timer = 20f;
			}
		} else if (i == 8) {
			dialogContent = "Next time press k to skip tutorial. Have fun!";
			dialog.text = dialogContent;
			cam.topRight = transform.position;
			cam.bottomLeft = transform.position;
			boundaries.SetActive (false);
			timer -= Time.deltaTime;
			if (timer <= 0) { // TODO change condition
				step = 9;
			}
		} else if (i == 9) {
			Destroy (gameObject);
		}
	}
}