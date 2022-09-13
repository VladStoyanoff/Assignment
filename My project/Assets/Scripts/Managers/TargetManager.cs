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

    [SerializeField] MeshFilter tntMesh;
    [SerializeField] Material tntMaterial;

    UIManager UIManagerScript;

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
        while (GameObject.FindGameObjectsWithTag("StationaryTarget").Length + GameObject.FindGameObjectsWithTag("MovingTarget").Length < 4)
        {
            StartCoroutine(UpdateStationaryTargets());
            StartCoroutine(UpdateMovingTargets());
        }
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
        var stationaryTargetIndex = UnityEngine.Random.Range(0, stationaryTargets.Length);
        stationaryTargets[stationaryTargetIndex].gameObject.SetActive(true);
        var randomTNT = UnityEngine.Random.Range(0, 20);
        if (randomTNT <= 5 && GameObject.FindGameObjectsWithTag("Bomb").Length < 1)
        {
            stationaryTargets[stationaryTargetIndex].GetComponent<MeshFilter>().sharedMesh = tntMesh.sharedMesh;
            stationaryTargets[stationaryTargetIndex].GetComponent<MeshRenderer>().material = tntMaterial;
            stationaryTargets[stationaryTargetIndex].gameObject.tag = "Bomb";
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 10));
        if (stationaryTargets[stationaryTargetIndex] != null)
        {
            stationaryTargets[stationaryTargetIndex].gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateMovingTargets()
    {
        var movingTargetIndex = UnityEngine.Random.Range(0, movingTargets.Length);
        movingTargets[movingTargetIndex].gameObject.SetActive(true);
        var randomTNT = UnityEngine.Random.Range(0, 20);
        if (randomTNT <= 5 && GameObject.FindGameObjectsWithTag("Bomb").Length < 1)
        {
            movingTargets[movingTargetIndex].GetComponent<MeshFilter>().sharedMesh = tntMesh.sharedMesh;
            movingTargets[movingTargetIndex].GetComponent<MeshRenderer>().material = tntMaterial;
            movingTargets[movingTargetIndex].gameObject.tag = "Bomb";
        }
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 10));
        if (movingTargets[movingTargetIndex] != null)
        {
            movingTargets[movingTargetIndex].gameObject.SetActive(false);
        }
    }
}
