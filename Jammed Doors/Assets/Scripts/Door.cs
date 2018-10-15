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

    [SerializeField]
    private NavMeshObstacle obstacle;
    [SerializeField]
    private Transform mesh;
    public UnityEvent OnDoorOpen;
    public UnityEvent OnMonsterDoorOpen;

    public void Open()
    {
        mesh.gameObject.SetActive(false);
        obstacle.enabled = false;
        state = doorState.Open;
        OnDoorOpen.Invoke();
    }
    public void MonsterBreak()
    {
        mesh.gameObject.SetActive(false);
        obstacle.enabled = false;
        state = doorState.Broken;
        OnMonsterDoorOpen.Invoke();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
