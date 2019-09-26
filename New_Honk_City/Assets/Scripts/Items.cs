using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Items
{

    public int id;
    public string name;
    public Sprite icon;

    Dictionary<string, int> NPC = new Dictionary<string, int>();
    public Dictionary<string, string> stats = new Dictionary<string, string>();

    public Items(int id, string name, Dictionary<string, int> stats)
    {
        this.id = id;
        this.name = name;
        this.icon = Resources.Load<Sprite>("" + name);
    }

    public Items(Items item)
    {
        this.id = item.id;
        this.name = item.name;
        this.icon = Resources.Load<Sprite>("" + item.name);
    }

}
