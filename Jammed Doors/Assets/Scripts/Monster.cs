using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum OffMeshLinkMoveMethod
{
    Teleport,
    NormalSpeed,
    Parabola,
    Curve
}
public class SoundData
{
    public Vector3 Orgin;
    public float Decibel;

    public SoundData(Vector3 orgin, float decibel)
    {
        Orgin = orgin;
        Decibel = decibel;
    }
}
public class Monster : MonoBehaviour
{
  
    public enum MonsterState
    {
        Wander,
        Moving,
        Chase,
        Wait
    }
    [SerializeField]
    MonsterState state;
    [SerializeField]
    private Animator MyAnimator;
    [SerializeField]
    private NavMeshAgent myAgent;
    [SerializeField]
    private Vector3 destination;
    [SerializeField]
    private Map map;
   
    private bool OnOffLink = false;
    SoundData target;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(destination, Vector3.one);

    }
    public float waitTime = 6f;
    [SerializeField]

    private float waitTimer;
	// Update is called once per frame
	void Update ()
    {

        MyAnimator.SetFloat("velocity", myAgent.velocity.magnitude);
        switch (state)
        {
            case MonsterState.Wander:
                if(Vector3.Distance( myAgent.pathEndPosition,transform.position)<= myAgent.stoppingDistance)
                {
                    Room r = map.GetCurrentRoom(transform.position);
                    Door d = r.GetFirstClosedDoor();

                    if( r != d.roomA)
                    {
                        destination = d.roomA.transform.position;
                   //     Debug.Log("room b");
                    }
                    else
                    {
                        destination = d.roomB.transform.position;
                    //    Debug.Log("room b");
                    }
      
                    state = MonsterState.Wait;
                   

                }
                break;
            case MonsterState.Moving:

                break;
            case MonsterState.Chase:
                myAgent.destination = destination;
                if (Vector3.Distance(myAgent.pathEndPosition, transform.position) <= myAgent.stoppingDistance)
                {
                    if(destination != target.Orgin)
                    {
                        destination = target.Orgin;
                    }
                    else
                    {
                        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, 2);

                        foreach (Collider c in colliders)
                        {

                            Player d = c.GetComponentInParent<Player>();
                            //  Debug.Log("test"); 
                            if (d)
                            {
                                MyAnimator.SetTrigger("attack");
                                Debug.Log("Player dead");
                            }
                        }
                        state = MonsterState.Wait;
                    }
                   
                }
                break;
            case MonsterState.Wait:
                waitTimer -= Time.deltaTime;
                if(waitTimer <= 0)
                {
                    waitTimer = waitTime;
                    myAgent.destination = destination;
                    state = MonsterState.Wander;
                }
                break;
        }
    }


    public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
    public AnimationCurve curve = new AnimationCurve();
    void Start()
    {
        waitTimer = waitTime;
        Debug.Log(map.GetCurrentRoom(transform.position));
        destination = map.GetCurrentRoom(transform.position).transform.position;
        myAgent.destination = destination;
        StartCoroutine(OffNavMeshLinkFunc(myAgent));
       
    }
    public void OnSoundHeard(SoundData data)
    {


        if (state != MonsterState.Chase)
        {
            waitTimer = waitTime;
            destination = map.GetCurrentRoom(data.Orgin).transform.position;
            state = MonsterState.Chase;
            target = data;
        }
        else
        {
            if(target.Decibel < data.Decibel)
            {
                target = data;
                destination = map.GetCurrentRoom(data.Orgin).transform.position;
            }
        }

    }
    public IEnumerator OffNavMeshLinkFunc(NavMeshAgent agent)
    {
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                MyAnimator.SetTrigger("breakDoor");
               
                if (method == OffMeshLinkMoveMethod.NormalSpeed)
                    yield return StartCoroutine(NormalSpeed(agent));
                else if (method == OffMeshLinkMoveMethod.Parabola)
                    yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));
                else if (method == OffMeshLinkMoveMethod.Curve)
                    yield return StartCoroutine(Curve(agent, 0.5f));

              
                agent.CompleteOffMeshLink();
                agent.ResetPath();
                myAgent.SetDestination(destination);
            }
            yield return null;
        }
    }

    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
       
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float delay = 0.5f;
        bool once = true;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
             delay -= Time.deltaTime;
            MyAnimator.SetFloat("velocity", agent.speed);
            if (delay <= 0)
            {
                Door door = agent.currentOffMeshLinkData.offMeshLink.gameObject.GetComponent<Door>();

                if (door != null && once)
                {
                    door.MonsterBreak();
                    once = false;
                }
            }
            yield return null;
        }
    }
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            
            
            yield return null;
        }
       
    }
    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = curve.Evaluate(normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}
