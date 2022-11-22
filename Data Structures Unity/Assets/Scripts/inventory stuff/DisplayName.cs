using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    public GameObject floatingText;
    public GameObject _temptext;  
    public bool isNear = false;
 
    private void OnTriggerEnter(Collider other)
    {
        //Checks if player has collided with Object hitbox
        if (other.gameObject.name == "Player")
        {
            //Checks object name and sets the floating text to the relevant object
            if (floatingText.name == "Microwave Text")
            {
                _temptext = Instantiate(floatingText, transform.position, Quaternion.Euler(0, 90, 0));
            }
            else if (floatingText.name == "Spoon Text")
            {
                _temptext = Instantiate(floatingText, transform.position, Quaternion.Euler(0, -90, 0));
            }
            else
            {
                _temptext = Instantiate(floatingText, transform.position, Quaternion.identity);
            }

        }
    }

    //Removes the floating text when player leaves the collider
    private void OnTriggerExit(Collider other)
    {
        Destroy(_temptext);
    }

    
}
