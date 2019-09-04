using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    UIPlayer uiPlayer;
    GameMain main;

    Rigidbody rb;

    float distToGround;

    bool isWalkLeft;
    bool isWalkRight;

    [SerializeField] float walkSpeed;
    [SerializeField] float turnSpeed;
    Vector3 myRotation;

    bool isStun;

    float deadTimer;
    [SerializeField] float deadTime = 0.5f;

    int myScore;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        uiPlayer = FindObjectOfType<UIPlayer>();
        main = FindObjectOfType<GameMain>();

        // get the distance to ground
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
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
            main.StartGame();
        }

        if (!main.isGameStart || isStun) return;

        if (isWalkLeft && !isWalkRight)
        {
            myRotation -= new Vector3(0, turnSpeed, 0);
        }
        else if (!isWalkLeft && isWalkRight)
        {
            myRotation += new Vector3(0, turnSpeed, 0);
        }

        transform.eulerAngles = myRotation;
        if (IsGrounded()) rb.velocity = transform.forward * walkSpeed;
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
        if (!main.isGameStart) return;

        //player drop
        if (transform.position.y < -10) main.GameOver();

        if (isWalkLeft || isWalkRight)
        {
            deadTimer = 0;
            return;
        }

        deadTimer += Time.deltaTime;
        //reload game when dead
        if (deadTimer >= deadTime) main.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * 15000);

            StartCoroutine(StartStunCoroutine());
        }
    }

    IEnumerator StartStunCoroutine()
    {
        isStun = true;
        yield return new WaitForSeconds(0.4f);
        isStun = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "customer")
        {
            var customer = other.gameObject.GetComponent<AICustomer>();
            switch (customer.myStatus)
            {
                case CustomerStatus.Eating:
                    myScore -= 1;
                    break;
                case CustomerStatus.Ordering:
                    myScore += 5;
                    customer.OrderComplete();
                    break;
            }

            uiPlayer.SetScore(myScore);
        }
    }
}
