using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerStatus
{
    NoOrder,
    Ordering,
    Eating
}

public class AICustomer : MonoBehaviour
{
    Material myMat;
    [HideInInspector] public CustomerStatus myStatus;

    float changeStatusTimer;
    float nextStatusTime;

    // Start is called before the first frame update
    void Start()
    {
        myMat = GetComponent<MeshRenderer>().material;
        SetNextStatusInfos();
    }

    private void Update()
    {
        changeStatusTimer += Time.deltaTime;

        if(changeStatusTimer > nextStatusTime)
        {
            myStatus = (CustomerStatus)Random.Range(0, 2);
            SetNextStatusInfos();
        }
    }

    void SetNextStatusInfos()
    {
        changeStatusTimer = 0;
        switch (myStatus)
        {
            case CustomerStatus.NoOrder:
                myMat.color = Color.red;
                nextStatusTime = 2;
                break;
            case CustomerStatus.Ordering:
                myMat.color = Color.blue;
                nextStatusTime = 4;
                break;
            case CustomerStatus.Eating:
                myMat.color = Color.green;
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
