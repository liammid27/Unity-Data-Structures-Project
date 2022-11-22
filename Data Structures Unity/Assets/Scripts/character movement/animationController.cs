using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public AudioSource audioClip;

    public Animator animator;

    public bool walker;

    public bool talker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (walker)
        {
            animator.SetBool("walk", true);
            if (!audioClip.isPlaying)
            {
                audioClip.Play();
            }
            
        }

        if (!walker)
        {
            animator.SetBool("walk", false);
            audioClip.Stop();
        }

        if (talker)
        {
            animator.SetBool("talk", true);

        }

        if (!talker)
        {
            animator.SetBool("talk", false);
        }
    }


}
