using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform player;
    [Range(0f,1f)] public float lerpFactor = 0.9f;

    public Vector3 topRight;
    public Vector3 bottomLeft;

    private float camHeight;
    private float camWidth;
	private float originalZ;
    private Vector3 clampedPosition;
    private Vector3 playerProjection;
    //private Vector3 offsetPosition;

    /*void OnGUI ()
    {
        if (GUI.Button (new Rect (5,5,30,20), "x1"))
        {
            Screen.SetResolution(512, 288, false);
        }
        if (GUI.Button (new Rect (35,5,30,20), "x2"))
        {
            Screen.SetResolution(1024, 576, false);
        }
        if (GUI.Button (new Rect (65,5,30,20), "x3"))
        {
            Screen.SetResolution(1536, 864, false);
        }
    }*/

	void Start () 
    {
		originalZ = transform.position.z;
        transform.position = new Vector3(player.position.x, player.position.y, originalZ);
        playerProjection = new Vector3();
        //offsetPosition = Vector3.right;
        Application.targetFrameRate = 60;
        camHeight = Camera.main.orthographicSize;
        camWidth = Camera.main.aspect * camHeight;
        //Screen.SetResolution(640, 576, false);
		Time.timeScale = 1f;
	}

    void Update()
    {
        
        //Debug.Log(transform.position.x);
    }
	
	void FixedUpdate () 
    {
		playerProjection.Set(player.position.x, player.position.y, originalZ);
        transform.position = Vector3.Lerp(playerProjection, transform.position, lerpFactor);


        clampedPosition.Set
        (
            Mathf.Clamp(transform.position.x, bottomLeft.x + camWidth, topRight.x - camWidth), 
            Mathf.Clamp(transform.position.y, bottomLeft.y + camHeight, topRight.y - camHeight),
            transform.position.z
        );

        transform.position = clampedPosition;
	}
}
