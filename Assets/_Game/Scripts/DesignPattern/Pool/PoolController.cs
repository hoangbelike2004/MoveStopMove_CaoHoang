using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField] private PoolAmount[] poolAmounts;


    private void Awake()
    {
        for(int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.PreLoad(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }

}

[System.Serializable]
public class PoolAmount
{
    public int amount;
    public GameUnit prefab;
    public Transform parent;
}

public enum PoolType_One
{
    Weapon_1 = 0,
    Weapon_2 = 1,
    Weapon_3 = 2,
    Weapon_4 = 3,
    Weapon_5 = 4,

}
