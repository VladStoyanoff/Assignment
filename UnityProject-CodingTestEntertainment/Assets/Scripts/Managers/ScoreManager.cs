using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;
    static ScoreManager instance;

    void Awake()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void ModifyScore(int score)
    {
        currentScore += score;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(currentScore);
    }

    public int GetScore() => currentScore;
}
