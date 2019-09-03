using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("butler").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90, playerTransform.eulerAngles.y, 0);
        transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
    }
}
