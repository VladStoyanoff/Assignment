using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;

    public static ScoreManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one ScoreManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void ModifyScore(int score)
    {
        currentScore += score;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public int GetScore() => currentScore;
}
