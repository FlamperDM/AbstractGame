using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool
{
    static List<Stack<GameObject>> Pools;
    static List<GameObject> DisablePool;

    static bool initiated;

    static GameObject parent;


    public static void Init()
    {
        Pools = new List<Stack<GameObject>>();
        DisablePool = new List<GameObject>();
        initiated = true;
        parent = GameObject.Find("ObjectsPool");
        if (parent == null) Object.Destroy(parent);

        parent = new GameObject("ObjectsPool");

        parent.SetActive(false);
    }

    public static void CreatePools(int quantity, string namePool = default(string))
    {
        Init();
        for (int i = 0; i < quantity; i++)
        {
            var pool = new GameObject(namePool + i.ToString());
            pool.transform.SetParent(parent.transform);
            DisablePool.Add(pool);
            Pools.Add(new Stack<GameObject>());
        }
    }

    public static GameObject Spawn(GameObject obj, int idPool, Vector3 spawnPosition = default(Vector3), Quaternion spawnRotation = default(Quaternion), Transform transform = default(Transform))
    {
        if (idPool >= Pools.Count || !initiated)
        {
            return null;
        }
        var gmObjs = new Stack<GameObject>();
        gmObjs = Pools[idPool];

        if (gmObjs.Count > 0)
        {
            obj = gmObjs.Pop();
            obj.transform.position = spawnPosition;
            obj.transform.rotation = spawnRotation;
            obj.transform.SetParent(transform);
        }
        else
        {
            obj = Object.Instantiate(obj, spawnPosition, spawnRotation, transform);
        }
        return obj;
    }

    public static void Despawn(int idPool, GameObject obj)
    {
        if (idPool < Pools.Count)
        {
            obj.transform.SetParent(DisablePool[idPool].transform.parent);
            Pools[idPool].Push(obj);
        }
    }
}
