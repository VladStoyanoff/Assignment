using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Timer")]
    float timerValue = 3f;
    float secondsBeforeStart = 3f;

    [SerializeField] Image countdownImage;
    [SerializeField] TextMeshProUGUI secondsText;
    [SerializeField] TextMeshProUGUI goText;

    void Update()
    {
        UpdateScore();
        UpdateTimer();
    }
    void UpdateScore()
    {
        scoreText.text = FindObjectOfType<ScoreManager>().GetScore().ToString("000000000");
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        secondsText.text = Mathf.Round(timerValue).ToString();

        if (timerValue > 0)
        {
            countdownImage.fillAmount = timerValue / secondsBeforeStart;
        }
        else
        {
            goText.gameObject.SetActive(true);
        }
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }
}
