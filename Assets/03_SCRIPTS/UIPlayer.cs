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

    [SerializeField] GameObject speedUpObject;

    [SerializeField] Text textLevel;
    int myLevel = 1;

    [SerializeField] GameObject uiGameOver;
    [SerializeField] Text textGameOverScore;

    //// Start is called before the first frame update
    void Start()
    {
        uiGameOver.SetActive(false);
        main = FindObjectOfType<GameMain>();

        textLevel.text = "Level " + myLevel;
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
        textTimer.text = "Next Level : " + time.ToString("0");
    }

    public void LevelUp()
    {
        StartCoroutine(LevelUpCoroutine());

        myLevel += 1;
        textLevel.text = "Level " + myLevel;
    }

    IEnumerator LevelUpCoroutine()
    {
        speedUpObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        speedUpObject.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        uiGameOver.SetActive(true);
        textGameOverScore.text = textScore.text;
    }
}
