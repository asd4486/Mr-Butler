using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    GameMain main;
    Rigidbody rb;

    Transform targetTable;

    [SerializeField] float mapOffset;

    [SerializeField] float walkSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        main = FindObjectOfType<GameMain>();
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!main.isGameStart) return;

        //find random table
        if (targetTable == null)
        {
            var tables = GameObject.FindGameObjectsWithTag("customer");
            targetTable = tables[Random.Range(0, tables.Length)].transform;
        }
        //enemy mover to target
        else
        {
            transform.LookAt(targetTable);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            rb.position = Vector3.MoveTowards(rb.position, targetTable.position, walkSpeed * Time.deltaTime);

            var dist = Vector3.Distance(rb.position, targetTable.position);
            if (dist < 1.5f) targetTable = null;
        }
    }
}
