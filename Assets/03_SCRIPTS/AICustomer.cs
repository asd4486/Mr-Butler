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
    Material mat;
    [HideInInspector] public CustomerStatus myStatus;

    float changeStatusTimer;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        changeStatusTimer += Time.deltaTime;

        if(changeStatusTimer > 2)
        {
            myStatus = (CustomerStatus)Random.Range(0, 2);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "butler")
    //    {
    //        Debug.Log("hit");
    //        mat.color = Color.green;
    //    }
    //}
}
