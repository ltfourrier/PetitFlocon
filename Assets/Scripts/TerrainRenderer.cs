using UnityEngine;
using System.Collections;

public class TerrainRenderer : MonoBehaviour {

	public int width, height; // in tiles
	public GameObject tilePrefab;
	private GameObject tile;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
		for(int x = 0; x < width; x++){
			for (int y = 0; y < height; y++){
				tile = Instantiate (tilePrefab);
				tile.transform.parent = transform;
				tile.transform.position = new Vector3 (transform.position.x + x * 8, transform.position.y + y * 8, transform.position.z);
			}
		}
	}
}
