using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameObject targetsTop;
    [SerializeField] GameObject targetsMid;

    GameObject[] stationaryTargets;
    GameObject[] movingTargets;
    GameObject[] targetsArray;

    UIManager UIManagerScript;

    bool IsRunningS;
    bool IsRunningM;
    float movingMultiplier;


    void Awake()
    {
        UIManagerScript = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        stationaryTargets = GameObject.FindGameObjectsWithTag("StationaryTarget");
        movingTargets = GameObject.FindGameObjectsWithTag("MovingTarget");
        targetsArray = stationaryTargets.Concat(movingTargets).ToArray();
    }

    void Update()
    {
        if (UIManagerScript.GetStartShootingBool() == false) return;
        if (IsRunningS) return;
        StartCoroutine(UpdateStationaryTargets());
        if (IsRunningM) return;
        StartCoroutine(UpdateMovingTargets());
    }

    public void DisableTargets()
    {
        for (var i = 0; i < stationaryTargets.Length; i++)
        {
            stationaryTargets[i].gameObject.SetActive(false);
        }

        for (var j = 0; j < movingTargets.Length; j++)
        {
            movingTargets[j].gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateStationaryTargets()
    {
        IsRunningS = true;
        var stationaryTargetIndex = UnityEngine.Random.Range(0, stationaryTargets.Length);
        stationaryTargets[stationaryTargetIndex].gameObject.SetActive(true);
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 10));
        stationaryTargets[stationaryTargetIndex].gameObject.SetActive(false);
        IsRunningS = false;
    }

    IEnumerator UpdateMovingTargets()
    {
        IsRunningM = true;
        var movingTargetIndex = UnityEngine.Random.Range(0, movingTargets.Length);
        movingTargets[movingTargetIndex].gameObject.SetActive(true);
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 10));
        movingTargets[movingTargetIndex].gameObject.SetActive(false);
        IsRunningM = false;
    }
}
