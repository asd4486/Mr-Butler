using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    bool isGameStart;

    UIPlayer uiPlayer;

    Rigidbody rb;
    bool isWalkLeft;
    bool isWalkRight;

    [SerializeField] float walkSpeed;
    [SerializeField] float turnSpeed;

    float deadTimer;
    [SerializeField] float deadTime = 0.5f;

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
        InputController();

        CheckDead();
    }

    private void FixedUpdate()
    {
        if (isWalkLeft && isWalkRight)
        {
            if (!isGameStart) isGameStart = true;
        }

        if (!isGameStart) return;

        if (isWalkLeft && !isWalkRight)
        {
            transform.Rotate(0, -turnSpeed, 0);
        }
        else if (!isWalkLeft && isWalkRight)
        {
            transform.Rotate(0, turnSpeed, 0);
        }

        rb.velocity = transform.forward * walkSpeed;
    }

    void InputController()
    {
        // player controller
        if (Input.GetMouseButtonDown(0))
        {
            isWalkRight = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isWalkLeft = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isWalkRight = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isWalkLeft = false;
        }
    }

    void CheckDead()
    {
        if (!isGameStart) return;

        if (isWalkLeft || isWalkRight)
        {
            deadTimer = 0;
            return;
        }

        deadTimer += Time.deltaTime;
        if (deadTimer >= deadTime)
            Debug.Log("dead");
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
                    customer.OrderComplete();
                    break;
            }

            uiPlayer.SetScore(myScore);
        }
    }
}
