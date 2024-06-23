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
    Hammer = 0,
    Candy_0 = 1,
    Axe = 2,
    Boomerang = 3,
    Arrow = 4,

}
