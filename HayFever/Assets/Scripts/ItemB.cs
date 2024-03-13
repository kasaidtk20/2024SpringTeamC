using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemB : Item, IItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Affect(Player player)
    {
        player.shootNumber++;
        Destroy(gameObject);
    }
}
