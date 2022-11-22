using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory
{
    public void Main(string[] args)
    {
        
        /*Item key1 = new Item(1, "key1", "can open doors");
        Item spoon = new Item(2, "spoon", "don't put in microwave");
        Item spaghetti = new Item(3, "spaghetti", "Remember to microwave before eating you are spaghetti");

        inventory.Add(key1.itemID, key1);
        inventory.Add(spoon.itemID, spoon);
        inventory.Add(spaghetti.itemID, spoon);*/
    }

    public Hashtable itemList = new Hashtable();

    public Item itemCreate(int id, string name, string descrip)
    {
        Item tempItem = new Item(id, name, descrip);

        itemList.Add(tempItem.itemID, tempItem);

        return tempItem;
    }

    public void removeItem(int itemID)
    {
        itemList.Remove(itemID);
    }

}

