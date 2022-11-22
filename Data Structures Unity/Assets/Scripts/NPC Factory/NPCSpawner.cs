using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCprefab;
    public NPCSpawner loc;
    public int amount;

    // Start is called before the first frame update
    void Start()
    {
        Factory NPCFactory = new Factory(NPCprefab, amount, loc);
    }

  
}
