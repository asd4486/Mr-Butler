using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    UIPlayer uiPlayer;

    [SerializeField] float timeLeft = 60.0f;

    [HideInInspector] public bool isGameStart;
    bool isGameOver;

    [SerializeField] GameObject startScreen;

    [SerializeField] GameObject customerPrefab;
    [SerializeField] int customerTotal = 10;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyTotal = 3;

    [SerializeField] float spawnSizeOffset;

    private void Awake()
    {
        uiPlayer = FindObjectOfType<UIPlayer>();
        startScreen.SetActive(true);
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        if (isGameOver || isGameStart) return;

        isGameStart = true;
        startScreen.SetActive(false);
        SpawnCustomers();
        SpawnEnemies();
    }

    void SpawnCustomers()
    {
        for (int i = 0; i < customerTotal; i++)
        {
            var pos = new Vector3(Random.Range(-spawnSizeOffset, spawnSizeOffset), 0, Random.Range(-spawnSizeOffset, spawnSizeOffset));
            while (!CheckCustomerPos(pos))
            {
                pos = new Vector3(Random.Range(-spawnSizeOffset, spawnSizeOffset), 0, Random.Range(-spawnSizeOffset, spawnSizeOffset));
            }
            var o = Instantiate(customerPrefab, pos, Quaternion.identity);
        }
    }

    //check if customer is too closes to other customer or not
    bool CheckCustomerPos(Vector3 pos)
    {
        var customers = GameObject.FindGameObjectsWithTag("customer");
        foreach (var c in customers)
        {
            var dist = Vector3.Distance(pos, c.transform.position);
            if (dist < 10) return false;
        }
        return true;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyTotal; i++)
        {
            var pos = new Vector3(Random.Range(-spawnSizeOffset, spawnSizeOffset), 1, Random.Range(-spawnSizeOffset, spawnSizeOffset));
            while (!CheckEnemyPos(pos))
            {
                pos = new Vector3(Random.Range(-spawnSizeOffset, spawnSizeOffset), 1, Random.Range(-spawnSizeOffset, spawnSizeOffset));
            }
            var o = Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }

    //check if customer is too closes to other customer or not
    bool CheckEnemyPos(Vector3 pos)
    {
        var player = GameObject.FindGameObjectWithTag("butler");
        var playerDist = Vector3.Distance(pos, player.transform.position);
        if (playerDist < 20) return false;

        var enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var e in enemies)
        {
            var dist = Vector3.Distance(pos, e.transform.position);
            if (dist < 20) return false;        
        }
        return true;
    }

    void Update()
    {
        if (!isGameStart) return;
        SetTimer();
    }

    public void SetTimer()
    {
        timeLeft -= Time.deltaTime;
        uiPlayer.SetTimer(timeLeft);

        if (timeLeft < 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        isGameStart = false;

        uiPlayer.ShowGameOverUI();
    }
}
