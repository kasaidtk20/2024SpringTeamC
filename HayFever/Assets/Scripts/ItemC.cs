using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemC : Item,IItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Affect(Player player)
    {
        player.hp += 4;
        Destroy(gameObject);
    }
}

public class Item : MonoBehaviour
{
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10f) Destroy(gameObject);
    }
}
