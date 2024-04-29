using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RightObstacle : Obstacle
{
    private float m_distance = -1.26f;

    public override void Start()
    {
        distance = m_distance;
        base.Start();
    }
}
