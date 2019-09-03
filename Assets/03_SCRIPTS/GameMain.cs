using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    [HideInInspector] public bool isGameStart;
    [SerializeField] GameObject startScreen;
 
    [SerializeField] GameObject customerPrefab;

    [SerializeField] int customerTotal = 10;
    [SerializeField] float spawnSizeOffset;

    // Start is called before the first frame update
    public void StartGame()
    {
        if(isGameStart) return;
        isGameStart = true;
        startScreen.SetActive(false);
        SpawnCustomers();
    }

    public void Reload()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    void SpawnCustomers()
    {
        for (int i = 0; i < customerTotal; i++)
        {
            var pos = new Vector3(Random.Range(-spawnSizeOffset, spawnSizeOffset), 0, Random.Range(-spawnSizeOffset, spawnSizeOffset));
            while (!CheckPos(pos))
            {
                pos = new Vector3(Random.Range(-spawnSizeOffset, spawnSizeOffset), 0, Random.Range(-spawnSizeOffset, spawnSizeOffset));
            }
            var o = Instantiate(customerPrefab, pos, Quaternion.identity);
        }
    }

    //check if customer is too closes to other customer or not
    bool CheckPos(Vector3 pos)
    {
        var customers = GameObject.FindGameObjectsWithTag("customer");
        foreach(var c in customers)
        {
            var dist = Vector3.Distance(pos, c.transform.position);
            if (dist < 10) return false;
        }
        return true;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
