using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "You Scored:\n" + FindObjectOfType<ScoreManager>().GetScore();
    }
}
