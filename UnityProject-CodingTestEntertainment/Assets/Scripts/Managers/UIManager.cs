using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TriangleFactory;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Timer")]
    float timerCountdownValue = 3f;
    float secondsBeforeStart = 3f;
    float timerGameValue = 10f;
    float secondsPerRound = 10f;
    bool startedCountdown;
    bool startShooting;

    ScoreManager scoreManagerScript;

    [SerializeField] Image countdownImage;
    [SerializeField] Image gameTimerImage;
    [SerializeField] TextMeshProUGUI secondsCountdownText;
    [SerializeField] TextMeshProUGUI secondsPlayText;
    [SerializeField] TextMeshProUGUI goText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    void Awake()
    {
        scoreManagerScript = FindObjectOfType<ScoreManager>();
    }

    void Start()
    {
        Player.OnWeaponEquipped += PlayerScript_OnWeaponEquipped;
        if (SceneManager.GetActiveScene().buildIndex == 1) 
        {
            scoreManagerScript.LoadBestScore();
        }
        bestScoreText.text = "Best Score: " + scoreManagerScript.GetBestScore().ToString("000000000");
    }

    void Update()
    {
        UpdateScore();
        UpdateCountdownTimer();
        UpdateGameTimer();
    }

    void UpdateScore()
    {
        scoreText.text = scoreManagerScript.GetScore().ToString("000000000");
    }

    void UpdateCountdownTimer()
    {
        if (startedCountdown == false) return;
        timerCountdownValue -= Time.deltaTime;
        secondsCountdownText.text = Mathf.Round(timerCountdownValue).ToString();

        if (timerCountdownValue > 0)
        {
            countdownImage.fillAmount = timerCountdownValue / secondsBeforeStart;
        }
        else
        {
            StartCoroutine(TurnOffCountdownElements());
        }
    }

    void UpdateGameTimer()
    {
        if (startShooting == false) return;
        gameTimerImage.gameObject.SetActive(true);
        timerGameValue -= Time.deltaTime;
        secondsPlayText.text = Mathf.Round(timerGameValue).ToString();

        if (timerGameValue > 0)
        {
            gameTimerImage.fillAmount = timerGameValue / secondsPerRound;
        }
        else
        {
            startShooting = false;
            scoreManagerScript.TrySaveBestScore();
            FindObjectOfType<LevelManager>().LoadEndMenu();
        }
    }

    IEnumerator TurnOffCountdownElements()
    {
        goText.gameObject.SetActive(true);
        secondsCountdownText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        countdownImage.gameObject.SetActive(false);
        startedCountdown = false;
        startShooting = true;
    }

    void PlayerScript_OnWeaponEquipped(object sender, EventArgs e)
    {
        startedCountdown = true;
        if (countdownImage != null)
        {
            countdownImage.gameObject.SetActive(true);
        }
    }

    public bool GetStartShootingBool() => startShooting;
}
