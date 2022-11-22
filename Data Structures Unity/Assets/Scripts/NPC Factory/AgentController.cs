using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AgentController : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    GameObject[] goals;
    GameObject currentGoal = null;

    public TextMeshPro Text;
    public string[] fluffTextarray = { "Hey!", "Whats up", "How ya doing!", "Good morning", "Who are you?", "What you lookin at?", "Outta my way!", "Greetings!", "Balls!", "Bazinga!!" };
    public string currentText;
    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        goals = GameObject.FindGameObjectsWithTag("Goal");
        currentGoal = goals[Random.Range(0, goals.Length - 1)];

        Text.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, currentGoal.transform.position) < 5)
        {
            currentGoal = goals[Random.Range(0, goals.Length - 1)];
        }

        agent.SetDestination(currentGoal.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Text.gameObject.SetActive(true);
            int rand = Random.Range(0, 9);
            currentText = fluffTextarray[rand];

            Text.text = currentText;
            Debug.Log("text active");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Text.gameObject.SetActive(false);
            Debug.Log("text not active");
        }
    }
}
