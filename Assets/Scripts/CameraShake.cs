using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
   private Quaternion originRotation;
   private float shake_decay;
   private float shake_intensity;
	public bool rotational;
 
    void Start()
    {
        originRotation = transform.rotation;
    }
 
   void Update ()
    {
        if (shake_intensity > 0)
        {
			if (rotational) {
				transform.rotation = new Quaternion (
					originRotation.x + Random.Range (-shake_intensity, shake_intensity) * .2f,
					originRotation.y + Random.Range (-shake_intensity, shake_intensity) * .2f,
					originRotation.z + Random.Range (-shake_intensity, shake_intensity) * .2f,
					originRotation.w + Random.Range (-shake_intensity, shake_intensity) * .2f);
			} else {
				// ONLY WORKS IF CAMERA CAN MOVE BACK TO CORRECT POSITION SOMEWHERE ELSE IN CODE
				transform.position = new Vector3 (
					transform.position.x + Random.Range (-shake_intensity, shake_intensity) * 10f,
					transform.position.y + Random.Range (-shake_intensity, shake_intensity) * 10f,
					transform.position.z + Random.Range (-shake_intensity, shake_intensity) * 10f);
			}
			
            shake_intensity -= shake_decay;
        }
        else
            transform.rotation = originRotation;
   }
 
    public void shake(float intensity, float decay){
      shake_intensity = intensity / 100;
      shake_decay = decay / 1000;
   }
}