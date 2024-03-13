using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _HP;
    [SerializeField] private Hay _pfbHay;

    [SerializeField] private float _shootInterval;
    public float shootInterval
    {
        get { return _shootInterval; }
        set
        {
            if (value < 0.1f) value = 0.1f;
            _shootInterval = value;
        }
    }

    [SerializeField] private int _shootNumber;
    public int shootNumber
    {
        get { return _shootNumber; }
        set { _shootNumber = value; }
    }

    [SerializeField] private Slider _HPBar;
    [SerializeField] private List<Sprite> _spriteTexes;

    private HFInputAction _hfinput;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    private float _hp;
    public float hp
    {
        get { return _hp; }
        set
        {
            if (value < _hp)
            {
                if (!_damagedTrigger)
                {
                    _damagedTrigger = true;
                    _renderer.color = new Color(255f / 255f, 125f / 255f, 125f / 255f);
                }
                _damagedCounter = 0;
            }

            if (value > _HP) value = _HP;
            else if (value < 0) value = 0;
            _hp = value;
            _HPBar.value = value;
            if (_hp == 0)
            {
                _renderer.sprite = _spriteTexes[0];
                enabled = false;
            }
        }
    }
    
    private float _shootCounter;

    private bool _damagedTrigger;
    private float _damagedCounter;

    // Start is called before the first frame update
    void Start()
    {
        _hfinput = new HFInputAction();
        _hfinput.Enable();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        hp = _HP;
        _HPBar.maxValue = _HP;
        _HPBar.value = _HP;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = _hfinput.Player.Move.ReadValue<Vector2>();
        _rigidbody.velocity = move * _speed;
        _shootCounter += Time.deltaTime;

        if (_shootCounter > _shootInterval)
        {
            _shootCounter = 0;
            Shoot(shootNumber);
        }

        if (_damagedTrigger)
        {
            _damagedCounter += Time.deltaTime;
            if (_damagedCounter > 0.1f)
            {
                _damagedTrigger = false;
                _renderer.color = Color.white;
            }
        }
        //transform.LookAt(move);
    }

    private void Shoot(int num)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float theta = Random.Range(0, 360);
        Vector2 min = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)) * 10;
        foreach(GameObject enemy in enemies)
        {
            Vector2 tmp = enemy.transform.position - transform.position;
            if (min.magnitude > tmp.magnitude) min = tmp;
        }

        for(int i = 0; i < num; i++)
        {
            Hay hay = Instantiate(_pfbHay, transform.position, Quaternion.identity);
            Vector2 dir = ComplexMultiply(min, new Vector2(Mathf.Cos(i * 2 * Mathf.PI / num), Mathf.Sin(i * 2 * Mathf.PI / num)));
            hay.Initialize(dir, 10, 1.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonoBehaviour col = collision.gameObject.GetComponent<MonoBehaviour>();
        if (col is enemyscript enemy) hp -= enemy.Attack();
        else if (col is IItem item) item.Affect(this);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        MonoBehaviour col = collision.gameObject.GetComponent<MonoBehaviour>();
        if (col is enemyscript enemy) hp-=enemy.Attack();
    }

    private Vector2 ComplexMultiply(Vector2 a, Vector2 b)
    {
        float x = a.x * b.x - a.y * b.y;
        float y = a.x * b.y + a.y * b.x;
        return new Vector2(x, y);
    }
}
