using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissoveLerp : MonoBehaviour {
    Material mat;
    public float lerpTime = 1f;
    float currentLerpTime;
    public bool Lerp;
    // Use this for initialization
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update () {
        if (Lerp)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {

                Lerp = false;
            }
            float perc = LerpUtility.Smoother(currentLerpTime, lerpTime);
            mat.SetFloat("_DissolveAmount",1- perc);
        }
     
        
    }
}
