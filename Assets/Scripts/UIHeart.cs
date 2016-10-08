using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIHeart : MonoBehaviour {

	public int minHealth;
	public PlayerController player;
	public List<Sprite> sprites;

	private int currentState;
	private Image img;
	// Use this for initialization
	void Start () {
		currentState = 0;
		img = GetComponent<Image> ();
	}

	void Update () {
		if (player.health >= minHealth + 80) {
			setState (0);
		} else if (player.health >= minHealth + 60) {
			setState (1);
		} else if (player.health >= minHealth + 40) {
			setState (2);
		} else if (player.health >= minHealth + 20) {
			setState (3);
		} else if (player.health > minHealth) {
			setState (4);
		} else {
			setState (5);
		}
	}

	private void setState(int i){
		if (currentState == i)
			return;

		img.sprite = sprites [i];
		currentState = i;
	}
}
