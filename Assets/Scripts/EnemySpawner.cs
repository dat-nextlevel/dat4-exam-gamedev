using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject desertEnemy;
    [SerializeField] private GameObject medievalEnemy;
    [SerializeField] private GameObject scifiEnemy;
    [SerializeField] private GameObject graveyardEnemy;
    private GameObject _playerPosition;
    private readonly int _spawnTimer = 15;
    private readonly List<Transform> _allZones = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        ZonePoints();
        StartCoroutine(Waiter());
    }

    private void Update()
    {
        _playerPosition = GameObject.Find("Player");      
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(_spawnTimer);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Transform spawnZone = GetClosestZone(_allZones.ToArray());
        Vector3 origin = spawnZone.position;
        Vector3 range = transform.localScale / 2.0f;
        Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
            Random.Range(-range.y, range.y),
            Random.Range(-range.z, range.z));
            
        Vector3 randomCoordinate = origin + randomRange;
        if (spawnZone.transform.parent.gameObject.name == "DesertSpawn")
        {
            Instantiate(desertEnemy, randomCoordinate, Quaternion.identity);
        }

        if (spawnZone.transform.parent.gameObject.name == "SciFiSpawn")
        {
            Instantiate(scifiEnemy, randomCoordinate, Quaternion.identity);

        }
        
        if (spawnZone.transform.parent.gameObject.name == "MedSpawn")
        {
            Instantiate(medievalEnemy, randomCoordinate, Quaternion.identity);

        }
        
        if (spawnZone.transform.parent.gameObject.name == "GraveyardSpawn")
        {
            Instantiate(graveyardEnemy, randomCoordinate, Quaternion.identity);

        }
    }

    private Transform GetClosestZone(Transform[] zones)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in _allZones)
        {
            float dist = Vector3.Distance(t.position, _playerPosition.transform.position);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        return tMin;
    }

    private void ZonePoints()
    {
        GameObject desertSpawn = GameObject.Find("DesertSpawn");
        GameObject scifiSpawn = GameObject.Find("SciFiSpawn");
        GameObject graveyardSpawn = GameObject.Find("GraveyardSpawn");
        GameObject medievalSpawn = GameObject.Find("MedSpawn");

        foreach (Transform child in desertSpawn.transform)
        {
            _allZones.Add(child);
        }
        foreach (Transform child in scifiSpawn.transform)
        {
            _allZones.Add(child);
        }
        foreach (Transform child in graveyardSpawn.transform)
        {
            _allZones.Add(child);
        }
        foreach (Transform child in medievalSpawn.transform)
        {
            _allZones.Add(child);
        }
    }
}
