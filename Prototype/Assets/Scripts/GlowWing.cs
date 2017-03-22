using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWing : MonoBehaviour {

    float glideColor;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        // float emission = Mathf.PingPong(Time.time, 1.0f);
        //Replace this with whatever you want for your base color at emission level '1'
        Color baseColor = Color.yellow;
        Color change = baseColor * Mathf.GammaToLinearSpace(glideColor);
        bool groundCheck = GameObject.Find("Plume").GetComponent<CharacterMovement>().wasGrounded;


        if (GameObject.Find("Plume").GetComponent<CharacterMovement>().gliding == true)
        {

            glideColor = GameObject.Find("Plume").GetComponent<CharacterMovement>().glideEndurance;
            // mat.SetColor("_Color", change);
            // mat.SetColor("_Color2", change);
            mat.SetFloat("_Glow", glideColor / 4);
        }
        if(groundCheck == true) {
            mat.SetFloat("_Glow", 0);
        }

        /*Settings for the glow, main Camera:
         Rendering Path: Deferred
         HDR should be on
         Add Bloom to main Camera:
         Intensity: 1.5
         Threshold: 1
         Blur Iterations: 1
         Sample Distance: .5
         */
    }
}
