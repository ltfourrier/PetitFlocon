using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ResourceType {wood};

public class Backpack : MonoBehaviour {

	public Text woodAmount;

	public Dictionary<ResourceType, int> resources;
	// Use this for initialization
	void Start () {
		resources = new Dictionary<ResourceType, int> () {{ResourceType.wood, 0}};
	}
	
	// Update is called once per frame
	void Update () {
		woodAmount.text = resources [ResourceType.wood].ToString();
	}
}
