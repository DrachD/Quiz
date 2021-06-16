using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// object pool is needed to optimize the game
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public int maxPool = 9;

    [SerializeField] GameObject _prefabCell;

    public List<GameObject> _allObjectCells = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject obj = Instantiate(_prefabCell, Vector2.zero, Quaternion.identity);
            _allObjectCells.Add(obj);
            _allObjectCells[i].SetActive(false);
        }
    }

    public void Reset()
    {
        foreach (GameObject obj in _allObjectCells)
        {
            obj.SetActive(false);
        }
    }
}
