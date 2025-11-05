using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowPool : MonoBehaviour
{
    [SerializeField] private int _startSizePool;
    private Dictionary<int, List<GameObject>> keyValuePairs = new Dictionary<int, List<GameObject>>();
    private List<GameObject> ObjectPool;
    private int _sizePool;
    private int index;

    public void Initialization()
    {
        _sizePool = _startSizePool;
    }

    public void SetWeaponThrow(Weapon weapon)
    {
        if (!keyValuePairs.ContainsKey(weapon.Id))
        {
            ObjectPool = new List<GameObject>();
            for (int i = 0; i < _sizePool; i++)
            {
                GameObject tempWeapon = Instantiate(weapon.Model, Vector3.zero, Quaternion.identity);
                tempWeapon.SetActive(false);
                ObjectPool.Add(tempWeapon);
            }
            keyValuePairs.Add(weapon.Id, ObjectPool);
        }
    }

    public GameObject GetWeaponGameObject(Weapon weapon)
    {
        GameObject tempgameObject = null;
        if (keyValuePairs.ContainsKey(weapon.Id))
        {
            tempgameObject = GetNextGameObject(weapon.Id);
        }
        tempgameObject.SetActive(true);
        return tempgameObject;
    }

    private GameObject GetNextGameObject(int id)
    {
        GameObject tempgameObject;
        ObjectPool = keyValuePairs[id];
        if (index < ObjectPool.Count)
        {
            tempgameObject = ObjectPool[index];
        }
        else
        {
            index = 0;
            tempgameObject = ObjectPool[index];
        }
        index++;
        return tempgameObject;
    }
}