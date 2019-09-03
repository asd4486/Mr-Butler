using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    Transform playerTransform;

    private Vector3 offset;

    //float distance;
    Vector3 playerPrevPos, playerMoveDir;
    [SerializeField] float camY;

    // Use this for initialization
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("butler").transform;
        offset = transform.position - playerTransform.position;

        playerPrevPos = playerTransform.position;
    }

    void LateUpdate()
    {
        var pos = playerTransform.position + (-playerTransform.forward * offset.magnitude);
        transform.position = new Vector3(pos.x, camY, pos.z);
        transform.LookAt(playerTransform.position);
    }
}
