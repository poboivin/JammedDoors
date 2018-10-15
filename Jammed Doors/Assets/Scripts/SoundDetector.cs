using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundDetector : MonoBehaviour {
    public float HearingRange = 5f;
    public UnityEvent OnSoundHeard;
    // Use this for initialization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HearingRange);
      

    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
