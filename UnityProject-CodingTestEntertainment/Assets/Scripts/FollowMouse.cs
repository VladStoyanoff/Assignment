using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    float sensitivity = 200f;
    float rotationUpDown = 0f;
    float rotationLeftRight = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        var y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationLeftRight -= y;
        rotationUpDown += x;
        rotationUpDown = Mathf.Clamp(rotationUpDown, -90f, 90f);
        rotationLeftRight = Mathf.Clamp(rotationLeftRight, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationLeftRight, rotationUpDown, 0f);
    }
}
