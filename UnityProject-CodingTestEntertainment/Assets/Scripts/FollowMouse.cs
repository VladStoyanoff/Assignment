using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    float sensitivity = 200f;
    float rotationUpDown = 0f;

    [SerializeField] Transform player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationUpDown -= y;
        rotationUpDown = Mathf.Clamp(rotationUpDown, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationUpDown, 0f, 0f);

        player.Rotate(Vector3.up * x);
    }
}
