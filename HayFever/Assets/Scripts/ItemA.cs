using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemA : Item, IItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Affect(Player player)
    {
        player.shootInterval *= 0.99f;
        Destroy(gameObject);
    }
}
