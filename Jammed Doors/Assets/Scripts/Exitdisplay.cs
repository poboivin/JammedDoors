using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitdisplay : MonoBehaviour {
    public float DisplayTime = 20f;
    public float DisplayFor = 4f;
    public AudioSource source;
    public Renderer[] visual;

    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();
        visual = gameObject.GetComponents<Renderer>();
        StartCoroutine(exitPromt());
    }
	
    public IEnumerator exitPromt()
    {
        yield return new WaitForSeconds(DisplayTime);
        source.enabled = true;
        foreach(Renderer r in visual)
        {
            r.enabled = true;
        }

        yield return new WaitForSeconds(DisplayFor);
        source.enabled = false;
        foreach (Renderer r in visual)
        {
            r.enabled = false;
        }
        StartCoroutine(exitPromt());

        yield return null;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
