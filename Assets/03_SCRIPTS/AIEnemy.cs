using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    Rigidbody rb;

    Vector3? targetPos;

    [SerializeField] float mapOffset;

    [SerializeField] float walkSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(targetPos == null)
        {
            targetPos = new Vector3(Random.Range(-mapOffset, mapOffset), transform.position.y, Random.Range(-mapOffset, mapOffset));
        }
        //enemy mover to target
        else
        {
            rb.position = Vector3.MoveTowards(rb.position, targetPos.Value, walkSpeed * Time.deltaTime);

            var dist = Vector3.Distance(rb.position, targetPos.Value);
            if (dist < 0.1f) targetPos = null;
        }
    }
}
