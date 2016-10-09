using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ResourceType {wood, mineral};

public class Backpack : MonoBehaviour {

	public Text woodAmount;
	public AudioClip woodSound;
	public Text mineralAmount;
	public AudioClip mineralSound;
	public Dictionary<ResourceType, int> resources;

	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		resources = new Dictionary<ResourceType, int> () {{ResourceType.wood, 0}, {ResourceType.mineral, 0}};
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		woodAmount.text = resources [ResourceType.wood].ToString();
		mineralAmount.text = resources [ResourceType.mineral].ToString();
	}

	public void addResource(ResourceType type) {
		resources [type]++;
		if (type == ResourceType.wood) {
			audioSource.clip = woodSound;
			audioSource.Play ();
		}
		if (type == ResourceType.mineral) {
			audioSource.clip = mineralSound;
			audioSource.Play ();
		}
	}
}
	
