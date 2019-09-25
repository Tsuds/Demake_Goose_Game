using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Items : MonoBehaviour
{
    
    Dictionary<string, string> NPC = new Dictionary<string, string>();
    public Dictionary<string, string> Item = new Dictionary<string, string>();

    private void Start()
    {
        Item.Add("Key", "NPC1");
        Item.Add("Rake", "NPC1");
        Item.Add("", "");
        Item.Add("", "");
        Item.Add("", "");
        Item.Add("", "");
        Item.Add("", "");
        Item.Add("", "");
    }

    private void Update()
    {
        foreach(KeyValuePair<string, string> keyValue in Item)
        {

        }
    }

}
