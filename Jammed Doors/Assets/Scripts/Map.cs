using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public Room[] Rooms;

    public Room GetCurrentRoom(Vector3 pos)
    {
        Room closest = Rooms[0];
        float dist = Vector3.Distance(pos, closest.gameObject.transform.position);
        foreach(Room r in Rooms)
        {
            float newDist = Vector3.Distance(pos, r.gameObject.transform.position);
            if(newDist < dist)
            {
                closest = r;
                dist = newDist;
            }
        }

        return closest;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
