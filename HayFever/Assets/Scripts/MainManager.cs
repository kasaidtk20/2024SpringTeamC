using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<enemyscript> _pfbEnemies;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _spawnNumber;
    [SerializeField] private TextMeshProUGUI _timerTex;
    [SerializeField] private TextMeshProUGUI _GGTex;

    private float _stimer;
    private int _mtimer;
    private float _spawnCounter;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        _spawnCounter += Time.deltaTime;
        _stimer += Time.deltaTime;
        if (_stimer > 60)
        {
            _stimer -= 60;
            _mtimer++;
            _spawnInterval -= 0.1f;
            _spawnNumber++;
        }
        _timerTex.text = _mtimer.ToString("00") + ":" + _stimer.ToString("00");
        if( _spawnCounter > _spawnInterval)
        {
            _spawnCounter = 0;
            SpawnEnemy(_spawnNumber);
        }
        if (_mtimer >= 5) GameClear();
        else if (_player.hp == 0) GameOver();
    }

    private void SpawnEnemy(int value)
    {
        Vector2 a = Camera.main.ViewportToWorldPoint(Vector2.zero);
        Vector2 b = Camera.main.ViewportToWorldPoint(Vector2.one);
        for(int i=0;i<value; i++)
        {
            Vector2 spawnPos = new Vector2();
            float theta = Random.Range(-0.25f * Mathf.PI, 1.75f * Mathf.PI);
            float rx = Random.Range((b.x - a.x) / 2f, (b.x - a.x) / 2f + 10f);
            float ry = Random.Range((b.y - a.y) / 2f, (b.y - a.y) / 2f + 10f);
            if (-0.25f * Mathf.PI < theta && theta <= 0.25f * Mathf.PI) spawnPos.Set(rx, Mathf.Tan(theta) * ry);
            else if (theta <= 0.75f * Mathf.PI) spawnPos.Set(1 / Mathf.Tan(theta) * rx, ry);
            else if (theta <= 1.25f * Mathf.PI) spawnPos.Set(-rx, -Mathf.Tan(theta) * ry);
            else spawnPos.Set(-1 / Mathf.Tan(theta) * rx, -ry);
            spawnPos += (a + b) / 2f;
            enemyscript enemy = Instantiate(_pfbEnemies[Random.Range(0, _pfbEnemies.Count)], spawnPos, Quaternion.identity);
            enemy._player = _player.transform;
        }
    }

    private void GameOver()
    {
        _GGTex.gameObject.SetActive(true);
        isGameOver = true;
        StartCoroutine(ShiftScene());
    }

    private void GameClear()
    {
        _GGTex.text = "GameClear";
        _GGTex.gameObject.SetActive(true);
    }

    private IEnumerator ShiftScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartScene");
    }
}

public interface IItem
{
    public void Affect(Player player);
}


