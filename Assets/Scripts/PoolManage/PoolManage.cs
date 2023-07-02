using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManage : MonoBehaviour
{

    static Dictionary<GameObject, Pool> poolDictionary;
    [SerializeField] public Pool[] poolParticles;
    [SerializeField] public Pool[] vFXPools;

    [SerializeField] public Pool[] enemyPools;

    void Start()
    {
        poolDictionary = new Dictionary<GameObject, Pool>();
    
        Initialize(poolParticles);
        Initialize(vFXPools);
        Initialize(enemyPools);
    }

    void CheckPoolSize(Pool[] pools)
    {
        foreach (var pool in pools)
        {
            if (pool.RuntimeSize > pool.Size)
            {
                Debug.LogWarning(
                    string.Format("Poll: {0} has a runtime size {1} bigger than its initial size{2}",
                    pool.Prefab.name, pool.RuntimeSize, pool.Size)
                );
            }
        }
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        CheckPoolSize(poolParticles);
        CheckPoolSize(vFXPools);
        CheckPoolSize(enemyPools);
    }

#endif

    void Initialize(Pool[] pools)
    {
        foreach (var pool in pools)
        {

#if UNITY_EDITOR
            if (poolDictionary.ContainsKey(pool.Prefab))
            {
                Debug.LogError("Alert: Same prefab in multiple pools! Prefab:" + pool.Prefab.name);
                continue;
            }
#endif

            poolDictionary.Add(pool.Prefab, pool);

            Transform poolParent = new GameObject("pool" + pool.Prefab.name).transform;
            poolParent.parent = transform;
            pool.Initialize(poolParent);
        }
    }



    /// <summary>
    ///  Return a Specified gameobject in the poll
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public static GameObject Release(GameObject prefab)
    {
#if UNITY_EDITOR
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError("Alert: Can't release prefab which don't contains in distionary" + prefab.name);
            return null;
        }
#endif

        return poolDictionary[prefab].PrepareObject();
    }
    public static GameObject Release(GameObject prefab, Vector3 position)
    {
#if UNITY_EDITOR
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError("Alert: Can't release prefab which don't contains in distionary" + prefab.name);
            return null;
        }
#endif

        return poolDictionary[prefab].PrepareObject(position);
    }

    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation)
    {
#if UNITY_EDITOR
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError("Alert: Can't release prefab which don't contains in distionary" + prefab.name);
            return null;
        }
#endif

        return poolDictionary[prefab].PrepareObject(position, rotation);
    }
    public static GameObject Release(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 localScale)
    {
#if UNITY_EDITOR
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogError("Alert: Can't release prefab which don't contains in distionary" + prefab.name);
            return null;
        }
#endif

        return poolDictionary[prefab].PrepareObject(position, rotation, localScale);
    }


}
