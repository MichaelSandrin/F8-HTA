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
        glideColor = GameObject.Find("Plume").GetComponent<CharacterMovement>().glideLockoutTimer;

        Material mat = renderer.material;

        //float emission = Mathf.PingPong(Time.time, 1.2f);
        Color baseColor = Color.yellow; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(glideColor);

        mat.SetColor("_EmissionColor", finalColor);
    }

    void OnGUI()
    {
        string glideTime = glideColor.ToString();
        GUI.Label(new Rect(0, 0, 100, 50), glideTime);
    }
}
