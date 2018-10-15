using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour {
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;
    public string tag; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag)
        {
            triggerEnter.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tag)
        {
            triggerExit.Invoke();
        }
    }
}
