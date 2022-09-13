using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    AudioManager audioManagerScript;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one ScoreManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        audioManagerScript = FindObjectOfType<AudioManager>();
    }

    public void StartGame()
    {
        audioManagerScript.PlayUIClickedClip();
        SceneManager.LoadScene(1);
    }

    public void LoadSaloon()
    {
        audioManagerScript.PlayUIClickedClip();
        SceneManager.LoadScene(1);
        audioManagerScript.StartGameMusic();
    }

    public void LoadEndMenu()
    {
        audioManagerScript.PlayUIClickedClip();
        SceneManager.LoadScene(2);
        audioManagerScript.StartEndMenuMusic();

    }
}
