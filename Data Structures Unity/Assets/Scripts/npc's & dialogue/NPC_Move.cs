using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move : MonoBehaviour
{
    public Animator anim;
    public Transform target;
    public Transform targetback;
    
    public bool forward = true;
    public bool backward = false;
    public float speed;
    public bool hasRotate = false;
    public bool inRange;

    public float distance;

    public GameObject player;

    //Runs both methods on update
    public void Update()
    {
        if (!inRange)
        {
            MoveForward();
            MoveBackward();
        }
        
        if (inRange)
        {
            anim.SetBool("IsWalking", false);
        }

        playerRange();
        
    }

    //Allows the NPC to move forward
    public void MoveForward()
    {
        if (forward && !inRange)
        {
            //Sets the animation bool and moves the NPC forward and rotates them all when necessary
            anim.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            if (transform.position == target.position)
            {
                Rotate();
                hasRotate = true;
                
                
                anim.SetBool("IsWalking", false);
                StartCoroutine("Chill");
            }
            
        }
        
    }

    //Allows the NPC to move backward
    public void MoveBackward()
    {
        if (backward && !inRange)
        {
            //Sets the animation bool and moves the NPC forward and rotates them all when necessary
            anim.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, targetback.position, speed);
            if (transform.position == targetback.position)
            {
                Rotate();
                hasRotate = true;


                anim.SetBool("IsWalking", false);
                StartCoroutine("Chill2");
            }

        }

    }


    public void Rotate()
    {
        //Rotates the NPC 180 degrees when it hits a wall

        if (hasRotate == false)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    //Makes the NPC idle for 10 seconds before turning around and walking in the other direction
    public IEnumerator Chill()
    {
       yield return new WaitForSeconds(10f);
        hasRotate = false;
        if (forward == true && !inRange)
        {
            forward = false;
            backward = true;
        }
    }

    //Makes the NPC idle for 10 seconds before turning around and walking in the other direction
    public IEnumerator Chill2()
    {
        yield return new WaitForSeconds(10f);
        hasRotate = false;
        if (backward == true && !inRange)
        {
            backward = false;
           forward = true;
        }
    }

    public void playerRange()
    {
        distance = (Vector3.Distance(player.transform.position, transform.position));

        if (distance <= 5)
        {
            inRange = true;

            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));

            StopCoroutine("Chill2");
            StopCoroutine("Chill");

            anim.SetBool("IsWalking", false);
        }

        if (distance > 5)
        {
            inRange = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Player")
        {

            inRange = false;
        }
    }
}
