using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory 
{
    public Vector3 location;
 

    public Factory(GameObject NPCType, int _amount, NPCSpawner loc)
    {
        CreateObjects(NPCType, _amount, loc);
    }

    void CreateObjects(GameObject NPCType, int _amount, NPCSpawner loc)
    {
        location = loc.gameObject.GetComponent<NPCSpawner>().transform.position;

        for (int i = 0; i < _amount; i++)
        {
            GameObject.Instantiate(NPCType, new Vector3(location.x, location.y, location.z), Quaternion.identity);
        }
    }
}
