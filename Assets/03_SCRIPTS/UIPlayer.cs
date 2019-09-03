using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    
    [SerializeField] Text textScore;
    //// Start is called before the first frame update
    void Start()
    {
        SetScore(0);
    }

    public void SetScore(int score)
    {
        textScore.text = "Score : " + score;
    }
}
