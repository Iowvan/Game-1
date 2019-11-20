using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject[] GroundPrefabs;
    private Transform _playerTransform;
    private float _spawnOffsetX = 0.0f;
    private float _groundLength = 40.0f;
    private int _concurrentGroundsAmount = 3;
    private int _lastGroundIndex = 0;
    private int _coinsPerGround = 6;
    private int _coinsPerSpawn = 3;

    private List<GameObject> _spawnedGrounds;
    private List<GameObject> _spawnedCoins;

    private void Start()
    {
        _spawnedGrounds = new List<GameObject>();
        _spawnedCoins = new List<GameObject>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < _concurrentGroundsAmount; i++)
        {
            SpawnCoins();
            if (i < 1)
                SpawnGround(0);               
            else
                SpawnGround();
        }
    }

    private void Update()
    {
        if (_playerTransform.position.x - _groundLength > (_spawnOffsetX - (_concurrentGroundsAmount * _groundLength)))
        {
            SpawnGround();
            DestroyGround();
            SpawnCoins();
            DestroyCoins();
        }
    }

    private void SpawnGround(int groundIndex = -1)
    {
        GameObject gameObject;
        if (groundIndex == -1)
            gameObject = Instantiate(GroundPrefabs[GetRandomGroundIndex()]) as GameObject;
        else
            gameObject = Instantiate(GroundPrefabs[groundIndex]) as GameObject;
        gameObject.transform.SetParent(transform);
        gameObject.transform.position = Vector3.right * _spawnOffsetX;
        _spawnOffsetX += _groundLength;
        _spawnedGrounds.Add(gameObject);
    }

    private void DestroyGround()
    {
        Destroy(_spawnedGrounds[0]);
        _spawnedGrounds.RemoveAt(0);
    }

    private int GetRandomGroundIndex()
    {
        if (GroundPrefabs.Length <= 1)
            return 0;

        int randomGroundIndex = _lastGroundIndex;
        while (randomGroundIndex == _lastGroundIndex)
        {
            randomGroundIndex = Random.Range(0, GroundPrefabs.Length);
        }

        _lastGroundIndex = randomGroundIndex;
        return randomGroundIndex;
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < _coinsPerGround / _coinsPerSpawn; i++)
        {
            GameObject gameObjectParent = Instantiate(CoinPrefab) as GameObject;
            gameObjectParent.transform.SetParent(transform);
            gameObjectParent.transform.position = GetRandomCoinSpawn();
            _spawnedCoins.Add(gameObjectParent);

            for (int k = 0; k < _coinsPerSpawn - 1; k++)
            {
                GameObject gameObjectChild = Instantiate(CoinPrefab) as GameObject;
                gameObjectChild.transform.SetParent(transform);
                gameObjectChild.transform.position = new Vector3(_spawnedCoins[_spawnedCoins.Count - 1].transform.position.x + 1, _spawnedCoins[_spawnedCoins.Count - 1].transform.position.y, 0);
                _spawnedCoins.Add(gameObjectChild);        
            }
        }
    }

    private void DestroyCoins()
    {
        for (int i = 0; i < _coinsPerGround; i++)
        {
            Destroy(_spawnedCoins[0]);
            _spawnedCoins.RemoveAt(0);
        }
    }

    private Vector3 GetRandomCoinSpawn()
    {
        float x = 0f;
        float y = 0f;
        int spawnPosX = Random.Range(0, 3);
        int spawnPosY = Random.Range(0, 1);

        switch (spawnPosX)
        {
            case 0:
                x = Random.Range(-19, -13);
                break;
            case 1:
                x = Random.Range(-9, -3);
                break;
            case 2:
                x = Random.Range(1, 7);
                break;
            case 3:
                x = Random.Range(11, 17);
                break;
            default:
                Debug.Log("spawnPosX selection error!");
                break;
        }

        switch (spawnPosY)
        {
            case 0:
                y = 2f;
                break;
            case 1:
                y = 4f;
                break;
            default:
                Debug.Log("spawnPosY selection error!");
                break;
        }

        return new Vector3(x + _spawnOffsetX, y, 0);
    }
}
