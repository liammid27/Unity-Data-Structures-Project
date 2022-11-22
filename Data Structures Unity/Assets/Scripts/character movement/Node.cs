using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    //Gets and sets the string value of the node
    private string _dialogue;
    public string Dialogue
    {
        get { return _dialogue; }
        set { _dialogue = value; }
    }

    //Gets and sets the next node in the list of the node
    private Node _next;
    public Node Next
    {
        get { return _next; }
        set { _next = value; }
    }

    //Gets and sets the previous node in the list of the node
    private Node _previous;
    public Node Previous
    {
        get { return _previous; }
        set { _previous = value; }
    }

    //Constructs the Node
    public Node(string str)
    {
        Dialogue = str;
    }

}

