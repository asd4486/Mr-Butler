using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] Text textScore;
    [SerializeField] Text textTimer;
    [SerializeField] float timeLeft = 60.0f;

    GameMain main;
    //// Start is called before the first frame update
    void Start()
    {
        main = FindObjectOfType<GameMain>();
        SetScore(0);
    }

    void Update()
    {
        SetTimer();
    }

    public void SetScore(int score)
    {
        textScore.text = "Score : " + score;
    }

    public void SetTimer()
    {
        timeLeft -= Time.deltaTime;
        textTimer.text = "Timer : " + (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            Debug.Log("Dead");
        }
    }
}
