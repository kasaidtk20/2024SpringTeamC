using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hay : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _smul;
    private float _attack;
    private Vector2 _dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)_dir * _speed * _smul * Time.deltaTime;
    }

    public void Initialize(Vector2 dir, float attack, float smul)
    {
        _dir = dir.normalized;
        _attack = attack;
        _smul = smul;
    }

    public void Hit()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        enemyscript enemy = collision.gameObject.GetComponent<enemyscript>();
        if (enemy != null)
        {
            enemy.Damaged(_attack * Time.deltaTime);
            enemy.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyscript enemy = collision.gameObject.GetComponent<enemyscript>();
        if(enemy != null)
        {
            enemy.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
