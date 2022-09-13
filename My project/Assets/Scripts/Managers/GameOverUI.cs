using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        scoreText.text = "You Scored:\n" + FindObjectOfType<ScoreManager>().GetScore();
    }
}
