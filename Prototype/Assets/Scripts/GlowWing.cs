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
        glideColor = GameObject.Find("Plume").GetComponent<CharacterMovement>().glideEndurance;
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

       // float emission = Mathf.PingPong(Time.time, 1.0f);
        Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(glideColor);

        mat.SetColor("_EmissionColor", finalColor);

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
