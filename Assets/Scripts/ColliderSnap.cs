using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof (BoxCollider2D))]

public class ColliderSnap : MonoBehaviour 
{
	public int tileSize;
    private Vector3 roundedPosition;
    private Vector3 roundedScale;

    void start()
    {

    }

	#if UNITY_EDITOR
	void Update () 
    {
		if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode) 
        {
			this.enabled = false;
		} 
        else 
        {
			roundedPosition.Set(Mathf.Round(transform.position.x / tileSize) * tileSize, Mathf.Round(transform.position.y / tileSize) * tileSize, transform.position.z);
            transform.position = roundedPosition;
            roundedScale.Set(Mathf.Round(transform.localScale.x), Mathf.Round(transform.localScale.y), transform.localScale.z);
            transform.localScale = roundedScale;
		}
	}
	#endif
}
