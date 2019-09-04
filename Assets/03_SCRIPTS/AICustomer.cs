using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CustomerStatus
{
    NoOrder,
    Ordering,
    Eating
}

public class AICustomer : MonoBehaviour
{
    GameMain main;

    Material myMat;
    [HideInInspector] public CustomerStatus myStatus;
    [SerializeField] Image waitBar;

    float changeStatusTimer;
    float nextStatusTime;

    private void Awake()
    {
        main = FindObjectOfType<GameMain>();
        myMat = GetComponentInChildren<MeshRenderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {     
        SetNextStatusInfos();
    }

    private void Update()
    {
        if (!main.isGameStart) return;

        changeStatusTimer += Time.deltaTime;
        SetOrderTimerFill();
        
        if(changeStatusTimer > nextStatusTime)
        {
            myStatus = (CustomerStatus)Random.Range(0, 2);
            SetNextStatusInfos();
        }
    }

    void SetOrderTimerFill()
    {
        if(myStatus == CustomerStatus.NoOrder) return;
        waitBar.fillAmount = (nextStatusTime - changeStatusTimer) / nextStatusTime;
    }

    void SetNextStatusInfos()
    {
        changeStatusTimer = 0;
        switch (myStatus)
        {
            case CustomerStatus.NoOrder:
                myMat.color = Color.green;
                nextStatusTime = 2;
                break;
            case CustomerStatus.Ordering:
                myMat.color = Color.blue;
                nextStatusTime = 5;
                break;
            case CustomerStatus.Eating:
                myMat.color = Color.red;
                nextStatusTime = 2;
                break;
        }
    }
    
    public void OrderComplete()
    {
        myStatus = CustomerStatus.Eating;
        SetNextStatusInfos();
    }
}
