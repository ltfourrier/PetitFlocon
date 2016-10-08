using UnityEngine;
using System.Collections.Generic;

public enum ResourceType {wood};

public class Backpack : MonoBehaviour {

	private GUIText woodAmount;

	public Dictionary<ResourceType, int> resources;
	// Use this for initialization
	void Start () {
		resources = new Dictionary<ResourceType, int> () {{ResourceType.wood, 0}};
		resources [ResourceType.wood]++;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
