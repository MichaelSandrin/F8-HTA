using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSight : MonoBehaviour
{

    public float DistanceToPlayer = 5.0f; // distance between camera and character
    public Material TransparentMaterial = null; //transparent material is set to nothing
    public float FadeInTimeout = 0.6f; //time to fade in
    public float FadeOutTimeout = 0.2f; //time to fade out
    public float TargetTransparency = 0.01f; //tranparet material transparency level from 0.0 to 1.0 being 0 to 100%

    void Update()
    {
        RaycastHit[] hits; // store object the raycast hits in an array called hits

         

        hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer);
        //a raycast with parameters for camera position, the cameras forward vector being where it should look,
        //and the distance the ray should be cast

        foreach (RaycastHit hit in hits) //for each object hit by the cast
        {
            Renderer R = hit.collider.GetComponent<Renderer>(); //find its renderer
            
            if (R == null) //if there is no renderer
            {
                continue; //skip to next object
            }
           
            //call the auto transparent sricpt 
            AutoTransparent AT = R.GetComponent<AutoTransparent>();

            if (AT == null) // if no script is attached, attach one
            {
                AT = R.gameObject.AddComponent<AutoTransparent>();

                //set the values for the script, the material type
                AT.TransparentMaterial = TransparentMaterial;
                //fade in time
                AT.FadeInTimeout = FadeInTimeout;
                //fade out time
                AT.FadeOutTimeout = FadeOutTimeout;
                //amount of transparency
                AT.TargetTransparency = TargetTransparency;
            }

            AT.BeTransparent(); // get called every frame to reset the falloff, tells which objects to be transparent
        }
        

        
    }
}

