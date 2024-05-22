using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public enum PoolType { Weapon};
[System.Serializable]
class Pool
{
    public GameObject Prefab;
    public PoolType poolType;
    public Transform Parent;
    public int size;
}
public class ObjectPooling : Singleton<ObjectPooling>
{
    public PoolType poolType;
    [SerializeField] private List<Pool> pools;
    private Dictionary<PoolType,IObjectPool<GameObject>> pooldictionary;

    private void Start()
    {
        pooldictionary = new Dictionary<PoolType, IObjectPool<GameObject>>();
        foreach(var pool in pools)
        {
            IObjectPool<GameObject> Iobject = new ObjectPool<GameObject>(()
                => Instantiate(pool.Prefab,pool.Parent),
                obj => obj.SetActive(true),
                obj => obj.SetActive(false),
                obj=> Destroy(obj.gameObject),
                true,
                pool.size,
                pool.size*2
                );
            pooldictionary.Add(pool.poolType, Iobject);
        }
    }


    public GameObject SpawnGameObjectFromPool(PoolType poolType,Vector3 position,Quaternion rotation)
    {
        IObjectPool<GameObject> newgoj = pooldictionary[poolType];
        if(newgoj != null)
        {
            GameObject ins = newgoj.Get();
            ins.transform.position = position;
            ins.transform.rotation = rotation;  
            return ins;
        }
        else
        {
            return null;
        }
        
    }




    public void ReturnPoolObject(PoolType pooltype,GameObject gob)
    {
        IObjectPool<GameObject> pool = pooldictionary[pooltype];
        if (pool != null) {
            gob.transform.rotation = Quaternion.Euler(-90,0,0);
            pool.Release(gob);
        }
    }
}
