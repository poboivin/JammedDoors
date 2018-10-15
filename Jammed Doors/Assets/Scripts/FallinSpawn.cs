using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinSpawn : MonoBehaviour {
    public float delay = 0f;
    public float lerpTime = 1f;
    float currentLerpTime;
    public bool Lerp;
    public float extraHeight;
    public Vector3 start;
    public Vector3 end;
    
    // Use this for initialization
    void Start () {
        end = this.transform.position;
        start = new Vector3(end.x, end.y + extraHeight, end.z);
        this.transform.position = start;
    }
    public void setLerp()
    {
        Lerp = true;
    }
	// Update is called once per frame
	void Update () {
        if (Lerp)
        {
            delay -= Time.deltaTime;
            if (delay<= 0)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > lerpTime)
                {

                    Lerp = false;
                }
                float perc = LerpUtility.Lerp(currentLerpTime, lerpTime, LerpUtility.lerpMode.Exponential);
                this.transform.position = Vector3.Lerp(start, end, perc);

            }
        }
    }
}
