using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    public Transform _player { get; set; }
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _HP;
    [SerializeField] private float _attack;
    private SpriteRenderer _renderer;
    [SerializeField] private List<Sprite> _spriteTexes;
    [SerializeField] private MonoBehaviour pfbItem;
    [SerializeField] private int _itemDropPercent;
    private float _hp;
    private bool _attackTrigger;
    private float _attackCounter;

    private int a = 0;
    // Start is called before the first frame update
    private void Start()
    {
        _renderer=GetComponent<SpriteRenderer>();
        _hp = _HP;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            transform.Translate((_player.position-transform.position) * _moveSpeed * Time.deltaTime);
        }

        if (_attackTrigger)
        {
            _attackCounter += Time.deltaTime;
            if( _attackCounter > 1)
            {
                _attackTrigger = false;
                _renderer.sprite = _spriteTexes[0];
            }
        }
    }

    public void Damaged(float value)
    {
        _hp -= value;
        if (_hp <= 0)
        {
            _hp = 0;
            int r = Random.Range(1, 100);
            if (r < _itemDropPercent) Instantiate(pfbItem,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public float Attack()
    {
        if (!_attackTrigger)
        {
            _attackTrigger = true;
            _renderer.sprite = _spriteTexes[1];
        }
        _attackCounter = 0;
        return _attack * Time.deltaTime;
    }
}
