using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransparent : MonoBehaviour
{
    //an array to hold the old material information to reapply later
    private Material[] oldMaterials = null;

    //how transparent the material needs to be
    private float m_Transparency = 1.0f;

    //the following get the current values then sets it to the transparent version
    public float TargetTransparency { get; set; }

    public float FadeInTimeout { get; set; }

    public float FadeOutTimeout { get; set; }

    public Material TransparentMaterial { get; set; }

    //boolean for to say if a material should be transparent
    private bool shouldBeTransparent = true;

    //sets boolean for transparentcy to be true
    public void BeTransparent()
    {
        shouldBeTransparent = true;
    }

    private void Start()
    {
        // reset the transparency;
        m_Transparency = 1.0f;
        //if there are no old materials
        if (oldMaterials == null)
        {
            // Save the current materials
            oldMaterials = GetComponent<Renderer>().materials;

            Material[] materialsList = new Material[oldMaterials.Length];

            for (int i = 0; i < materialsList.Length; i++)
            {
                // repalce material with transparent
                materialsList[i] = Object.Instantiate(TransparentMaterial);
                materialsList[i].SetColor("_Color", oldMaterials[i].GetColor("_Color"));
            }

            // make transparent
            GetComponent<Renderer>().materials = materialsList;
        }
    }

   
    private void Update()
    { //if the object shouldn't be transparent destroy the transparent material
        if (!shouldBeTransparent && m_Transparency >= 1.0)
        {
            Destroy(this);
        }

        //Are we fading in our out?
        if (shouldBeTransparent)
        {
            //Fading out
            if (m_Transparency > TargetTransparency)
            {
                m_Transparency -= ((1.0f - TargetTransparency) * Time.deltaTime) / FadeOutTimeout;
            }
        }
        else
        {
            //Fading in
            m_Transparency += ((1.0f - TargetTransparency) * Time.deltaTime) / FadeInTimeout;
        }

        Material[] materialsList = GetComponent<Renderer>().materials;
        for (int i = 0; i < materialsList.Length; i++)
        {
            Color C = oldMaterials[i].GetColor("_Color");

            C.a = m_Transparency;
            materialsList[i].color = C;
        }

        //The object will start to become visible again if BeTransparent() is not called
        shouldBeTransparent = false;
    }

    private void OnDestroy()
    {
        // restore old materials
        GetComponent<Renderer>().materials = oldMaterials;
    }
}
