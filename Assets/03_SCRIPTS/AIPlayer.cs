using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    UIPlayer uiPlayer;

    Rigidbody rb;
    bool isWalkLeft;
    bool isWalkRight;

    [SerializeField] float walkSpeed;
    [SerializeField] float turnSpeed;

    int myScore;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        uiPlayer = FindObjectOfType<UIPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isWalkLeft = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isWalkRight = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isWalkLeft = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isWalkRight = false;
        }


        if (isWalkLeft && !isWalkRight)
        {
            transform.Rotate(0, -turnSpeed, 0);
        }
        else if (!isWalkLeft && isWalkRight)
        {
            transform.Rotate(0, turnSpeed, 0);
        }

        if (!isWalkLeft && !isWalkRight) Debug.Log("dead");
        else rb.velocity = transform.forward * walkSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "customer")
        {
            var customer = other.gameObject.GetComponent<AICustomer>();
            switch (customer.myStatus)
            {
                case CustomerStatus.NoOrder:
                    myScore -= 1;
                    break;
                case CustomerStatus.Ordering:
                    myScore += 1;
                    break;
                    //case CustomerStatus.Eating:
                    //    break;
            }

            uiPlayer.SetScore(myScore);
        }
    }
}
