using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemID { get; set; }
    public string iName { get; set; }
    public string iDescription { get; set; }

    public Item(int id, string name, string descrip)
    {
        this.itemID = id;
        this.iName = name;
        this.iDescription = descrip;
    }

}

