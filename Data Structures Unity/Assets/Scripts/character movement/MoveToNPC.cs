using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToNPC : MonoBehaviour
{

    public float distance;

    public Transform target;

    public NavMeshAgent Agent;

    public bool canWalk;

    public bool canTalk;


    public void Update()
    {

        distance = (Vector3.Distance(target.transform.position, Agent.transform.position));

        if(distance >= 5)
        {
            canWalk = true;

            canTalk = false;

            Agent.SetDestination(target.position);          
        }

        if (distance < 5)
        {

            canWalk = false;

            //canTalk = true;

            Agent.ResetPath();
        }

    }

    public void MoveTo(string npcName)
    {
        findNPC(npcName);

        Agent.SetDestination(target.localPosition);
    }

    public void findNPC(string npcName)
    {
        if (npcName == "Max")
        {
             target = GameObject.Find(npcName).transform;
        }
        if (npcName == "Carter")
        {
            target = GameObject.Find(npcName).transform;
        }
        if (npcName == "Eva")
        {
            target = GameObject.Find(npcName).transform;
        }    
    }

}
