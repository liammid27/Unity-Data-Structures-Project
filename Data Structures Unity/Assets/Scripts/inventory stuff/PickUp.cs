using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Item tempItem;
    public GameObject tempObj;
    public bool nearMicro;

    private void OnTriggerEnter(Collider other)
    {
        //Gathers object infromationn that is picked up in the command script
        if (other.gameObject.name == "Food")
        {
            //Allows players to pick up food
            tempItem = new Item(1, "food", "can be warmed and eaten");
            tempObj = new GameObject();
            tempObj = other.gameObject;
            
        }
        else if (other.gameObject.name == "Spoon")
        {
            //Allows players to pick up spoon
            tempItem = new Item(2, "spoon", "used to eat food");
            tempObj = new GameObject();
            tempObj = other.gameObject;
        }
        else if (other.gameObject.name == "Microwave")
        {
            //Allows players to interact with Microwave
            nearMicro = true;
            tempObj = new GameObject();
            tempObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Diables all these features if play leaves object collider
        if (other.gameObject.name == "Food" || other.gameObject.name == "Spoon")
        {

            tempItem = new Item(0, "empty", "nothing in inventory");
            
        }
        else if (other.gameObject.name == "Microwave")
        {
            nearMicro = false;
            
        }
    }
}
