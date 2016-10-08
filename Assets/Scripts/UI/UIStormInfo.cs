using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStormInfo : MonoBehaviour {

	public WeatherController weather;
	private Text txt;
	private string content;

	void Start () {
		txt = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!weather.storm)
			content = "Next:    " + Mathf.RoundToInt (weather.nextTimer).ToString ();
		else
			content = "WARNING! " + Mathf.RoundToInt (weather.strenght).ToString ();
		txt.text = content;
	}
}
