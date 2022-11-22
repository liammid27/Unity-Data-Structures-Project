using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogue : MonoBehaviour
{
    //Declaring all variables for the intro dialogue
    public TextAsset inkJSON;
    private Story story;
    public bool isOver = false;
    public string endText;


    public Text textUI;
    public Button ButtonUI;
    public MouseLook mouseLook;
    //public PlayerMove player;
    //Dialogue was changed from a single linked list to a custom doubly linked list
    DoublyLinkedList Dialogue = new DoublyLinkedList();

    void Start()
    {
        //starts the story and updates the UI
        story = new Story(inkJSON.text);
        
        endText = "I should go talk to Eva";

        updateUI();


    }

    void updateUI()
    {
        //Clears UI
        resetUI();

        //Checks if the player is done with dialogue and enables/disables movement
        if (isOver == true)
        {
            mouseLook.enabled = true;
            //player.enabled = true;
        }
        else
        {
            mouseLook.enabled = false;
            //player.enabled = false;
        }
        
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

            if (choiceText.text == "*Get up*")
            {
                isOver = true;
            }
            choiceButton.onClick.AddListener(delegate
            {
                choiceSelect(choice);
            });
        }

    }

    void resetUI()
    {
        //Deletes the current UI, allowing it to be reprinted
        for ( int i = 0; i < this.transform.childCount; i++)
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

        if ( story.canContinue)
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
