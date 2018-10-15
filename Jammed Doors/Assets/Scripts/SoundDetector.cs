using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SoundEvent : UnityEvent<SoundData> { }
public class SoundDetector : MonoBehaviour {
    public float HearingRange = 5f;
    public SoundEvent OnSoundHeard;
    // Use this for initialization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, HearingRange);
      

    }
    void Start () {
        OnSoundHeard = new SoundEvent();
        OnSoundHeard.AddListener(gameObject.GetComponent<Monster>().OnSoundHeard);
        

    }
	public void Hearing(Vector3 pos, float range)
    {
        float dist = Vector3.Distance(pos, transform.position);
        if (dist  < HearingRange+ range)
        {

            Debug.Log("I heard THat "+ ( (HearingRange + range) - dist).ToString());
            OnSoundHeard.Invoke(new SoundData(pos, (HearingRange + range) - dist));
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
