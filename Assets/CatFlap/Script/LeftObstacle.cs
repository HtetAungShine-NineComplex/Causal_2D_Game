using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftObstacle : Obstacle
{
    private float m_distance = 1.26f;
    
    public override void Start()
    {
        distance = m_distance;
        base.Start();
        
    }
}
