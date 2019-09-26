using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Items> items = new List<Items>();

    private void Awake()
    {
        BuildDatabase();
    }
    
    void BuildDatabase()
    {
        items = new List<Items>()
        {
            new Items(0, "Key",
            new Dictionary<string, int>
            {
                {"power", 15 },
                {"defence", 10 }
            })
        };
    }

    public Items GetItem(int id)
    {
        return items.Find(items => items.id == id);
    }

    public Items GetItem(string itemName)
    {
        return items.Find(items => items.name == itemName);
    }

}
