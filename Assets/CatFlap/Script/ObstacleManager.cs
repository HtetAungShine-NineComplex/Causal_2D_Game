using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> rightObstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> leftObstacles = new List<GameObject>();

    private void Start()
    {
        //When trigger to the collider, need to enable opposite side of Obstacles
        Bird.GetInstance.OnTriggerLeftCollider += activateRightObstacle;
        Bird.GetInstance.OnTriggerRightCollider += activateLeftObstacle;
    }

    private void activateLeftObstacle()
    {
        Debug.Log("Trigger Left Obstacle");
        activateRandomObstacles(leftObstacles);
        deactivateObstacles(rightObstacles);
    }

    private void activateRightObstacle()
    {
        Debug.Log("Trigger Right Obstacle");
        activateRandomObstacles(rightObstacles);
        deactivateObstacles(leftObstacles);
    }

    private void activateRandomObstacles(List<GameObject> obstacles)
    {
        int ObstacleCount = Random.Range(2, 4);
        List<GameObject> shuffleObstacles = new List<GameObject>(obstacles);
        shuffleObstacles.Shuffle();
        for (int i = 0; i < ObstacleCount && i < shuffleObstacles.Count; i++)
        {
            shuffleObstacles[i].SetActive(true);
        }
    }

    private void deactivateObstacles(List<GameObject> obstacles)
    {
        foreach(var obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while(n>1)
        {
            n--;
            int k = Random.Range(0, n+1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
