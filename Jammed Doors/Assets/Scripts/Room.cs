using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
    public List<Door> Doors;
    public Vector3 Scale;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, Scale);

    }
    public Door GetRandomDoor()
    {
        int index = Random.Range(0, Doors.Count - 1);
        return Doors[index];
    }
    public Door GetFirstClosedDoor()
    {
        foreach(Door d in Doors)
        {
            if(d.state == Door.doorState.Closed)
            {
                return d;
            }
        }
    return GetRandomDoor();
    }
    // Use this for initialization
    void Start () {
        Doors = new List<Door>();
        Collider[] colliders = Physics.OverlapBox(transform.position, Scale / 2);
        
        foreach (Collider c in colliders)
        {
           
            Door d = c.GetComponentInParent<Door>();
            if (d)
            {
                if (d.roomA == null)
                {
                    d.roomA = this;
                }
                else
                {
                    d.roomB = this;
                }
                Doors.Add(d);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
