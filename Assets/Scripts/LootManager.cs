using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    public GameObject snackPrefab;
    public GameObject plane;

    public float snackOffset = 2f;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSnack()
    {
        Vector3 spawnPos;

        Vector3 min = plane.GetComponent<Renderer>().bounds.min;
        Vector3 max = plane.GetComponent<Renderer>().bounds.max;

        spawnPos = new Vector3(Random.Range(min.x + snackOffset, max.x - snackOffset), snackPrefab.transform.position.y, Random.Range(min.z + snackOffset, max.z - snackOffset));

        if (Physics.OverlapSphere(spawnPos, 0.2f).Length == 0)

            Instantiate(snackPrefab, spawnPos, snackPrefab.transform.rotation);
    }
}
