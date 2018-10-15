using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float RangeSmall;
    public float RangeBig;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RangeSmall);
        Gizmos.DrawWireSphere(transform.position, RangeBig);

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
