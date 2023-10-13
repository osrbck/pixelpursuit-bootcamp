using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _pool;
    [SerializeField] private Transform _matchPool;

    [SerializeField] private GameObject[] _itemPrefabs;

    [SerializeField] private int _coupleCount;

    [SerializeField] private int _matchCount;


    private void Start()
    {
        FillPool();
    }

    private void FillPool()
    {
        for(int i = 0; i<_coupleCount; i++)
        {
            GameObject go1 = Instantiate(_itemPrefabs[i % _itemPrefabs.Length], RandomPositionOverPool(),Quaternion.identity);
            go1.transform.SetParent(transform);
            GameObject go2 = Instantiate(_itemPrefabs[i % _itemPrefabs.Length], RandomPositionOverPool(), Quaternion.identity);
            go2.transform.SetParent(transform);
        }
    }

    private Vector3 RandomPositionOverPool()
    {
        Vector3 position = _pool.position;
        position += new Vector3(UnityEngine.Random.Range(-8f, 10f), UnityEngine.Random.Range(4f, 7f), UnityEngine.Random.Range(-8f, 10f));

        return position;
    }
}

