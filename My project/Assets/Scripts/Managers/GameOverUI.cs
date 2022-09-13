using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        Debug.Log(FindObjectOfType<ScoreManager>().GetScore());
        Cursor.lockState = CursorLockMode.None;
        scoreText.text = "You Scored:\n" + FindObjectOfType<ScoreManager>().GetScore();
    }
}
