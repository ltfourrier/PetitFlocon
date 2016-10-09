using UnityEngine;
using System.Collections.Generic;

public class Terrain : MonoBehaviour {

	public int blankPercentage;
	public List<Sprite> sprites;
	// Use this for initialization
	void Start () {
		if (Random.Range (0, 100) <= blankPercentage)
			GetComponent<SpriteRenderer> ().sprite = sprites [0];
		else
			GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range (0, sprites.Count)];
	}
}
