using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissoveLerp : MonoBehaviour {
    public List< Material> mats;
    public float lerpTime = 1f;
    float currentLerpTime;
    public bool Lerp;
    public LerpUtility.lerpMode lerpMode; 
    // Use this for initialization
    private void Start()
    {
        mats = new List<Material>();
        foreach( Renderer r in gameObject.GetComponentsInChildren<Renderer>())
        {
            mats.Add(r.material);
        }
        if (Lerp == false)
        {
            foreach (Material m in mats)
            {
                m.SetFloat("_DissolveAmount", 1);

            }
        }
       
    }
    public void StartLerp()
    {
        Lerp = true;
    }
    // Update is called once per frame
    void Update () {
        if (Lerp)
        {
            currentLerpTime += Time.deltaTime;
          
            float perc = LerpUtility.Lerp(currentLerpTime, lerpTime, lerpMode);

            foreach(Material m in mats)
            {
                m.SetFloat("_DissolveAmount", 1 - perc);

            }
            if (currentLerpTime > lerpTime)
            {

                Lerp = false;
                currentLerpTime = 0;
            }
        }
     
        
    }
}
