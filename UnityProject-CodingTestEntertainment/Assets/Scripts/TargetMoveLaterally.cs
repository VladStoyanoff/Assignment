using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveLaterally : MonoBehaviour
{
    float randomMultiplier;

    void Start()
    {
       randomMultiplier = Random.Range(-1f, 1f);
    }
    void Update()
    {
        if (FindObjectOfType<UIManager>().GetStartShootingBool() == false) return;
        transform.position += new Vector3(.01f * randomMultiplier, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        randomMultiplier = Random.Range(-1f, 1f);
    }
}
