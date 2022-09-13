using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveLaterally : MonoBehaviour
{
    float randomMultiplier;
    float speed = .01f;

    void Start()
    {
       randomMultiplier = Random.Range(-1f, 1f);
    }
    void Update()
    {
        if (FindObjectOfType<UIManager>().GetStartShootingBool() == false) return;
        transform.position += new Vector3(speed * randomMultiplier, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "EndPoint")
        {
            speed = -speed;
        }
    }
}
