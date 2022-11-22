using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class ConsoleCommand : MonoBehaviour
{
    public inventory tempInv;                            //script references

    public Item tempItem;

    public PickUp tempPickUp;

    public Canvas ConsoleCanvas;

    public Text output;                                   //UI game assets

    public InputField inputter;

    public string tempText;                              //functionality variables

    public Hashtable commands = new Hashtable();         //commands hashtable

    public bool checkCanvas = false;

    public MoveToNPC tempNPC;

    public NavMeshAgent playerAgent;

    public Transform playerTarget;

    public ColliderManager tempCollide;

    public animationController animControl;

    public NPC_Move npcmover;

    public bool canWalk;

    public bool canTalk;

    void Start()
    {
        ConsoleCanvas.gameObject.SetActive(checkCanvas); //checkcnavas is bool to switch betwen canvas states

        tempInv = new inventory();

        output.text = "";                               //clear text box at game start

        commands.Add(1,"cook");                         //populate commands hashtable
        commands.Add(2,"look at");
        commands.Add(3,"pick up");
        commands.Add(4, "inventory");
        commands.Add(5, "eat");
        commands.Add(6, "walk to");
        commands.Add(7, "talk to");

        tempNPC.Agent = playerAgent;
    }

    void Update()
    {
        SwitchConsole();                              //open console when players presses keypad enter

        tempText = inputter.text;       

        if (Input.GetKeyDown(KeyCode.F2))            //execute command when player presses F2
        {
            Debug.Log("f2");

            ReadText(tempText);

            inputter.text = "";
        }       

        if(tempNPC.canWalk == false)
        {
            animControl.walker = false;

            npcmover.inRange = false;
        }
        if (tempNPC.canWalk == true)
        {
            animControl.walker = true;
        }
    }

    void ReadText(string args)
    {     

        if (args.Length < 4)
        {
            appendText("wdym?");
            return;
        }

        foreach (string keyword in commands.Values)
        {
            if (args.Contains(keyword))
            {

                if (keyword == "cook")
                {
                    //copyText = args.Substring(keyword.Length+1, args.Length - keyword.Length+1);

                    Cook(copyText(args, keyword));      
                   
                }
                if (keyword == "look at")
                {
                    LookAt(copyText(args, keyword));
                }
                if (keyword == "pick up")
                {
                    PickUp(copyText(args, keyword));
                }
                if(keyword == "inventory")
                {
                    inventory();
                }
                if(keyword == "eat")
                {
                    eat((copyText(args,keyword)));
                }
                if(keyword == "walk to")
                {
                    walkTo((copyText(args, keyword)));
                }
                if(keyword == "talk to")
                {
                    talkTo((copyText(args, keyword)));
                }

            }
            
        }
    }

    void appendText(string message)                            //simple function to add a new line of text in the console
    {
        output.text += "\n" + message;
    }

    string copyText(string sentence, string keyword)          //copies the word after the keyword and returns it
    {
        int copyLength = sentence.Length - keyword.Length -1;
        int startPos = keyword.Length + 1;

        return sentence.Substring(startPos, copyLength);       
    }

    void eat(string item)
    {

        bool canEat = false;                                                //check if the item being eaten is spaghetti

        foreach(Item _item in tempInv.itemList.Values)
        {
            if (_item.iName == item && (item == "spaghet"))
            {
                if (tempInv.itemList.Contains(2))                          //spoon's item ID =2
                {
                    canEat = true;
                }
                else
                {
                    appendText("you don't have a spoon :(");
                }
            }
        }

        if (canEat)
        {
            tempInv.removeItem(4);
            appendText("you just ate some "+ item +" my dude.");
        }
        else
        {
            appendText("you can't eat " + item);
        }
        
    }

    void inventory()
    {
        appendText("your inventory contains: ");
        foreach (Item _item in tempInv.itemList.Values)
        {           
            appendText(_item.iName);
        }
        if (tempInv.itemList.Count ==0)
        {
            appendText("nothing :(");
        }
    }

    void PickUp(string itemName)                                             //receives what string should be picked up and adds it to the hashtable
    {
        //DisplayName tempDisplay;

        if (tempInv.itemList.Contains(tempPickUp.tempItem.itemID))
        {
            appendText("you already have that in your inventory");
            return;
        }
        if (tempPickUp.tempItem.iName == itemName)
        {
            tempInv.itemList.Add(tempPickUp.tempItem.itemID, tempPickUp.tempItem);
            appendText("you added " + tempPickUp.tempItem.iName + " added to your inventory");

            Destroy(tempPickUp.tempObj.gameObject);

            GameObject tempOBJ = GameObject.Find(itemName +"text(Clone)");

            tempOBJ.SetActive(false);
            
}
        else
        {
            appendText("watchu tryin pick up fam? " + "I don't see any " + itemName + " around here?!");
        }
    }

    void Cook(string item)
    {

        bool checkFood = false;

        foreach (Item _item in tempInv.itemList.Values)
        {
            if (_item.iName == item)
            {
                if (_item.iName == "food")
                {

                    tempItem = _item;

                    checkFood = true;

                }
                else
                {
                    appendText("you can't cook a "+ item +" brooo!");
                    checkFood = false;
                }
            }
                
        }

        if (checkFood)
        {
            tempInv.itemList.Remove(tempItem.itemID);                                       //used to be inside foreach **

            appendText(tempItem.iName + " was removed from your inventory!");

            appendText("that food sure is cookin up boi!");

            tempItem = new Item(4, "spaghet", "this smell good ass hell, dayum boi!!!");

            tempInv.itemList.Add(tempItem.itemID, tempItem);

            appendText(tempItem.iName + " was added to your inventory!");

            Debug.Log("foodie");
        }
        
    }

    void LookAt(string item)
    {

        bool inInv = false;
        
        foreach (Item _item in tempInv.itemList.Values)
        {
            if (_item.iName == item)
            {
                inInv = true;
                appendText(" ");
                appendText("you are looking at" + _item.iName + " rn");
                appendText(_item.iDescription);
            }
        }

        if (!inInv)
        {
            appendText("You don't seem to have a "+ item +" in your inventory bruv");
        }
    }

    void walkTo(string npc)
    {
        
        tempNPC.findNPC(npc);

        tempNPC.Update();

        animControl.walker = true;

    }

    void talkTo(string npc)
    {       

        if(npc == "" || npc == " ")
        {
            appendText("please write a full name");
        }

        if (npc == "Carter" || npc == "Max" || npc == "Eva")
        {

            Debug.Log(tempNPC.distance);

            if (tempNPC.distance <= 5) 
            {
                if (!tempNPC.canWalk)
                {
                    tempNPC.canTalk = true;



                    animControl.talker = true;
                }
            }
            
        }     
      
    }

    void SwitchConsole()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (checkCanvas == false)
            {
                checkCanvas = true;
            }
            else checkCanvas = false;

            ConsoleCanvas.gameObject.SetActive(checkCanvas);
        }
    }

}
    


