using System.Collections;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    //Declaring all variables for the intro dialogue
    public TextAsset inkJSON;
    private Story story;
    public bool isOver = false;
    public string endText;
    public bool talk = false;
    public float distance;

    public MoveToNPC tempNPC;
    public Text textUI;
    public Button ButtonUI;

    public GameObject player;
    public GameObject npcCur;

    //Again replaces linked list with doubly linked list
    DoublyLinkedList Dialogue = new DoublyLinkedList();

    public bool hasCollided = false;
    public bool isPossible = true;

    private void Start()
    {
        //starts the story and grabs end text
        story = new Story(inkJSON.text);

        endText = "Maybe someone else knows more about what happend";
        
    }

    private void collisionCheck()
    {

        distance = (Vector3.Distance(player.transform.position, npcCur.transform.position));

        //Checks if a collision is made and updates the UI with the relevant info
        if (isPossible) 
        {
            
            if (tempNPC.canTalk && (distance < 5))
            {
                talk = true;
                updateUI();
                isPossible = false;
            }
          
        }
        
    }

    private void Update()
    {
        //Updates when collsion is made
        collisionCheck();
    }

    public void updateUI()
    {
        //Clears UI
        resetUI();

        //Spawns UI text and loads the next line/s of text
        Text storyText = Instantiate(textUI) as Text;
        string text = LoadNext();

        //Adds the speakers name before the text
        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            text = tags[0] + " - " + text;
        }

        //Makes the UI text equal to current story text
        storyText.text = text;

        storyText.transform.SetParent(this.transform, false);

        //Checks if the dialogue is over and displays the final line of text
        if (isOver == true)
        {
            storyText.text = endText;

        }

        //Instantiates button prefabs and fills them with the relevant info
        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(ButtonUI) as Button;
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;
            choiceButton.transform.SetParent(this.transform, false);


            if (choiceText.text == "End Conversation")
            {
                talk = false;
                Debug.Log("Yes End Convo");
                isOver = true;
            }
            choiceButton.onClick.AddListener(delegate
            {
                choiceSelect(choice);
            } );
        }
    }

    //Deletes the current UI, allowing it to be reprinted
    void resetUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    //Links the choice chosen to the JSON file and index of choices
    void choiceSelect(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        updateUI();

    }

    //Continues the story and loads the next Line/s of dialogue
    string LoadNext()
    {
        string text = "";
        string prevtext = "Null";

        if (prevtext == text)
        {
            Debug.Log("Yes Prev text");
        }

        if (story.canContinue)
        {
            //Linked list is filled with the next dialogue and choices
            Dialogue.AddLast(story.ContinueMaximally());

            //The UI texted is changed based on the top most node in the linked list
            foreach (Node node in Dialogue)
            {
                text = node.Dialogue.ToString();
                prevtext = text;
            }

        }

        return text;

    }
}


   
