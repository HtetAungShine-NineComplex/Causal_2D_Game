using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 currentPos;

    public float distance;
    private float speed = 2.5f;

    private void Update()
    {
        move();
    }
    public virtual void initTargetPos(float distance)
    {
        targetPos = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
    }
    public virtual void Start()
    {

        currentPos = transform.position;
        initTargetPos(distance);
    }
    private void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        transform.position = currentPos;
    }
}
