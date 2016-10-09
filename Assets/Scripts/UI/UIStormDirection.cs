using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStormDirection : MonoBehaviour {

	public WeatherController weather;
	private Image img;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (weather.storm)
			img.enabled = false;
		else
			img.enabled = true;
		img.transform.rotation = Quaternion.FromToRotation (Vector3.up, weather.stormDirection);
	}
}
