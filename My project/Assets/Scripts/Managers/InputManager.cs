using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputAction playerInputAction;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject mainCamera;

    bool paused;

    void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        PauseGame();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            panel.gameObject.SetActive(paused);
            mainCamera.GetComponent<FollowMouse>().enabled = !paused;
            StopTime();
            CursorBehaviour();
        }
    }

    void CursorBehaviour()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        Cursor.lockState = CursorLockMode.None;
    }

    void StopTime()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            return;
        }
        Time.timeScale = 0;
    }
    public bool Interact() => playerInputAction.Player.Interact.WasPressedThisFrame();
    public bool Fire() => playerInputAction.Player.Fire.WasPressedThisFrame();
    public float Movement() => playerInputAction.Player.Movement.ReadValue<float>();
    public Vector2 GetMouseScreenPosition() => Mouse.current.position.ReadValue();
}
