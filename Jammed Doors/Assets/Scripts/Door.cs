using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Door : MonoBehaviour {
    public enum doorState
    {
        Closed,
        Open,
        Broken
    }
    public doorState state = doorState.Closed;
    public Room roomA;
    public Room roomB;
    public GameObject puzzle;
    [SerializeField]
    private NavMeshObstacle obstacle;
    [SerializeField]
    private Transform mesh;
    public UnityEvent OnDoorOpen;
    public UnityEvent OnMonsterDoorOpen;
    public float doorFlashTime = 0.3f;

    public void TryToUnlock()
    {
        Player.ToggleFreezePlayer();
        if (puzzle.activeSelf == true)
        {
            puzzle.SetActive(false);

        }
        else if (puzzle.activeSelf == false)
        {
            puzzle.SetActive(true);
          
        }
     
    }
    public void Open()
    {
        mesh.gameObject.SetActive(false);
        obstacle.enabled = false;
        state = doorState.Open;
        OnDoorOpen.Invoke();
    }
    public void PlayerBreak()
    {
        mesh.gameObject.SetActive(false);
        obstacle.enabled = false;
        state = doorState.Broken;
        OnMonsterDoorOpen.Invoke();
        Debug.Log("breaking door");
       // StartCoroutine(MonsterVisibility());
    }
    public void MonsterBreak()
    {
        mesh.gameObject.SetActive(false);
        obstacle.enabled = false;
        state = doorState.Broken;
        OnMonsterDoorOpen.Invoke();
        Debug.Log("breaking door");
        StartCoroutine(MonsterVisibility());
    }

    IEnumerator MonsterVisibility()
    {
        Player.ToggleSoundCam();
        yield return new WaitForSeconds(doorFlashTime);
        Player.ToggleSoundCam();
        yield return null;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
