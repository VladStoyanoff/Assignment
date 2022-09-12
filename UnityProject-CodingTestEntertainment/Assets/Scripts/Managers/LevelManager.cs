using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one LevelManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoadSaloon()
    {
        FindObjectOfType<ScoreManager>().ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {

    }
}
