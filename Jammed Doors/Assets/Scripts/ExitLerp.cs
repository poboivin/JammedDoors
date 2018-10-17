using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLerp : MonoBehaviour {
    public float delay = 0f;
    public float lerpTime = 1f;
    float currentLerpTime;
    public bool Lerp;
    public Vector3 start;
    public Vector3 end;
    public float amp= 1;
    public float phase = 1;
    // Use this for initialization
    void Start () {
		
	}
    public void setLerp()
    {
        end = this.transform.position;
        start = this.transform.localScale;
        Lerp = true;
    }
    // Update is called once per frame
    void Update () {
        if (Lerp)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > lerpTime)
                {
                    //currentLerpTime = 0;
                   // Lerp = false;
                }
            
                this.transform.localScale = LerpUtility.SineWave(currentLerpTime, start, end, amp, phase);

            }
        }
    }
}
