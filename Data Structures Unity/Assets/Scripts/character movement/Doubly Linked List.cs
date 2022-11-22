using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublyLinkedList : IEnumerable<Node>
{
    //Declares the first and last nodes in the list
    private Node front;
    public Node First
    {
        get { return front; }
    }

    private Node end;
    public Node Last
    {
        get { return end; }
    }

    //Declares the size of the Linked List
    public int Size
    {
        get;
        private set;
    }

    //Loops through the Linked List 
    public IEnumerator<Node> GetEnumerator()
    {
        Node current = front;
        while (current != null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    //Loops through the Linked List in reverse
    public IEnumerable GetEnumReverse()
    {
        Node current = end;
        while (current != null)
        {
            yield return current;
            current = current.Previous;
        }

    }

    //Adds a node to the end of the Linked List
    public void AddLast(string dialogue)
    {
        Node node = new Node(dialogue);
        if (end == null)
        {
            front = node;
        }
        else
        {
            node.Previous = end;
            end.Next = node;
        }

        end = node;
        Size++;
    }

   
    //Adds a node to the start of the linked list
    public void AddFirst(string dialogue)
    {
        Node node = new Node(dialogue);
        node.Next = front;

        if (front == null)
        {
            end = node;
        }
        else
        {
            front.Previous = node;
        }

        front = node;
        Size++;
    }

    //Removes the node at the front of the linked list
    public void RemoveFirst()
    {
        front = front.Next;
        
        if (front == null)
        {
            end = null;
        }
        Size--;
    }

    //Removes the node at the end of the linked list
    public void RemoveLast()
    {
        if (end != null)
        {
            end = end.Previous;
            if (end == null)
            {
                front = null;
            }

            Size--;
        }
    }


}
