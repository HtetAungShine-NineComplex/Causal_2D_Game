using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreTxt;
    private int score;

    private void Start()
    {
        score = 0;
        UpdateScoreTxt();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreTxt();
    }

    public void UpdateScoreTxt()
    {
        scoreTxt.text = "Score: " + score.ToString();
    }
}
