using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    GameMain main;

    [SerializeField] Text textScore;
    [SerializeField] Text textTimer;

    [SerializeField] GameObject uiGameOver;
    [SerializeField] Text textGameOverScore;

    //// Start is called before the first frame update
    void Start()
    {
        uiGameOver.SetActive(false);
        main = FindObjectOfType<GameMain>();
        SetScore(0);
    }

    public void OnClickReloadScene()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void SetScore(int score)
    {
        textScore.text = "Score : " + score;
    }

    public void SetTimer(float time)
    {
        textTimer.text = "Timer : " + time.ToString("0");
    }

    public void ShowGameOverUI()
    {
        uiGameOver.SetActive(true);
        textGameOverScore.text = textScore.text;
    }
}
