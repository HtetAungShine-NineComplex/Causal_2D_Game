using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> rightObstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> leftObstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> OccupiedPos = new List<GameObject>();
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinParentTransform;

    private int ObstacleCount;
    private void Start()
    {


        //When trigger to the collider, need to enable opposite side of Obstacles
        Bird.GetInstance.OnTriggerLeftCollider += activateRightObstacle;
        Bird.GetInstance.OnTriggerRightCollider += activateLeftObstacle;
    }

    private void activateLeftObstacle()
    {
        Debug.Log("Trigger Left Obstacle");
        activateRandomObstacles(leftObstacles, true);
        deactivateObstacles(rightObstacles);
    }

    private void activateRightObstacle()
    {
        Debug.Log("Trigger Right Obstacle");
        activateRandomObstacles(rightObstacles, false);
        deactivateObstacles(leftObstacles);
    }

    private void activateRandomObstacles(List<GameObject> obstacles, bool isleft)
    {
        bool randomSide = Random.Range(0, 2) == 0;
        Debug.Log("Player Score " + ScoreManager.Score);
        if (ScoreManager.Score >= 1 && ScoreManager.Score <= 10)
        {
            ObstacleCount = Random.Range(2, 3);
            SpawnCoin(obstacles, randomSide, 1, 1);

        }
        else if (ScoreManager.Score >= 11 && ScoreManager.Score <= 20) //When Score is between 11-20
        {
            ObstacleCount = Random.Range(3, 4);
            SpawnCoin(obstacles, randomSide, 1, 1);
        }
        else if (ScoreManager.Score >= 21 && ScoreManager.Score <= 30) //When Score is between 21-30
        {
            ObstacleCount = Random.Range(3, 5);
            SpawnCoin(obstacles, randomSide, 1, 1);
        }
        else if (ScoreManager.Score >= 31 && ScoreManager.Score <= 50) //When Score is between 31-50
        {
            ObstacleCount = Random.Range(4, 5);
            SpawnCoin(obstacles, randomSide, 1, 1);
        }
        else if (ScoreManager.Score >= 51 && ScoreManager.Score <= 100) //When Score is between 31-50
        {
            ObstacleCount = Random.Range(4, 6);
            SpawnCoin(obstacles, randomSide, 1, 1);
        }
        /*        else
                {
                    ObstacleCount = Random.Range(5, 7);
                }  */

        List<GameObject> shuffleObstacles = new List<GameObject>(obstacles);
        shuffleObstacles.Shuffle();
        for (int i = 0; i < ObstacleCount && i < shuffleObstacles.Count; i++)
        {
            shuffleObstacles[i].SetActive(true);
            OccupiedPos.Add(shuffleObstacles[i]);
        }

    }

    private void SpawnCoin(List<GameObject> obstacles, bool isLeft, int coinCount, int spawnTimes)
    {
        List<GameObject> coinPosition = new List<GameObject>();
        for (int i = 0; i < obstacles.Count; i++)
        {
            if (!OccupiedPos.Contains(obstacles[i]))
            {
                coinPosition.Add(obstacles[i]);
            }
        }

        if (coinPosition.Count > 0)
        {
            for (int i = 0; i < spawnTimes; i++)
            {
                for (int j = 0; j < coinCount; j++)
                {
                    Vector3 randomCoinPos = coinPosition[Random.Range(0, coinPosition.Count)].transform.position;
                    GameObject randomCoin = Instantiate(coinPrefab, randomCoinPos, Quaternion.identity);
                    Vector3 randomCoinInitialPos = randomCoin.transform.position;
                    if (isLeft)
                    {
                        randomCoin.transform.position = randomCoinInitialPos + new Vector3(1.58f, 0, 0);
                        Debug.Log("Left Coin spawned");
                    }
                    else
                    {
                        randomCoin.transform.position = randomCoinInitialPos + new Vector3(-1.66f, 0, 0);
                        Debug.Log("Right Coin spawned");
                    }
                }
            }
        }
    }
        private void deactivateObstacles(List<GameObject> obstacles)
    {
        foreach (var obstacle in obstacles)
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
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}