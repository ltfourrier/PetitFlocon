using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
   private Quaternion originRotation;
   private float shake_decay;
   private float shake_intensity;
 
    void Start()
    {
        originRotation = transform.rotation;
    }
 
   void Update ()
    {
        if (shake_intensity > 0)
        {
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
                originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
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