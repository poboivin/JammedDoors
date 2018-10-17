using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float RangeSmall;
    public float RangeBig;
    public float interactRange = 1;
    public GameObject audioCam;
    static GameObject staticAudioCam;
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RangeSmall);
        Gizmos.DrawWireSphere(transform.position, RangeBig);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, interactRange);
    }
    // Use this for initialization
    void Start () {
        staticAudioCam = audioCam;

    }
    public static void ToggleSoundCam()
    {
        staticAudioCam.SetActive(!staticAudioCam.activeSelf);
        
    }
	public static void ToggleFreezePlayer()
    {
        GameObject.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = !GameObject.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled;
        GameObject.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.HeadBob>().enabled = !GameObject.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.HeadBob>().enabled;

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            foreach(SoundDetector s in FindObjectsOfType<SoundDetector>())
            {
                s.Hearing(transform.position, RangeBig);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

           Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, interactRange);

            foreach(Collider c in colliders)
            {

                Door d = c.GetComponentInParent<Door>();
              //  Debug.Log("test"); 
                if (d)
                {
                    foreach (SoundDetector s in FindObjectsOfType<SoundDetector>())
                    {
                        s.Hearing(transform.position, RangeSmall);
                    }
                    d.TryToUnlock();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, interactRange);

            foreach (Collider c in colliders)
            {

                Door d = c.GetComponentInParent<Door>();
                //  Debug.Log("test"); 
                if (d)
                {
                    foreach (SoundDetector s in FindObjectsOfType<SoundDetector>())
                    {
                        s.Hearing(transform.position, RangeBig);
                    }
                    d.PlayerBreak();
                }
            }
        }
    }
}
