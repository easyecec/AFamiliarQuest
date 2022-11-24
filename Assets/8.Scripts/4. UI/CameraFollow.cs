using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float yOffset = 0.3f;
    [SerializeField] private float xOffset = 7f;
    [SerializeField] private float zOffset = -10f;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, zOffset);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
