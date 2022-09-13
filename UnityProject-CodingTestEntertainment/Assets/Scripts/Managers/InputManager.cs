using System.Collections;
using System.Collections.Generic;
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
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public bool Interact() => playerInputAction.Player.Interact.WasPressedThisFrame();
    public bool Fire() => playerInputAction.Player.Fire.WasPressedThisFrame();
    public float Movement() => playerInputAction.Player.Movement.ReadValue<float>();
    public Vector2 GetMouseScreenPosition() => Mouse.current.position.ReadValue();
}
