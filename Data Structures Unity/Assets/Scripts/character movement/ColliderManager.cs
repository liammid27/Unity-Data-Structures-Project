using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    public bool collided = false;

    public GameObject player;

    public bool inRange;

    public bool readyTalk;

    public float distance;


    public void playerRange()
    {
        distance = (Vector3.Distance(player.transform.position, transform.position));

        if (distance <= 5)
        {
            inRange = true;           

        }

        if (distance > 5)
        {
            inRange = false;
        }

    }
}

